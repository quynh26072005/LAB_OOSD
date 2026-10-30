using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Services
{
    public class ThanhToanService
    {
        // ---------- Tien thue ----------
        public DataTable LayKy(string soHopDong)
        {
            return Db.Query(@"SELECT k.MaKy, k.MaPhong, k.TuThang, k.DenThang, k.TuNgay, k.DenNgay, k.SoTien, k.HanNop,
                                     ISNULL((SELECT SUM(SoTien) FROM PhieuThu WHERE MaKy=k.MaKy),0) AS DaThu,
                                     k.SoTien-ISNULL((SELECT SUM(SoTien) FROM PhieuThu WHERE MaKy=k.MaKy),0) AS ConNo,
                                     k.TrangThai
                              FROM KyThanhToan k WHERE k.SoHopDong=@s ORDER BY k.MaPhong, k.TuThang",
                new SqlParameter("@s", soHopDong));
        }

        private static string TrangThaiTheoTien(decimal daThu, decimal tong)
        {
            if (daThu <= 0) return "Chưa thanh toán";
            return daThu >= tong ? "Đã thanh toán" : "Thanh toán một phần";
        }

        public KetQuaXuLy ThuTienThue(string soPhieuThu, int maKy, decimal soTien, string hinhThuc, string maNV)
        {
            if (string.IsNullOrWhiteSpace(soPhieuThu) || string.IsNullOrWhiteSpace(hinhThuc) || string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail("Phiếu thu chưa đủ thông tin.");
            if (soTien <= 0) return KetQuaXuLy.Fail("Số tiền thu phải lớn hơn 0.");
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var q = new SqlCommand(@"SELECT k.SoTien, ISNULL((SELECT SUM(SoTien) FROM PhieuThu WHERE MaKy=k.MaKy),0)
                                             FROM KyThanhToan k WITH (UPDLOCK) WHERE k.MaKy=@k", cn, tx);
                    q.Parameters.AddWithValue("@k", maKy);
                    decimal tong, da;
                    using (var rd = q.ExecuteReader())
                    {
                        if (!rd.Read()) { rd.Close(); tx.Rollback(); return KetQuaXuLy.Fail("Không tìm thấy kỳ thanh toán."); }
                        tong = rd.GetDecimal(0); da = rd.GetDecimal(1);
                    }
                    if (soTien > tong - da)
                    { tx.Rollback(); return KetQuaXuLy.Fail("Số tiền vượt quá còn nợ của kỳ (" + (tong - da).ToString("N0") + ")."); }

                    var ins = new SqlCommand(@"INSERT INTO PhieuThu(SoPhieuThu,NgayThu,SoTien,HinhThuc,MaKy,MaNV)
                                               VALUES(@p,GETDATE(),@t,@h,@k,@n)", cn, tx);
                    ins.Parameters.AddWithValue("@p", soPhieuThu);
                    ins.Parameters.AddWithValue("@t", soTien);
                    ins.Parameters.AddWithValue("@h", hinhThuc);
                    ins.Parameters.AddWithValue("@k", maKy);
                    ins.Parameters.AddWithValue("@n", maNV);
                    ins.ExecuteNonQuery();

                    var up = new SqlCommand("UPDATE KyThanhToan SET TrangThai=@tt WHERE MaKy=@k", cn, tx);
                    up.Parameters.AddWithValue("@tt", TrangThaiTheoTien(da + soTien, tong));
                    up.Parameters.AddWithValue("@k", maKy);
                    up.ExecuteNonQuery();
                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã ghi nhận thu tiền thuê.");
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        // ---------- Dich vu cuoi thang ----------
        public DataTable LayDichVu()
        {
            return Db.Query("SELECT MaDV, TenDV, DonViTinh, DonGia FROM DichVu ORDER BY MaDV");
        }

        public DataTable LayHoaDonDV(string soHopDong)
        {
            return Db.Query(@"SELECT h.SoHDDV, h.MaPhong, h.Thang, h.Nam, h.NgayLap, h.HanNop, h.TongTien,
                                     ISNULL((SELECT SUM(SoTien) FROM PhieuThu WHERE SoHDDV=h.SoHDDV),0) AS DaThu,
                                     h.TrangThai
                              FROM HoaDonDichVu h WHERE h.SoHopDong=@s ORDER BY h.Nam, h.Thang, h.MaPhong",
                new SqlParameter("@s", soHopDong));
        }

        public DataTable LayChiTietHoaDonDV(string soHDDV)
        {
            return Db.Query(@"SELECT c.MaDV, d.TenDV, c.SoLuong, c.DonGia, c.ThanhTien
                              FROM ChiTietHoaDonDV c JOIN DichVu d ON c.MaDV=d.MaDV WHERE c.SoHDDV=@s",
                new SqlParameter("@s", soHDDV));
        }

        public KetQuaXuLy LapHoaDonDV(string soHDDV, string soHopDong, string maPhong, int thang, int nam,
                                      string maNV, List<DichVuDungItem> ds)
        {
            if (string.IsNullOrWhiteSpace(soHDDV) || string.IsNullOrWhiteSpace(soHopDong) ||
                string.IsNullOrWhiteSpace(maPhong) || string.IsNullOrWhiteSpace(maNV) || ds == null || ds.Count == 0)
                return KetQuaXuLy.Fail("Hóa đơn dịch vụ chưa đủ thông tin.");
            if (thang < 1 || thang > 12) return KetQuaXuLy.Fail("Tháng không hợp lệ.");
            foreach (var x in ds)
                if (x.SoLuong <= 0) return KetQuaXuLy.Fail("Số lượng dịch vụ phải lớn hơn 0.");

            DateTime dauThang = new DateTime(nam, thang, 1);
            DateTime cuoiThang = dauThang.AddMonths(1).AddDays(-1);
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var q = new SqlCommand(@"SELECT COUNT(*) FROM ChiTietHopDong
                                             WHERE SoHopDong=@s AND MaPhong=@p AND NgayBatDau<=@cuoi AND NgayKetThuc>=@dau", cn, tx);
                    q.Parameters.AddWithValue("@s", soHopDong);
                    q.Parameters.AddWithValue("@p", maPhong);
                    q.Parameters.AddWithValue("@dau", dauThang);
                    q.Parameters.AddWithValue("@cuoi", cuoiThang);
                    if (Convert.ToInt32(q.ExecuteScalar()) == 0)
                    { tx.Rollback(); return KetQuaXuLy.Fail("Phòng không thuộc hợp đồng hoặc tháng nằm ngoài thời gian thuê."); }

                    decimal tong = 0;
                    foreach (var x in ds) tong += x.SoLuong * x.DonGia;

                    var hd = new SqlCommand(@"INSERT INTO HoaDonDichVu(SoHDDV,SoHopDong,MaPhong,Thang,Nam,NgayLap,HanNop,TongTien,MaNV)
                                              VALUES(@h,@s,@p,@t,@n,CAST(GETDATE() AS date),@han,@tong,@nv)", cn, tx);
                    hd.Parameters.AddWithValue("@h", soHDDV);
                    hd.Parameters.AddWithValue("@s", soHopDong);
                    hd.Parameters.AddWithValue("@p", maPhong);
                    hd.Parameters.AddWithValue("@t", thang);
                    hd.Parameters.AddWithValue("@n", nam);
                    hd.Parameters.AddWithValue("@han", cuoiThang);     // BR07: thanh toan cuoi thang
                    hd.Parameters.AddWithValue("@tong", tong);
                    hd.Parameters.AddWithValue("@nv", maNV);
                    hd.ExecuteNonQuery();

                    foreach (var x in ds)
                    {
                        var ct = new SqlCommand(@"INSERT INTO ChiTietHoaDonDV VALUES(@h,@d,@sl,@g,@tt)", cn, tx);
                        ct.Parameters.AddWithValue("@h", soHDDV);
                        ct.Parameters.AddWithValue("@d", x.MaDV);
                        ct.Parameters.AddWithValue("@sl", x.SoLuong);
                        ct.Parameters.AddWithValue("@g", x.DonGia);
                        ct.Parameters.AddWithValue("@tt", x.SoLuong * x.DonGia);
                        ct.ExecuteNonQuery();
                    }
                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã lập hóa đơn dịch vụ " + soHDDV + ", tổng " + tong.ToString("N0") + ".");
                }
                catch (SqlException ex)
                {
                    try { tx.Rollback(); } catch { }
                    if (ex.Number == 2627 || ex.Number == 2601)
                        return KetQuaXuLy.Fail("Phòng này đã có hóa đơn dịch vụ của tháng " + thang + "/" + nam + ".");
                    return KetQuaXuLy.Fail(ex.Message);
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        public KetQuaXuLy ThuTienDichVu(string soPhieuThu, string soHDDV, decimal soTien, string hinhThuc, string maNV)
        {
            if (string.IsNullOrWhiteSpace(soPhieuThu) || string.IsNullOrWhiteSpace(soHDDV) ||
                string.IsNullOrWhiteSpace(hinhThuc) || string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail("Phiếu thu chưa đủ thông tin.");
            if (soTien <= 0) return KetQuaXuLy.Fail("Số tiền thu phải lớn hơn 0.");
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var q = new SqlCommand(@"SELECT h.TongTien, ISNULL((SELECT SUM(SoTien) FROM PhieuThu WHERE SoHDDV=h.SoHDDV),0)
                                             FROM HoaDonDichVu h WITH (UPDLOCK) WHERE h.SoHDDV=@h", cn, tx);
                    q.Parameters.AddWithValue("@h", soHDDV);
                    decimal tong, da;
                    using (var rd = q.ExecuteReader())
                    {
                        if (!rd.Read()) { rd.Close(); tx.Rollback(); return KetQuaXuLy.Fail("Không tìm thấy hóa đơn dịch vụ."); }
                        tong = rd.GetDecimal(0); da = rd.GetDecimal(1);
                    }
                    if (soTien > tong - da)
                    { tx.Rollback(); return KetQuaXuLy.Fail("Số tiền vượt quá còn nợ của hóa đơn (" + (tong - da).ToString("N0") + ")."); }

                    var ins = new SqlCommand(@"INSERT INTO PhieuThu(SoPhieuThu,NgayThu,SoTien,HinhThuc,SoHDDV,MaNV)
                                               VALUES(@p,GETDATE(),@t,@h,@d,@n)", cn, tx);
                    ins.Parameters.AddWithValue("@p", soPhieuThu);
                    ins.Parameters.AddWithValue("@t", soTien);
                    ins.Parameters.AddWithValue("@h", hinhThuc);
                    ins.Parameters.AddWithValue("@d", soHDDV);
                    ins.Parameters.AddWithValue("@n", maNV);
                    ins.ExecuteNonQuery();

                    var up = new SqlCommand("UPDATE HoaDonDichVu SET TrangThai=@tt WHERE SoHDDV=@d", cn, tx);
                    up.Parameters.AddWithValue("@tt", TrangThaiTheoTien(da + soTien, tong));
                    up.Parameters.AddWithValue("@d", soHDDV);
                    up.ExecuteNonQuery();
                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã ghi nhận thu tiền dịch vụ.");
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
