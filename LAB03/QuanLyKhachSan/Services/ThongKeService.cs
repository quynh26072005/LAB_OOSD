using System;
using System.Data;

namespace QuanLyKhachSan.Services
{
    public class ThongKeService
    {
        // 1. Lấy thông tin tổng quan các chỉ số thống kê
        public ThongKeModel GetThongKeTongQuan(DateTime tuNgay, DateTime denNgay)
        {
            // Thực tế sẽ dùng SQL query:
            // SELECT 
            //   (SELECT COUNT(*) FROM PhieuDat WHERE NgayDat BETWEEN @TuNgay AND @DenNgay) AS PhieuDat,
            //   (SELECT COUNT(*) FROM PhieuDat WHERE TrangThai = N'Đang ở') AS DangO,
            //   (SELECT COUNT(*) FROM HoaDon WHERE NgayLap BETWEEN @TuNgay AND @DenNgay) AS HoaDon,
            //   (SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE NgayLap BETWEEN @TuNgay AND @DenNgay) AS DoanhThu,
            //   (SELECT ISNULL(SUM(SoTien), 0) FROM PhieuDenBu WHERE NgayLap BETWEEN @TuNgay AND @DenNgay) AS TongDenBu

            return new ThongKeModel
            {
                SoPhieuDat = 28,
                SoDangO = 7,
                SoHoaDon = 21,
                DoanhThuHD = 52600000,
                TongDenBu = 2100000
            };
        }

        // 2. Lấy danh sách tổng hợp dịch vụ được sử dụng
        public DataTable GetDichVuSuDung(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaDV", typeof(string));
            dt.Columns.Add("TenDichVu", typeof(string));
            dt.Columns.Add("TongSoLuong", typeof(int));
            dt.Columns.Add("TongTien", typeof(decimal));

            // Dữ liệu mẫu (sẽ thay bằng SQL SELECT với GROUP BY dịch vụ)
            dt.Rows.Add("DV01", "Nước suối", 45, 450000);
            dt.Rows.Add("DV02", "Giặt ủi", 12, 600000);
            dt.Rows.Add("DV03", "Ăn sáng", 20, 1000000);
            dt.Rows.Add("DV04", "Cho thuê xe máy", 5, 750000);

            return dt;
        }
    }

    public class ThongKeModel
    {
        public int SoPhieuDat { get; set; }
        public int SoDangO { get; set; }
        public int SoHoaDon { get; set; }
        public decimal DoanhThuHD { get; set; }
        public decimal TongDenBu { get; set; }
    }
}