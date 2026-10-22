using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Models;

namespace QuanLyHoSoBenhAn.Services
{
    /// <summary>
    /// Tương ứng NhanVien.DangNhap()/KiemTraQuyen() và NhatKyTruyCap.GhiNhatKy()
    /// trong biểu đồ lớp phân tích (mục 2.10).
    /// </summary>
    public class AuthService
    {
        /// <summary>Đăng nhập; trả về null nếu sai tài khoản/mật khẩu hoặc tài khoản đã nghỉ việc.</summary>
        public NhanVien DangNhap(string tenDangNhap, string matKhau)
        {
            string hash = DbHelper.HashPassword(matKhau);
            const string sql = @"
                SELECT MaNhanVien, HoTen, DienThoai, Email, TenDangNhap, VaiTro, TrangThai
                FROM NhanVien
                WHERE TenDangNhap = @TenDangNhap AND MatKhauHash = @Hash AND TrangThai = N'DangLamViec';";

            DataRow row = DbHelper.ExecuteQuerySingleRow(sql,
                DbHelper.P("@TenDangNhap", tenDangNhap),
                DbHelper.P("@Hash", hash));

            if (row == null) return null;

            var nv = Map(row);
            GhiNhatKy(nv.MaNhanVien, "DangNhap", "NhanVien", nv.MaNhanVien);
            return nv;
        }

        public void GhiNhatKy(int? maNhanVien, string hanhDong, string doiTuong, int? maDoiTuong)
        {
            const string sql = @"
                INSERT INTO NhatKyTruyCap (MaNhanVien, HanhDong, DoiTuong, MaDoiTuong)
                VALUES (@MaNhanVien, @HanhDong, @DoiTuong, @MaDoiTuong);";
            DbHelper.ExecuteNonQuery(sql,
                DbHelper.P("@MaNhanVien", maNhanVien),
                DbHelper.P("@HanhDong", hanhDong),
                DbHelper.P("@DoiTuong", doiTuong),
                DbHelper.P("@MaDoiTuong", maDoiTuong));
        }

        public DataTable LayNhatKy(DateTime? tuNgay, DateTime? denNgay)
        {
            const string sql = @"
                SELECT nk.MaNhatKy, nk.ThoiGian, ISNULL(nv.HoTen, N'(hệ thống)') AS NhanVien,
                       nk.HanhDong, nk.DoiTuong, nk.MaDoiTuong
                FROM NhatKyTruyCap nk
                LEFT JOIN NhanVien nv ON nv.MaNhanVien = nk.MaNhanVien
                WHERE (@Tu IS NULL OR nk.ThoiGian >= @Tu)
                  AND (@Den IS NULL OR nk.ThoiGian < DATEADD(DAY, 1, @Den))
                ORDER BY nk.ThoiGian DESC;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@Tu", tuNgay), DbHelper.P("@Den", denNgay));
        }

        private static NhanVien Map(DataRow row)
        {
            return new NhanVien
            {
                MaNhanVien = Convert.ToInt32(row["MaNhanVien"]),
                HoTen = row["HoTen"].ToString(),
                DienThoai = row["DienThoai"] as string,
                Email = row["Email"] as string,
                TenDangNhap = row["TenDangNhap"].ToString(),
                VaiTro = row["VaiTro"].ToString(),
                TrangThai = row["TrangThai"].ToString()
            };
        }
    }
}
