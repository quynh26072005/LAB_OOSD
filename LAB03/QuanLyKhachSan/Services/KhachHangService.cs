using System.Data;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class KhachHangService
    {
        private DbContext db = new DbContext();

        public DataTable LayDanhSachKhachHang()
        {
            string query = "SELECT MaKhach, TenKhach, DienThoai FROM KhachHang";
            return db.GetData(query);
        }

        public void ThemKhachHang(string maKhach, string tenKhach, string dienThoai)
        {
            string query = $"INSERT INTO KhachHang (MaKhach, TenKhach, DienThoai) VALUES ('{maKhach}', N'{tenKhach}', '{dienThoai}')";
            db.ExecuteQuery(query);
        }
    }
}