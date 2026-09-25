using System.Data;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class DanhMucService
    {
        private DbContext db = new DbContext();

        public DataTable LayDanhSachKhuVuc()
        {
            string query = "SELECT MaKhuVuc, TenKhuVuc FROM KhuVuc";
            return db.GetData(query);
        }

        public void ThemKhuVuc(string maKhuVuc, string tenKhuVuc)
        {
            string query = $"INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc) VALUES ('{maKhuVuc}', N'{tenKhuVuc}')";
            db.ExecuteQuery(query);
        }
    }
}