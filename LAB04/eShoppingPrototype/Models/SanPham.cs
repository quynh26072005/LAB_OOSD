using System;

namespace eShoppingPrototype.Models
{
    public class SanPham
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int MaNhom { get; set; }
        public string NhaSanXuat { get; set; }
        public string MoTa { get; set; }
        public string ThongSoKyThuat { get; set; }
        public string HinhAnh { get; set; }
        public decimal GiaBan { get; set; }
        public string TinhTrang { get; set; }
        public DateTime NgayCapNhat { get; set; }

        // Navigation property
        public string TenNhom { get; set; }
    }
}
