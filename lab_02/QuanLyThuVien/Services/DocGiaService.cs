using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    /// <summary>
    /// Service quản lý độc giả và thẻ
    /// </summary>
    public class DocGiaService
    {
        #region Độc giả

        public DataTable LayDanhSachDocGia()
        {
            string sql = "SELECT * FROM DocGia ORDER BY Ho, Ten";
            return Db.Query(sql);
        }

        public DataTable TimKiemDocGia(string tuKhoa)
        {
            string sql = @"SELECT * FROM DocGia 
                          WHERE MaDocGia LIKE @TuKhoa 
                             OR Ho LIKE @TuKhoa 
                             OR Ten LIKE @TuKhoa
                             OR Email LIKE @TuKhoa
                          ORDER BY Ho, Ten";
            return Db.Query(sql, new SqlParameter("@TuKhoa", $"%{tuKhoa}%"));
        }

        public KetQuaXuLy ThemDocGia(DocGia dg)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(dg.MaDocGia))
                    return KetQuaXuLy.Loi("Mã độc giả không được để trống!");

                if (string.IsNullOrWhiteSpace(dg.Ho) || string.IsNullOrWhiteSpace(dg.Ten))
                    return KetQuaXuLy.Loi("Họ tên không được để trống!");

                // Kiểm tra mã đã tồn tại
                string checkSql = "SELECT COUNT(*) FROM DocGia WHERE MaDocGia = @Ma";
                int count = Convert.ToInt32(Db.ExecuteScalar(checkSql, new SqlParameter("@Ma", dg.MaDocGia)));
                if (count > 0)
                    return KetQuaXuLy.Loi("Mã độc giả đã tồn tại!");

                string sql = @"INSERT INTO DocGia (MaDocGia, Ho, Ten, Phai, NgaySinh, SoDienThoai, DiaChi, Email, Anh3x4)
                              VALUES (@Ma, @Ho, @Ten, @Phai, @NgaySinh, @SDT, @DiaChi, @Email, @Anh)";

                Db.Execute(sql,
                    new SqlParameter("@Ma", dg.MaDocGia),
                    new SqlParameter("@Ho", dg.Ho),
                    new SqlParameter("@Ten", dg.Ten),
                    new SqlParameter("@Phai", dg.Phai),
                    new SqlParameter("@NgaySinh", dg.NgaySinh),
                    new SqlParameter("@SDT", (object)dg.SoDienThoai ?? DBNull.Value),
                    new SqlParameter("@DiaChi", (object)dg.DiaChi ?? DBNull.Value),
                    new SqlParameter("@Email", (object)dg.Email ?? DBNull.Value),
                    new SqlParameter("@Anh", (object)dg.Anh3x4 ?? DBNull.Value)
                );

                return KetQuaXuLy.OK("Thêm độc giả thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy SuaDocGia(DocGia dg)
        {
            try
            {
                string sql = @"UPDATE DocGia 
                              SET Ho=@Ho, Ten=@Ten, Phai=@Phai, NgaySinh=@NgaySinh, 
                                  SoDienThoai=@SDT, DiaChi=@DiaChi, Email=@Email, Anh3x4=@Anh
                              WHERE MaDocGia=@Ma";

                Db.Execute(sql,
                    new SqlParameter("@Ma", dg.MaDocGia),
                    new SqlParameter("@Ho", dg.Ho),
                    new SqlParameter("@Ten", dg.Ten),
                    new SqlParameter("@Phai", dg.Phai),
                    new SqlParameter("@NgaySinh", dg.NgaySinh),
                    new SqlParameter("@SDT", (object)dg.SoDienThoai ?? DBNull.Value),
                    new SqlParameter("@DiaChi", (object)dg.DiaChi ?? DBNull.Value),
                    new SqlParameter("@Email", (object)dg.Email ?? DBNull.Value),
                    new SqlParameter("@Anh", (object)dg.Anh3x4 ?? DBNull.Value)
                );

                return KetQuaXuLy.OK("Cập nhật độc giả thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy XoaDocGia(string maDG)
        {
            try
            {
                // Kiểm tra xem độc giả có phiếu mượn không
                string checkSql = "SELECT COUNT(*) FROM PhieuMuon WHERE MaDocGia = @Ma";
                int count = Convert.ToInt32(Db.ExecuteScalar(checkSql, new SqlParameter("@Ma", maDG)));
                if (count > 0)
                    return KetQuaXuLy.Loi("Không thể xóa! Độc giả đã có phiếu mượn.");

                string sql = "DELETE FROM DocGia WHERE MaDocGia=@Ma";
                Db.Execute(sql, new SqlParameter("@Ma", maDG));
                return KetQuaXuLy.OK("Xóa độc giả thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        #endregion

        #region Thẻ độc giả

        public DataTable LayDanhSachThe()
        {
            string sql = @"SELECT t.*, dg.Ho, dg.Ten 
                          FROM TheDocGia t
                          JOIN DocGia dg ON t.MaDocGia = dg.MaDocGia
                          ORDER BY t.NgayCap DESC";
            return Db.Query(sql);
        }

        public TheDocGia LayTheHopLe(string maDocGia)
        {
            string sql = @"SELECT * FROM TheDocGia 
                          WHERE MaDocGia = @Ma AND TrangThai = 1";
            DataTable dt = Db.Query(sql, new SqlParameter("@Ma", maDocGia));

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new TheDocGia
            {
                MaThe = row["MaThe"].ToString(),
                MaDocGia = row["MaDocGia"].ToString(),
                NgayCap = Convert.ToDateTime(row["NgayCap"]),
                HanSuDung = Convert.ToDateTime(row["HanSuDung"]),
                DaDongLePhi = Convert.ToBoolean(row["DaDongLePhi"]),
                TrangThai = Convert.ToInt32(row["TrangThai"])
            };
        }

        public KetQuaXuLy CapThe(string maDocGia, DateTime ngayCap, DateTime hanSuDung, bool dadongLePhi)
        {
            try
            {
                // Kiểm tra độc giả tồn tại
                string checkDG = "SELECT COUNT(*) FROM DocGia WHERE MaDocGia = @Ma";
                int count = Convert.ToInt32(Db.ExecuteScalar(checkDG, new SqlParameter("@Ma", maDocGia)));
                if (count == 0)
                    return KetQuaXuLy.Loi("Độc giả không tồn tại!");

                // Kiểm tra thẻ hiện tại còn hiệu lực không (BR02)
                TheDocGia theHienTai = LayTheHopLe(maDocGia);
                if (theHienTai != null && theHienTai.ConHieuLuc)
                {
                    return KetQuaXuLy.Loi("Độc giả đã có thẻ còn hiệu lực! Không thể cấp thẻ mới.");
                }

                // Vô hiệu hóa các thẻ cũ
                string updateOld = "UPDATE TheDocGia SET TrangThai = 0 WHERE MaDocGia = @Ma";
                Db.Execute(updateOld, new SqlParameter("@Ma", maDocGia));

                // Tạo mã thẻ mới
                string maThe = "THE" + DateTime.Now.ToString("yyMMddHHmmss");

                // Thêm thẻ mới
                string sql = @"INSERT INTO TheDocGia (MaThe, MaDocGia, NgayCap, HanSuDung, DaDongLePhi, TrangThai)
                              VALUES (@MaThe, @MaDG, @NgayCap, @HanSD, @LePhi, 1)";

                Db.Execute(sql,
                    new SqlParameter("@MaThe", maThe),
                    new SqlParameter("@MaDG", maDocGia),
                    new SqlParameter("@NgayCap", ngayCap),
                    new SqlParameter("@HanSD", hanSuDung),
                    new SqlParameter("@LePhi", dadongLePhi)
                );

                return KetQuaXuLy.OK($"Cấp thẻ thành công! Mã thẻ: {maThe}");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        public KetQuaXuLy GiaHanThe(string maThe, DateTime hanMoi, bool dongLePhi)
        {
            try
            {
                string sql = @"UPDATE TheDocGia 
                              SET HanSuDung = @Han, DaDongLePhi = @LePhi
                              WHERE MaThe = @Ma";

                Db.Execute(sql,
                    new SqlParameter("@Ma", maThe),
                    new SqlParameter("@Han", hanMoi),
                    new SqlParameter("@LePhi", dongLePhi)
                );

                return KetQuaXuLy.OK("Gia hạn thẻ thành công!");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
            }
        }

        #endregion
    }
}
