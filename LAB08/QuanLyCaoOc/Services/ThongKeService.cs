using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Services
{
    public class ThongKeService
    {
        public DataTable TongHop(DateTime tu, DateTime den)
        {
            return Db.Query(@"SELECT
                (SELECT COUNT(*) FROM HopDong WHERE NgayLap BETWEEN @tu AND @den) AS SoHopDongMoi,
                (SELECT COUNT(*) FROM PhieuGiaHan WHERE NgayLap BETWEEN @tu AND @den) AS SoPhieuGiaHan,
                (SELECT ISNULL(SUM(SoTien),0) FROM PhieuThu WHERE MaKy IS NOT NULL
                    AND CAST(NgayThu AS date) BETWEEN @tu AND @den) AS ThuTienThue,
                (SELECT ISNULL(SUM(SoTien),0) FROM PhieuThu WHERE SoHDDV IS NOT NULL
                    AND CAST(NgayThu AS date) BETWEEN @tu AND @den) AS ThuDichVu,
                (SELECT ISNULL(SUM(SoTien),0) FROM BienBanPhat WHERE TrangThai=N'Đã nộp'
                    AND NgayNop BETWEEN @tu AND @den) AS ThuTienPhat",
                new SqlParameter("@tu", tu.Date), new SqlParameter("@den", den.Date));
        }

        public DataTable DoanhThuTheoThang(DateTime tu, DateTime den)
        {
            return Db.Query(@"SELECT YEAR(NgayThu) AS Nam, MONTH(NgayThu) AS Thang,
                                     SUM(CASE WHEN MaKy IS NOT NULL THEN SoTien ELSE 0 END) AS TienThue,
                                     SUM(CASE WHEN SoHDDV IS NOT NULL THEN SoTien ELSE 0 END) AS DichVu,
                                     SUM(SoTien) AS Tong
                              FROM PhieuThu WHERE CAST(NgayThu AS date) BETWEEN @tu AND @den
                              GROUP BY YEAR(NgayThu), MONTH(NgayThu) ORDER BY Nam, Thang",
                new SqlParameter("@tu", tu.Date), new SqlParameter("@den", den.Date));
        }

        public DataTable CongNo()
        {
            return Db.Query(@"SELECT h.SoHopDong, k.TenKhach, kt.MaPhong,
                                     SUM(kt.SoTien-ISNULL(pt.DaThu,0)) AS NoTienThue
                              FROM KyThanhToan kt
                              JOIN HopDong h ON kt.SoHopDong=h.SoHopDong
                              JOIN KhachHang k ON h.MaKhach=k.MaKhach
                              LEFT JOIN (SELECT MaKy, SUM(SoTien) AS DaThu FROM PhieuThu
                                         WHERE MaKy IS NOT NULL GROUP BY MaKy) pt ON pt.MaKy=kt.MaKy
                              WHERE kt.HanNop<=CAST(GETDATE() AS date) AND kt.SoTien>ISNULL(pt.DaThu,0)
                              GROUP BY h.SoHopDong, k.TenKhach, kt.MaPhong
                              ORDER BY h.SoHopDong, kt.MaPhong");
        }

        public DataTable TinhTrangPhong()
        {
            return Db.Query(@"SELECT t.TenTang,
                                     COUNT(*) AS TongPhong,
                                     SUM(CASE WHEN EXISTS(SELECT 1 FROM ChiTietHopDong c WHERE c.MaPhong=p.MaPhong
                                          AND c.NgayBatDau<=CAST(GETDATE() AS date)
                                          AND c.NgayKetThuc>=CAST(GETDATE() AS date)) THEN 1 ELSE 0 END) AS DangThue,
                                     SUM(p.DienTich) AS TongDienTich
                              FROM Phong p JOIN Tang t ON p.MaTang=t.MaTang
                              GROUP BY t.TenTang, t.SoTang ORDER BY t.SoTang");
        }
    }
}
