using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    /// <summary>
    /// Service quản lý đầu sách
    /// </summary>
    public class SachService
    {
        public DataTable LayDanhSachSach()
        {
            string sql = @"SELECT ds.*, tl.TenTheLoai, nxb.TenNXB
                          FROM DauSach ds
                          LEFT JOIN TheLoai tl ON ds.MaTheLoai = tl.MaTheLoai
                          LEFT JOIN NhaXuatBan nxb ON ds.MaNhaXuatBan = nxb.MaNhaXuatBan
                          ORDER BY ds.TenSach";
            return Db.Query(sql);
        }

        public DataTable TimKiemSach(string tuKhoa)
        {
            string sql = @"SELECT ds.*, tl.TenTheLoai, nxb.TenNXB
                          FROM DauSach ds
                          LEFT JOIN TheLoai tl ON ds.MaTheLoai = tl.MaTheLoai
                          LEFT JOIN NhaXuatBan nxb ON ds.MaNhaXuatBan = nxb.MaNhaXuatBan
                          WHERE ds.TenSach LIKE @TuKhoa OR ds.MaDauSach LIKE @TuKhoa
                          ORDER BY ds.TenSach";
            return Db.Query(sql, new SqlParameter("@TuKhoa", $"%{tuKhoa}%"));
        }

        public DataTable LaySachConKho()
        {
            string sql = @"SELECT ds.*, tl.TenTheLoai, nxb.TenNXB
                          FROM DauSach ds
                          LEFT JOIN TheLoai tl ON ds.MaTheLoai = tl.MaTheLoai
                          LEFT JOIN NhaXuatBan nxb ON ds.MaNhaXuatBan = nxb.MaNhaXuatBan
                          WHERE ds.SoLuongHienCo > 0
                          ORDER BY ds.TenSach";
            return Db.Query(sql);
        }

        public KetQuaXuLy ThemSach(DauSach sach)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(sach.MaDauSach))
                    return KetQuaXuLy.Loi("Mã đầu sách không được để trống!");

                if (string.IsNullOrWhiteSpace(sach.TenSach))
                    return KetQuaXuLy.Loi("Tên sách không được để trống!");

                if (sach.SoLuongHienCo < 0)
                    return KetQuaXuLy.Loi("Số lượng không được âm!");

                // Kiểm tra mã đã tồn tại
                string checkSql = "SELECT COUNT(*) FROM DauSach WHERE MaDauSach = @Ma";
                int count = Convert.ToInt32(Db.ExecuteScalar(checkSql, new SqlParameter("@Ma", sach.MaDauSach)));
                if (count > 0)
                    return KetQuaXuLy.Loi("Mã đầu sách đã tồn tại!");

                string sql = @"INSERT INTO DauSach (MaDauSach, TenSach, NamXuatBan, SoLuongHienCo, MaTheLoai, MaNhaXuatBan)
                              VALUES (@Ma, @Ten, @Nam, @SoLuong, @MaTL, @MaNXB)";

                Db.Execute(sql,
                    new SqlParameter("@Ma", sach.MaDauSach),
                    new SqlParameter("@Ten", sach.TenSach),
                    new SqlParameter("@Nam", sach.NamXuatBan),
                    new SqlParameter("@SoLuong", sach.SoLuongHienCo),
                    new SqlParameter("@MaTL", sach.MaTheLoai),
                    new SqlParameter("@MaNXB", sach.MaNhaXuatBan)
                );

                return KetQuaXuLy.OK("Thêm sách thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy SuaSach(DauSach sach)
        {
            try
            {
                if (sach.SoLuongHienCo < 0)
                    return KetQuaXuLy.Loi("Số lượng không được âm!");

                string sql = @"UPDATE DauSach 
                              SET TenSach=@Ten, NamXuatBan=@Nam, SoLuongHienCo=@SoLuong, 
                                  MaTheLoai=@MaTL, MaNhaXuatBan=@MaNXB
                              WHERE MaDauSach=@Ma";

                Db.Execute(sql,
                    new SqlParameter("@Ma", sach.MaDauSach),
                    new SqlParameter("@Ten", sach.TenSach),
                    new SqlParameter("@Nam", sach.NamXuatBan),
                    new SqlParameter("@SoLuong", sach.SoLuongHienCo),
                    new SqlParameter("@MaTL", sach.MaTheLoai),
                    new SqlParameter("@MaNXB", sach.MaNhaXuatBan)
                );

                return KetQuaXuLy.OK("Cập nhật sách thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy XoaSach(string maSach)
        {
            try
            {
                // Kiểm tra xem sách có đang được mượn không
                string checkSql = @"SELECT COUNT(*) FROM ChiTietPhieuMuon 
                                   WHERE MaDauSach = @Ma AND NgayTraThucTe IS NULL";
                int count = Convert.ToInt32(Db.ExecuteScalar(checkSql, new SqlParameter("@Ma", maSach)));
                if (count > 0)
                    return KetQuaXuLy.Loi("Không thể xóa! Sách đang được mượn.");

                string sql = "DELETE FROM DauSach WHERE MaDauSach=@Ma";
                Db.Execute(sql, new SqlParameter("@Ma", maSach));
                return KetQuaXuLy.OK("Xóa sách thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public int LaySoLuongHienCo(string maSach)
        {
            string sql = "SELECT SoLuongHienCo FROM DauSach WHERE MaDauSach = @Ma";
            object result = Db.ExecuteScalar(sql, new SqlParameter("@Ma", maSach));
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}
