using System;
using System.Data;

namespace QuanLyKhachSan.Services
{
    public class TraPhongService
    {
        // 1. Lấy thông tin phòng đang ở theo mã phiếu lưu trú/đặt
        public DataTable GetThongTinPhong(string maPhieu)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Phong", typeof(string));
            dt.Columns.Add("DonGia", typeof(decimal));

            // Mẫu dữ liệu giả lập (Thực tế sẽ execute query SQL: SELECT TenPhong, DonGia FROM ...)
            if (!string.IsNullOrEmpty(maPhieu))
            {
                dt.Rows.Add("P101", 500000);
            }
            return dt;
        }

        // 2. Lấy danh sách tiện nghi của phòng
        public DataTable GetTienNghiPhong(string tenPhong)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TienNghi", typeof(string));
            dt.Columns.Add("Loai", typeof(string));
            dt.Columns.Add("TinhTrang", typeof(string));

            dt.Rows.Add("Tivi", "Điện tử", "Tốt");
            dt.Rows.Add("Tủ lạnh", "Điện lạnh", "Tốt");
            dt.Rows.Add("Ly thủy tinh", "Đồ dùng", "Bình thường");

            return dt;
        }

        // 3. Lấy danh sách tiện nghi cần đền bù
        public DataTable GetTienNghiDenBu(string maPhieu)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TienNghiDB", typeof(string));
            dt.Columns.Add("MucDo", typeof(string));
            dt.Columns.Add("SoTien", typeof(decimal));

            return dt;
        }

        // 4. Lập phiếu đền bù
        public bool LapPhieuDenBu(string maPhieuDenBu, string maPhieuDat, string mucDo, decimal soTien)
        {
            // Thực hiện INSERT vào bảng PhieuDenBu trong Database
            return true;
        }

        // 5. Lập hóa đơn thanh toán
        public DataTable LapHoaDon(string maHoaDon, string maPhieuDat, int soNgay, decimal tienDenBu)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("HoaDon", typeof(string));
            dt.Columns.Add("PhieuDat", typeof(string));
            dt.Columns.Add("TienPhong", typeof(decimal));
            dt.Columns.Add("TienDichVu", typeof(decimal));
            dt.Columns.Add("TongTien", typeof(decimal));
            dt.Columns.Add("TrangThai", typeof(string));

            decimal tienPhong = soNgay * 500000;
            decimal tienDichVu = 200000;
            decimal tongTien = tienPhong + tienDichVu + tienDenBu;

            dt.Rows.Add(maHoaDon, maPhieuDat, tienPhong, tienDichVu, tongTien, "Chưa thanh toán");
            return dt;
        }

        // 6. Xử lý thanh toán & Hoàn tất trả phòng
        public bool ThanhToanVagHoanTat(string maHoaDon, string hinhThuc, decimal soTienThanhToan)
        {
            // Update trạng thái Hóa đơn -> "Đã thanh toán"
            // Update trạng thái Phòng -> "Trống"
            return true;
        }
    }
}