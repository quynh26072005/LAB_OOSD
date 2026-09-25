using System.Data;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class DatPhongService
    {
        private DbContext db = new DbContext();

        public void TaoPhieuDat(string soPhieu, string maKhach, string maNV, string ngayNhan, string ngayTra, float tienCoc)
        {
            // Lưu ý: Cấu trúc câu lệnh này phụ thuộc vào bảng PhieuDat trong SQL của bạn
            string query = $"INSERT INTO PhieuDat (SoPhieu, MaKhach, MaNV, NgayNhan, NgayTra, TienCoc) " +
                           $"VALUES ('{soPhieu}', '{maKhach}', '{maNV}', '{ngayNhan}', '{ngayTra}', {tienCoc})";
            db.ExecuteQuery(query);
        }

        // Lấy danh sách khách hàng để đưa vào ComboBox (cboKhach)
        public DataTable LayDanhSachKhach()
        {
            return db.GetData("SELECT MaKhach, TenKhach FROM KhachHang");
        }
    }
}