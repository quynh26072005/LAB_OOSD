using System;

namespace eShoppingPrototype.Models
{
    public class GioHangItem
    {
        public SanPham SanPham { get; set; }
        public int SoLuong { get; set; }
        
        public decimal ThanhTien
        {
            get { return SanPham.GiaBan * SoLuong; }
        }
    }
}
