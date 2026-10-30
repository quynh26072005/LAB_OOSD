using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Services
{
    public class HopDongService
    {
        public const int ThoiGianThueToiThieu = 6;   // BR04
        public const int SoThangTraTruoc = 6;        // BR05

        public DataTable LayKhach()
        {
            return Db.Query("SELECT * FROM KhachHang ORDER BY TenKhach");
        }

        public DataTable LayHopDong()
        {
            return Db.Query(@"SELECT h.SoHopDong, k.TenKhach, h.NgayLap, h.TrangThai, h.MaNV, h.MaKhach
                              FROM HopDong h JOIN KhachHang k ON h.MaKhach=k.MaKhach
                              ORDER BY h.NgayLap DESC, h.SoHopDong DESC");
        }

        public DataTable LayChiTiet(string soHopDong)
        {
            return Db.Query(@"SELECT c.MaPhong, t.TenTang, p.DienTich, p.HuongPhong,
                                     c.NgayBatDau, c.SoThang, c.NgayKetThuc, c.DonGiaThue
                              FROM ChiTietHopDong c
                              JOIN Phong p ON c.MaPhong=p.MaPhong
                              JOIN Tang t ON p.MaTang=t.MaTang
                              WHERE c.SoHopDong=@s ORDER BY c.MaPhong",
                new SqlParameter("@s", soHopDong));
        }

        public DataTable LayLichThanhToan(string soHopDong)
        {
            return Db.Query(@"SELECT k.MaKy, k.MaPhong, k.TuThang, k.DenThang, k.TuNgay, k.DenNgay,
                                     k.SoTien, k.HanNop, k.TrangThai
                              FROM KyThanhToan k WHERE k.SoHopDong=@s
                              ORDER BY k.MaPhong, k.TuThang",
                new SqlParameter("@s", soHopDong));
        }

        public KetQuaXuLy ThemKhach(string ma, string ten, string soGiayTo, string nguoiDaiDien, string sdt, string diaChi)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) ||
                string.IsNullOrWhiteSpace(soGiayTo) || string.IsNullOrWhiteSpace(nguoiDaiDien))
                return KetQuaXuLy.Fail("Thông tin khách thuê chưa đầy đủ.");
            try
            {
                Db.Execute("INSERT INTO KhachHang VALUES(@m,@t,@g,@d,@s,@a)",
                    new SqlParameter("@m", ma), new SqlParameter("@t", ten), new SqlParameter("@g", soGiayTo),
                    new SqlParameter("@d", nguoiDaiDien), new SqlParameter("@s", (object)sdt ?? DBNull.Value),
                    new SqlParameter("@a", (object)diaChi ?? DBNull.Value));
                return KetQuaXuLy.Ok("Đã lưu khách thuê.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        /// <summary>Ngay ket thuc = ngay bat dau + soThang thang - 1 ngay.</summary>
        public static DateTime TinhNgayKetThuc(DateTime batDau, int soThang)
        {
            return batDau.Date.AddMonths(soThang).AddDays(-1);
        }

        private bool PhongTrungLich(SqlConnection cn, SqlTransaction tx, string maPhong, DateTime batDau, DateTime ketThuc)
        {
            var cmd = new SqlCommand(@"SELECT COUNT(*) FROM ChiTietHopDong WITH (UPDLOCK, HOLDLOCK)
                                       WHERE MaPhong=@p AND @bd<=NgayKetThuc AND @kt>=NgayBatDau", cn, tx);
            cmd.Parameters.AddWithValue("@p", maPhong);
            cmd.Parameters.AddWithValue("@bd", batDau.Date);
            cmd.Parameters.AddWithValue("@kt", ketThuc.Date);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private void ThemKy(SqlConnection cn, SqlTransaction tx, string soHD, string maPhong,
                            int tuThang, int denThang, DateTime batDauHD, decimal donGia)
        {
            DateTime tu = batDauHD.Date.AddMonths(tuThang - 1);
            DateTime den = batDauHD.Date.AddMonths(denThang).AddDays(-1);
            var cmd = new SqlCommand(@"INSERT INTO KyThanhToan(SoHopDong,MaPhong,TuThang,DenThang,TuNgay,DenNgay,SoTien,HanNop)
                                       VALUES(@s,@p,@tt,@dt,@tn,@dn,@st,@h)", cn, tx);
            cmd.Parameters.AddWithValue("@s", soHD);
            cmd.Parameters.AddWithValue("@p", maPhong);
            cmd.Parameters.AddWithValue("@tt", tuThang);
            cmd.Parameters.AddWithValue("@dt", denThang);
            cmd.Parameters.AddWithValue("@tn", tu);
            cmd.Parameters.AddWithValue("@dn", den);
            cmd.Parameters.AddWithValue("@st", donGia * (denThang - tuThang + 1));
            cmd.Parameters.AddWithValue("@h", tu);          // han nop = ngay dau ky
            cmd.ExecuteNonQuery();
        }

        /// <summary>Sinh lich thanh toan cho khoang thang [tuThang, denThang] cua mot phong.
        /// Thang 1-6: mot ky tra truoc; tu thang 7: moi thang mot ky, nop dau thang.</summary>
        public void SinhLich(SqlConnection cn, SqlTransaction tx, string soHD, string maPhong,
                             DateTime batDauHD, decimal donGia, int tuThang, int denThang)
        {
            int m = tuThang;
            if (m == 1)
            {
                ThemKy(cn, tx, soHD, maPhong, 1, SoThangTraTruoc, batDauHD, donGia);
                m = SoThangTraTruoc + 1;
            }
            for (; m <= denThang; m++)
                ThemKy(cn, tx, soHD, maPhong, m, m, batDauHD, donGia);
        }

        public KetQuaXuLy TaoHopDong(string soHD, string maKhach, string maNV, DateTime ngayLap,
                                     DateTime batDau, int soThang, string noiDung, List<PhongThueItem> ds)
        {
            if (string.IsNullOrWhiteSpace(soHD) || string.IsNullOrWhiteSpace(maKhach) ||
                string.IsNullOrWhiteSpace(maNV) || ds == null || ds.Count == 0)
                return KetQuaXuLy.Fail("Hợp đồng chưa đủ thông tin hoặc chưa chọn phòng.");
            if (soThang < ThoiGianThueToiThieu)
                return KetQuaXuLy.Fail("Thời gian thuê tối thiểu " + ThoiGianThueToiThieu + " tháng.");
            if (batDau.Date < ngayLap.Date)
                return KetQuaXuLy.Fail("Ngày bắt đầu thuê không được trước ngày lập hợp đồng.");

            DateTime ketThuc = TinhNgayKetThuc(batDau, soThang);
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var hd = new SqlCommand(@"INSERT INTO HopDong(SoHopDong,MaKhach,MaNV,NgayLap,NoiDung)
                                              VALUES(@s,@k,@n,@l,@nd)", cn, tx);
                    hd.Parameters.AddWithValue("@s", soHD);
                    hd.Parameters.AddWithValue("@k", maKhach);
                    hd.Parameters.AddWithValue("@n", maNV);
                    hd.Parameters.AddWithValue("@l", ngayLap.Date);
                    hd.Parameters.AddWithValue("@nd", (object)noiDung ?? DBNull.Value);
                    hd.ExecuteNonQuery();

                    foreach (var x in ds)
                    {
                        var q = new SqlCommand("SELECT TrangThai FROM Phong WHERE MaPhong=@p", cn, tx);
                        q.Parameters.AddWithValue("@p", x.MaPhong);
                        object tt = q.ExecuteScalar();
                        if (tt == null) { tx.Rollback(); return KetQuaXuLy.Fail("Không tìm thấy phòng " + x.MaPhong); }
                        if (Convert.ToString(tt) == "Bảo trì") { tx.Rollback(); return KetQuaXuLy.Fail("Phòng " + x.MaPhong + " đang bảo trì."); }
                        if (PhongTrungLich(cn, tx, x.MaPhong, batDau, ketThuc))
                        { tx.Rollback(); return KetQuaXuLy.Fail("Phòng " + x.MaPhong + " đã có hợp đồng trong khoảng thời gian thuê."); }

                        var ct = new SqlCommand(@"INSERT INTO ChiTietHopDong(SoHopDong,MaPhong,NgayBatDau,SoThang,NgayKetThuc,DonGiaThue)
                                                  VALUES(@s,@p,@bd,@st,@kt,@g)", cn, tx);
                        ct.Parameters.AddWithValue("@s", soHD);
                        ct.Parameters.AddWithValue("@p", x.MaPhong);
                        ct.Parameters.AddWithValue("@bd", batDau.Date);
                        ct.Parameters.AddWithValue("@st", soThang);
                        ct.Parameters.AddWithValue("@kt", ketThuc);
                        ct.Parameters.AddWithValue("@g", x.DonGiaThang);
                        ct.ExecuteNonQuery();

                        SinhLich(cn, tx, soHD, x.MaPhong, batDau, x.DonGiaThang, 1, soThang);

                        var up = new SqlCommand("UPDATE Phong SET TrangThai=N'Đang thuê' WHERE MaPhong=@p", cn, tx);
                        up.Parameters.AddWithValue("@p", x.MaPhong);
                        up.ExecuteNonQuery();
                    }
                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã lập hợp đồng " + soHD + " và sinh lịch thanh toán.");
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        /// <summary>Danh dau hop dong het han va tra phong ve Trong khi khong con hop dong hieu luc.</summary>
        public void CapNhatTrangThai()
        {
            Db.Execute(@"UPDATE HopDong SET TrangThai=N'Hết hạn'
                         WHERE TrangThai=N'Hiệu lực'
                           AND NOT EXISTS(SELECT 1 FROM ChiTietHopDong c
                                          WHERE c.SoHopDong=HopDong.SoHopDong AND c.NgayKetThuc>=CAST(GETDATE() AS date))");
            Db.Execute(@"UPDATE Phong SET TrangThai=N'Trống'
                         WHERE TrangThai=N'Đang thuê'
                           AND NOT EXISTS(SELECT 1 FROM ChiTietHopDong c
                                          WHERE c.MaPhong=Phong.MaPhong AND c.NgayKetThuc>=CAST(GETDATE() AS date))");
        }
    }
}
