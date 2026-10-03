namespace eShoppingPrototype.Models
{
    public class NguoiNhan
    {
        public int MaNguoiNhan { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public int MaKhuVuc { get; set; }
        
        // Navigation property
        public string TenKhuVuc { get; set; }
    }
}
