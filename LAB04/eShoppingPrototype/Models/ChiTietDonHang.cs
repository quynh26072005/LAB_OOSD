namespace eShoppingPrototype.Models
{
    public class ChiTietDonHang
    {
        public int MaChiTiet { get; set; }
        public int MaDonHang { get; set; }
        public int MaSP { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
