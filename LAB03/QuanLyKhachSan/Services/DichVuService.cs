using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLyKhachSan.BUS
{
    public class DichVuService
    {
        // Giả lập cơ sở dữ liệu tạm thời (hoặc bạn có thể gọi kết nối ADO.NET / Entity Framework tại đây)
        private static DataTable dtDichVuSuDung;

        public DichVuService()
        {
            if (dtDichVuSuDung == null)
            {
                KhoiTaoDuLieuBanDau();
            }
        }

        private void KhoiTaoDuLieuBanDau()
        {
            dtDichVuSuDung = new DataTable();
            dtDichVuSuDung.Columns.Add("SoPhieu", typeof(string));
            dtDichVuSuDung.Columns.Add("Phong", typeof(string));
            dtDichVuSuDung.Columns.Add("Ngay", typeof(string));
            dtDichVuSuDung.Columns.Add("DichVu", typeof(string));
            dtDichVuSuDung.Columns.Add("SoLuong", typeof(int));
            dtDichVuSuDung.Columns.Add("DonGia", typeof(decimal));
            dtDichVuSuDung.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");

            // Dữ liệu mẫu ban đầu
            dtDichVuSuDung.Rows.Add("DP001", "A101", "12/09/2026", "Ăn sáng", 2, 50000);
            dtDichVuSuDung.Rows.Add("DP001", "A101", "12/09/2026", "Giặt ủi", 1, 30000);
            dtDichVuSuDung.Rows.Add("DP002", "B201", "13/09/2026", "Nước tiệc / Bia", 4, 20000);
        }

        /// <summary>
        /// Lấy toàn bộ danh sách sử dụng dịch vụ
        /// </summary>
        public DataTable LayDanhSachSuDungDichVu()
        {
            return dtDichVuSuDung;
        }

        /// <summary>
        /// Ghi nhận/Thêm mới một lượt sử dụng dịch vụ
        /// </summary>
        public bool GhiNhanSuDungDichVu(string soPhieu, string phong, string ngay, string tenDichVu, int soLuong, decimal donGia)
        {
            try
            {
                dtDichVuSuDung.Rows.Add(soPhieu, phong, ngay, tenDichVu, soLuong, donGia);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy đơn giá dịch vụ theo tên dịch vụ
        /// </summary>
        public decimal LayDonGiaDichVu(string tenDichVu)
        {
            switch (tenDichVu.Trim().ToLower())
            {
                case "ăn sáng":
                    return 50000;
                case "giặt ủi":
                    return 30000;
                case "nước tiệc / bia":
                case "bia":
                    return 20000;
                default:
                    return 50000;
            }
        }
    }
}