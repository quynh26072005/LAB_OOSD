using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Services
{
    public class GiaHanService
    {
        private readonly HopDongService hopDong = new HopDongService();

        public DataTable LayPhieuGiaHan()
        {
            return Db.Query(@"SELECT SoPhieuGH, SoHopDong, MaPhong, NgayLap, SoThangGiaHan, NgayKetThucCu, NgayKetThucMoi, MaNV
                              FROM PhieuGiaHan ORDER BY NgayLap DESC, SoPhieuGH DESC");
        }

        /// <summary>Tien thue da den han chua nop + dich vu da lap hoa don chua nop cua mot phong trong hop dong.</summary>
        public CongNoPhong TinhCongNo(string soHopDong, string maPhong, DateTime ngay)
        {
            object thue = Db.Scalar(@"SELECT ISNULL(SUM(k.SoTien-ISNULL((SELECT SUM(SoTien) FROM PhieuThu WHERE MaKy=k.MaKy),0)),0)
                                      FROM KyThanhToan k
                                      WHERE k.SoHopDong=@s AND k.MaPhong=@p AND k.HanNop<=@n",
                new SqlParameter("@s", soHopDong), new SqlParameter("@p", maPhong), new SqlParameter("@n", ngay.Date));
            object dv = Db.Scalar(@"SELECT ISNULL(SUM(h.TongTien-ISNULL((SELECT SUM(SoTien) FROM PhieuThu WHERE SoHDDV=h.SoHDDV),0)),0)
                                    FROM HoaDonDichVu h WHERE h.SoHopDong=@s AND h.MaPhong=@p",
                new SqlParameter("@s", soHopDong), new SqlParameter("@p", maPhong));
            return new CongNoPhong { NoTienThue = Convert.ToDecimal(thue), NoDichVu = Convert.ToDecimal(dv) };
        }

        public KetQuaXuLy LapPhieuGiaHan(string soPhieu, string soHopDong, string maPhong, int soThangGiaHan,
                                         DateTime ngayLap, string maNV)
        {
            if (string.IsNullOrWhiteSpace(soPhieu) || string.IsNullOrWhiteSpace(soHopDong) ||
                string.IsNullOrWhiteSpace(maPhong) || string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail("Phiếu gia hạn chưa đủ thông tin.");
            if (soThangGiaHan <= 0) return KetQuaXuLy.Fail("Số tháng gia hạn phải lớn hơn 0.");

            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var q = new SqlCommand(@"SELECT NgayBatDau, SoThang, NgayKetThuc, DonGiaThue
                                             FROM ChiTietHopDong WITH (UPDLOCK)
                                             WHERE SoHopDong=@s AND MaPhong=@p", cn, tx);
                    q.Parameters.AddWithValue("@s", soHopDong);
                    q.Parameters.AddWithValue("@p", maPhong);
                    DateTime batDau, ketThucCu; int soThang; decimal donGia;
                    using (var rd = q.ExecuteReader())
                    {
                        if (!rd.Read()) { rd.Close(); tx.Rollback(); return KetQuaXuLy.Fail("Phòng không thuộc hợp đồng này."); }
                        batDau = rd.GetDateTime(0); soThang = rd.GetInt32(1);
                        ketThucCu = rd.GetDateTime(2); donGia = rd.GetDecimal(3);
                    }

                    if (ngayLap.Date > ketThucCu.Date)
                    { tx.Rollback(); return KetQuaXuLy.Fail("Hợp đồng của phòng đã hết hạn, không thể gia hạn."); }

                    // BR08: thoi gian gia han <= 1/2 thoi gian thue
                    if (soThangGiaHan * 2 > soThang)
                    { tx.Rollback(); return KetQuaXuLy.Fail("Thời gian gia hạn tối đa " + (soThang / 2) + " tháng (không quá 1/2 thời gian thuê " + soThang + " tháng)."); }

                    // BR09: khong con no tien thue va da thanh toan day du dich vu
                    CongNoPhong no = TinhCongNo(soHopDong, maPhong, ngayLap);
                    if (no.ConNo)
                    {
                        tx.Rollback();
                        return KetQuaXuLy.Fail("Không thể gia hạn: còn nợ tiền thuê " + no.NoTienThue.ToString("N0") +
                                               ", nợ dịch vụ " + no.NoDichVu.ToString("N0") + ".");
                    }

                    DateTime ketThucMoi = HopDongService.TinhNgayKetThuc(batDau, soThang + soThangGiaHan);

                    var tr = new SqlCommand(@"SELECT COUNT(*) FROM ChiTietHopDong
                                              WHERE MaPhong=@p AND SoHopDong<>@s AND NgayBatDau>@cu AND NgayBatDau<=@moi", cn, tx);
                    tr.Parameters.AddWithValue("@p", maPhong);
                    tr.Parameters.AddWithValue("@s", soHopDong);
                    tr.Parameters.AddWithValue("@cu", ketThucCu);
                    tr.Parameters.AddWithValue("@moi", ketThucMoi);
                    if (Convert.ToInt32(tr.ExecuteScalar()) > 0)
                    { tx.Rollback(); return KetQuaXuLy.Fail("Phòng đã được hợp đồng khác đặt trong khoảng gia hạn."); }

                    var ins = new SqlCommand(@"INSERT INTO PhieuGiaHan VALUES(@p,@s,@ph,@nl,@st,@cu,@moi,@nv)", cn, tx);
                    ins.Parameters.AddWithValue("@p", soPhieu);
                    ins.Parameters.AddWithValue("@s", soHopDong);
                    ins.Parameters.AddWithValue("@ph", maPhong);
                    ins.Parameters.AddWithValue("@nl", ngayLap.Date);
                    ins.Parameters.AddWithValue("@st", soThangGiaHan);
                    ins.Parameters.AddWithValue("@cu", ketThucCu);
                    ins.Parameters.AddWithValue("@moi", ketThucMoi);
                    ins.Parameters.AddWithValue("@nv", maNV);
                    ins.ExecuteNonQuery();

                    var up = new SqlCommand(@"UPDATE ChiTietHopDong SET SoThang=@st, NgayKetThuc=@kt
                                              WHERE SoHopDong=@s AND MaPhong=@p", cn, tx);
                    up.Parameters.AddWithValue("@st", soThang + soThangGiaHan);
                    up.Parameters.AddWithValue("@kt", ketThucMoi);
                    up.Parameters.AddWithValue("@s", soHopDong);
                    up.Parameters.AddWithValue("@p", maPhong);
                    up.ExecuteNonQuery();

                    // cac thang gia han deu >= thang 7 nen tra dau thang
                    hopDong.SinhLich(cn, tx, soHopDong, maPhong, batDau, donGia, soThang + 1, soThang + soThangGiaHan);

                    var hd = new SqlCommand("UPDATE HopDong SET TrangThai=N'Hiệu lực' WHERE SoHopDong=@s", cn, tx);
                    hd.Parameters.AddWithValue("@s", soHopDong);
                    hd.ExecuteNonQuery();
                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã gia hạn phòng " + maPhong + " đến " + ketThucMoi.ToString("dd/MM/yyyy") + ".");
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }
    }
}
