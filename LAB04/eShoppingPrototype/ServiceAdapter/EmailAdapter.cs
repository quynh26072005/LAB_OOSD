using System;
using System.Text;
using eShoppingPrototype.Models;

namespace eShoppingPrototype.ServiceAdapter
{
    public class EmailAdapter
    {
        public bool SendOrderConfirmationEmail(string recipientEmail, DonHang donHang)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail))
                return false;

            System.Threading.Thread.Sleep(300);

            Console.WriteLine($"[EMAIL SERVICE] Sending confirmation to: {recipientEmail}");
            Console.WriteLine(BuildEmailContent(donHang));

            return true;
        }

        private string BuildEmailContent(DonHang donHang)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("========================================");
            sb.AppendLine("ORDER CONFIRMATION - e-SHOPPING");
            sb.AppendLine("========================================");
            sb.AppendLine($"Order ID: {donHang.MaDonHang}");
            sb.AppendLine($"Date: {donHang.ThoiDiemDat:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine();
            sb.AppendLine("RECIPIENT INFORMATION:");
            sb.AppendLine($"Name: {donHang.NguoiNhan.HoTen}");
            sb.AppendLine($"Address: {donHang.NguoiNhan.DiaChi}");
            sb.AppendLine($"Phone: {donHang.NguoiNhan.DienThoai}");
            sb.AppendLine();
            sb.AppendLine("ORDER DETAILS:");
            
            foreach (var item in donHang.ChiTietDonHang)
            {
                sb.AppendLine($"- {item.TenSanPham} x{item.SoLuong} = {item.ThanhTien:N0} VND");
            }
            
            sb.AppendLine();
            sb.AppendLine($"Subtotal: {donHang.TongTriGiaSanPham:N0} VND");
            sb.AppendLine($"Shipping ({donHang.LoaiGiaoHang}): {donHang.PhiGiaoHang:N0} VND");
            sb.AppendLine($"TOTAL: {donHang.TongTriGiaHoaDon:N0} VND");
            sb.AppendLine("========================================");

            return sb.ToString();
        }
    }
}
