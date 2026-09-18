using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    /// <summary>
    /// Service quản lý danh mục: Nhân viên, Thể loại, NXB
    /// </summary>
    public class DanhMucService
    {
        #region Nhân viên

        public DataTable LayDanhSachNhanVien()
        {
            string sql = "SELECT * FROM NhanVien ORDER BY MaNhanVien";
            return Db.Query(sql);
        }

        public KetQuaXuLy ThemNhanVien(NhanVien nv)
        {
            try
            {
                // Kiểm tra mã đã tồn tại
                string checkSql = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @Ma";
                int count = Convert.ToInt32(Db.ExecuteScalar(checkSql, new SqlParameter("@Ma", nv.MaNhanVien)));
                if (count > 0)
                    return KetQuaXuLy.Loi("Mã nhân viên đã tồn tại!");

                string sql = @"INSERT INTO NhanVien (MaNhanVien, Ho, Ten, Phai, NgaySinh, ChucVu, SoDienThoai)
                              VALUES (@Ma, @Ho, @Ten, @Phai, @NgaySinh, @ChucVu, @SDT)";

                Db.Execute(sql,
                    new SqlParameter("@Ma", nv.MaNhanVien),
                    new SqlParameter("@Ho", nv.Ho),
                    new SqlParameter("@Ten", nv.Ten),
                    new SqlParameter("@Phai", nv.Phai),
                    new SqlParameter("@NgaySinh", nv.NgaySinh),
                    new SqlParameter("@ChucVu", nv.ChucVu),
                    new SqlParameter("@SDT", (object)nv.SoDienThoai ?? DBNull.Value)
                );

                return KetQuaXuLy.OK("Thêm nhân viên thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy SuaNhanVien(NhanVien nv)
        {
            try
            {
                string sql = @"UPDATE NhanVien 
                              SET Ho=@Ho, Ten=@Ten, Phai=@Phai, NgaySinh=@NgaySinh, 
                                  ChucVu=@ChucVu, SoDienThoai=@SDT
                              WHERE MaNhanVien=@Ma";

                Db.Execute(sql,
                    new SqlParameter("@Ma", nv.MaNhanVien),
                    new SqlParameter("@Ho", nv.Ho),
                    new SqlParameter("@Ten", nv.Ten),
                    new SqlParameter("@Phai", nv.Phai),
                    new SqlParameter("@NgaySinh", nv.NgaySinh),
                    new SqlParameter("@ChucVu", nv.ChucVu),
                    new SqlParameter("@SDT", (object)nv.SoDienThoai ?? DBNull.Value)
                );

                return KetQuaXuLy.OK("Cập nhật nhân viên thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy XoaNhanVien(string maNV)
        {
            try
            {
                string sql = "DELETE FROM NhanVien WHERE MaNhanVien=@Ma";
                Db.Execute(sql, new SqlParameter("@Ma", maNV));
                return KetQuaXuLy.OK("Xóa nhân viên thành công!");
            }
            catch (SqlException ex) when (ex.Number == 547) // FK violation
            {
                return KetQuaXuLy.Loi("Không thể xóa! Nhân viên đã lập phiếu mượn/phạt.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        #endregion

        #region Thể loại

        public DataTable LayDanhSachTheLoai()
        {
            string sql = "SELECT * FROM TheLoai ORDER BY TenTheLoai";
            return Db.Query(sql);
        }

        public KetQuaXuLy ThemTheLoai(TheLoai tl)
        {
            try
            {
                string checkSql = "SELECT COUNT(*) FROM TheLoai WHERE MaTheLoai = @Ma";
                int count = Convert.ToInt32(Db.ExecuteScalar(checkSql, new SqlParameter("@Ma", tl.MaTheLoai)));
                if (count > 0)
                    return KetQuaXuLy.Loi("Mã thể loại đã tồn tại!");

                string sql = "INSERT INTO TheLoai (MaTheLoai, TenTheLoai) VALUES (@Ma, @Ten)";
                Db.Execute(sql,
                    new SqlParameter("@Ma", tl.MaTheLoai),
                    new SqlParameter("@Ten", tl.TenTheLoai)
                );

                return KetQuaXuLy.OK("Thêm thể loại thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy SuaTheLoai(TheLoai tl)
        {
            try
            {
                string sql = "UPDATE TheLoai SET TenTheLoai=@Ten WHERE MaTheLoai=@Ma";
                Db.Execute(sql,
                    new SqlParameter("@Ma", tl.MaTheLoai),
                    new SqlParameter("@Ten", tl.TenTheLoai)
                );

                return KetQuaXuLy.OK("Cập nhật thể loại thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy XoaTheLoai(string maTL)
        {
            try
            {
                string sql = "DELETE FROM TheLoai WHERE MaTheLoai=@Ma";
                Db.Execute(sql, new SqlParameter("@Ma", maTL));
                return KetQuaXuLy.OK("Xóa thể loại thành công!");
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return KetQuaXuLy.Loi("Không thể xóa! Thể loại đang được sử dụng.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        #endregion

        #region Nhà xuất bản

        public DataTable LayDanhSachNXB()
        {
            string sql = "SELECT * FROM NhaXuatBan ORDER BY TenNXB";
            return Db.Query(sql);
        }

        public KetQuaXuLy ThemNXB(NhaXuatBan nxb)
        {
            try
            {
                string checkSql = "SELECT COUNT(*) FROM NhaXuatBan WHERE MaNhaXuatBan = @Ma";
                int count = Convert.ToInt32(Db.ExecuteScalar(checkSql, new SqlParameter("@Ma", nxb.MaNhaXuatBan)));
                if (count > 0)
                    return KetQuaXuLy.Loi("Mã NXB đã tồn tại!");

                string sql = @"INSERT INTO NhaXuatBan (MaNhaXuatBan, TenNXB, DiaChi, SoDienThoai) 
                              VALUES (@Ma, @Ten, @DiaChi, @SDT)";
                Db.Execute(sql,
                    new SqlParameter("@Ma", nxb.MaNhaXuatBan),
                    new SqlParameter("@Ten", nxb.TenNXB),
                    new SqlParameter("@DiaChi", (object)nxb.DiaChi ?? DBNull.Value),
                    new SqlParameter("@SDT", (object)nxb.SoDienThoai ?? DBNull.Value)
                );

                return KetQuaXuLy.OK("Thêm NXB thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy SuaNXB(NhaXuatBan nxb)
        {
            try
            {
                string sql = @"UPDATE NhaXuatBan 
                              SET TenNXB=@Ten, DiaChi=@DiaChi, SoDienThoai=@SDT 
                              WHERE MaNhaXuatBan=@Ma";
                Db.Execute(sql,
                    new SqlParameter("@Ma", nxb.MaNhaXuatBan),
                    new SqlParameter("@Ten", nxb.TenNXB),
                    new SqlParameter("@DiaChi", (object)nxb.DiaChi ?? DBNull.Value),
                    new SqlParameter("@SDT", (object)nxb.SoDienThoai ?? DBNull.Value)
                );

                return KetQuaXuLy.OK("Cập nhật NXB thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy XoaNXB(string maNXB)
        {
            try
            {
                string sql = "DELETE FROM NhaXuatBan WHERE MaNhaXuatBan=@Ma";
                Db.Execute(sql, new SqlParameter("@Ma", maNXB));
                return KetQuaXuLy.OK("Xóa NXB thành công!");
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return KetQuaXuLy.Loi("Không thể xóa! NXB đang được sử dụng.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        #endregion
    }
}
