using System.Data;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class PhongService
    {
        private DbContext db = new DbContext();

        public DataTable LayDanhSachPhong()
        {
            string query = "SELECT SoPhong, MaKhuVuc, SucChua, DonGia FROM Phong";
            return db.GetData(query);
        }

        public void ThemPhong(string soPhong, string maKhuVuc, int sucChua, float donGia)
        {
            string query = $"INSERT INTO Phong (SoPhong, MaKhuVuc, SucChua, DonGia) VALUES ('{soPhong}', '{maKhuVuc}', {sucChua}, {donGia})";
            db.ExecuteQuery(query);
        }
    }
}