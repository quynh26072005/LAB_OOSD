using System;
using System.Data;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    /// <summary>Báo cáo/thống kê theo kỳ, khoa, bác sĩ, doanh thu - dùng cho FrmThongKe.</summary>
    public class ThongKeService
    {
        public DataTable ThongKeLuotKhamTheoKhoa(DateTime tuNgay, DateTime denNgay)
        {
            const string sql = @"
                SELECT k.TenKhoa, COUNT(*) AS SoLuotKham,
                       SUM(CASE WHEN pk.TrangThai = N'HoanTat' THEN 1 ELSE 0 END) AS SoLuotHoanTat
                FROM PhieuKham pk
                JOIN Khoa k ON k.MaKhoa = pk.MaKhoa
                WHERE pk.ThoiGian >= @Tu AND pk.ThoiGian < DATEADD(DAY, 1, @Den)
                GROUP BY k.TenKhoa
                ORDER BY SoLuotKham DESC;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@Tu", tuNgay), DbHelper.P("@Den", denNgay));
        }

        public DataTable ThongKeLuotKhamTheoBacSi(DateTime tuNgay, DateTime denNgay)
        {
            const string sql = @"
                SELECT nv.HoTen AS BacSi, COUNT(*) AS SoLuotKham
                FROM PhieuKham pk
                JOIN NhanVien nv ON nv.MaNhanVien = pk.MaNhanVien
                WHERE pk.ThoiGian >= @Tu AND pk.ThoiGian < DATEADD(DAY, 1, @Den)
                GROUP BY nv.HoTen
                ORDER BY SoLuotKham DESC;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@Tu", tuNgay), DbHelper.P("@Den", denNgay));
        }

        public DataTable ThongKeDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            const string sql = @"
                SELECT CAST(tt.NgayThanhToan AS DATE) AS Ngay, SUM(tt.SoTien) AS DoanhThu, COUNT(*) AS SoGiaoDich
                FROM ThanhToan tt
                WHERE tt.NgayThanhToan >= @Tu AND tt.NgayThanhToan < DATEADD(DAY, 1, @Den)
                GROUP BY CAST(tt.NgayThanhToan AS DATE)
                ORDER BY Ngay;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@Tu", tuNgay), DbHelper.P("@Den", denNgay));
        }

        public DataRow ThongKeTongQuan(DateTime tuNgay, DateTime denNgay)
        {
            const string sql = @"
                SELECT
                    (SELECT COUNT(*) FROM BenhNhan WHERE NgayTao >= @Tu AND NgayTao < DATEADD(DAY,1,@Den)) AS BenhNhanMoi,
                    (SELECT COUNT(*) FROM PhieuKham WHERE ThoiGian >= @Tu AND ThoiGian < DATEADD(DAY,1,@Den)) AS TongLuotKham,
                    (SELECT COUNT(*) FROM PhieuKham WHERE TrangThai = N'HoanTat' AND ThoiGian >= @Tu AND ThoiGian < DATEADD(DAY,1,@Den)) AS LuotHoanTat,
                    (SELECT ISNULL(SUM(SoTien),0) FROM ThanhToan WHERE NgayThanhToan >= @Tu AND NgayThanhToan < DATEADD(DAY,1,@Den)) AS TongDoanhThu,
                    (SELECT ISNULL(SUM(SoTien),0) FROM LePhiNam WHERE NgayThu >= @Tu AND NgayThu < DATEADD(DAY,1,@Den)) AS TongLePhi;";
            return DbHelper.ExecuteQuerySingleRow(sql, DbHelper.P("@Tu", tuNgay), DbHelper.P("@Den", denNgay));
        }
    }
}
