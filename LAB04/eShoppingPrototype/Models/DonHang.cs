using System;
using System.Collections.Generic;

namespace eShoppingPrototype.Models
{
    public class DonHang
    {
        public int MaDonHang { get; set; }
        public int MaKH { get; set; }
        public int MaNguoiNhan { get; set; }
        public DateTime ThoiDiemDat { get; set; }

        // Shipping
        public string LoaiGiaoHang { get; set; }
        public decimal PhiGiaoHang { get; set; }

        // Payment
        public string LoaiThe { get; set; }
        public string So4SoCuoi { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string TenChuThe { get; set; }
        public decimal LePhi { get; set; }

        // Totals
        public decimal TongTriGiaSanPham { get; set; }
        public decimal TongTriGiaHoaDon { get; set; }

        // Status
        public string TrangThai { get; set; }

        // Navigation properties
        public NguoiNhan NguoiNhan { get; set; }
        public TheTinDung TheTinDung { get; set; }
        public List<ChiTietDonHang> ChiTietDonHang { get; set; }

        public DonHang()
        {
            ChiTietDonHang = new List<ChiTietDonHang>();
            ThoiDiemDat = DateTime.Now;
            TrangThai = "Pending";
        }
    }
}
