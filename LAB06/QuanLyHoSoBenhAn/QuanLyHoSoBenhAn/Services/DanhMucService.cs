using System;
using System.Data;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    /// <summary>
    /// CRUD cho các danh mục dùng chung: Khoa, NhanVien/BacSi, Thuoc, XetNghiem.
    /// Tương ứng FrmDanhMuc trong mục 3.7-3.9 kiến trúc WinForms.
    /// </summary>
    public class DanhMucService
    {
        private readonly AuthService _auth = new AuthService();

        /* ---------- Khoa ---------- */

        public DataTable LayDanhSachKhoa() => DbHelper.ExecuteQuery("SELECT * FROM Khoa ORDER BY TenKhoa;");

        public void ThemKhoa(string tenKhoa, string dienThoai, string viTri)
        {
            DbHelper.ExecuteNonQuery(
                "INSERT INTO Khoa (TenKhoa, DienThoai, ViTri) VALUES (@Ten, @DT, @VT);",
                DbHelper.P("@Ten", tenKhoa), DbHelper.P("@DT", dienThoai), DbHelper.P("@VT", viTri));
        }

        public void CapNhatKhoa(int maKhoa, string tenKhoa, string dienThoai, string viTri)
        {
            DbHelper.ExecuteNonQuery(
                "UPDATE Khoa SET TenKhoa=@Ten, DienThoai=@DT, ViTri=@VT WHERE MaKhoa=@Ma;",
                DbHelper.P("@Ten", tenKhoa), DbHelper.P("@DT", dienThoai), DbHelper.P("@VT", viTri), DbHelper.P("@Ma", maKhoa));
        }

        public void XoaKhoa(int maKhoa) =>
            DbHelper.ExecuteNonQuery("DELETE FROM Khoa WHERE MaKhoa=@Ma;", DbHelper.P("@Ma", maKhoa));

        /* ---------- Xét nghiệm (danh mục) ---------- */

        public DataTable LayDanhSachXetNghiem() => DbHelper.ExecuteQuery("SELECT * FROM XetNghiem ORDER BY TenXetNghiem;");

        public void ThemXetNghiem(string ten, string donVi, string moTa, decimal donGia)
        {
            DbHelper.ExecuteNonQuery(
                "INSERT INTO XetNghiem (TenXetNghiem, DonVi, MoTa, DonGia) VALUES (@Ten, @DV, @MT, @Gia);",
                DbHelper.P("@Ten", ten), DbHelper.P("@DV", donVi), DbHelper.P("@MT", moTa), DbHelper.P("@Gia", donGia));
        }

        public void CapNhatXetNghiem(int ma, string ten, string donVi, string moTa, decimal donGia)
        {
            DbHelper.ExecuteNonQuery(
                "UPDATE XetNghiem SET TenXetNghiem=@Ten, DonVi=@DV, MoTa=@MT, DonGia=@Gia WHERE MaXetNghiem=@Ma;",
                DbHelper.P("@Ten", ten), DbHelper.P("@DV", donVi), DbHelper.P("@MT", moTa),
                DbHelper.P("@Gia", donGia), DbHelper.P("@Ma", ma));
        }

        /* ---------- Thuốc (danh mục) ---------- */

        public DataTable LayDanhSachThuoc() => DbHelper.ExecuteQuery("SELECT * FROM Thuoc ORDER BY TenThuoc;");

        public void ThemThuoc(string ten, string hamLuong, string donViTinh, decimal donGia)
        {
            DbHelper.ExecuteNonQuery(
                "INSERT INTO Thuoc (TenThuoc, HamLuong, DonViTinh, DonGia) VALUES (@Ten, @HL, @DVT, @Gia);",
                DbHelper.P("@Ten", ten), DbHelper.P("@HL", hamLuong), DbHelper.P("@DVT", donViTinh), DbHelper.P("@Gia", donGia));
        }

        public void CapNhatThuoc(int ma, string ten, string hamLuong, string donViTinh, decimal donGia, string trangThai)
        {
            DbHelper.ExecuteNonQuery(
                "UPDATE Thuoc SET TenThuoc=@Ten, HamLuong=@HL, DonViTinh=@DVT, DonGia=@Gia, TrangThai=@TT WHERE MaThuoc=@Ma;",
                DbHelper.P("@Ten", ten), DbHelper.P("@HL", hamLuong), DbHelper.P("@DVT", donViTinh),
                DbHelper.P("@Gia", donGia), DbHelper.P("@TT", trangThai), DbHelper.P("@Ma", ma));
        }

        /* ---------- Nhân viên / Bác sĩ / Tài khoản ---------- */

        public DataTable LayDanhSachNhanVien() => DbHelper.ExecuteQuery(@"
            SELECT nv.MaNhanVien, nv.HoTen, nv.DienThoai, nv.Email, nv.TenDangNhap, nv.VaiTro, nv.TrangThai,
                   bs.SoChungChi, bs.ChuyenMon
            FROM NhanVien nv
            LEFT JOIN BacSi bs ON bs.MaNhanVien = nv.MaNhanVien
            ORDER BY nv.MaNhanVien;");

        public DataTable LayDanhSachBacSi() => DbHelper.ExecuteQuery(@"
            SELECT bs.MaNhanVien, nv.HoTen, bs.SoChungChi, bs.ChuyenMon
            FROM BacSi bs JOIN NhanVien nv ON nv.MaNhanVien = bs.MaNhanVien
            WHERE nv.TrangThai = N'DangLamViec'
            ORDER BY nv.HoTen;");

        /// <summary>Tạo tài khoản nhân viên mới; nếu vaiTro = BacSi thì tạo thêm dòng chuyên biệt BacSi.</summary>
        public int ThemNhanVien(string hoTen, string dienThoai, string email, string tenDangNhap,
            string matKhau, string vaiTro, string soChungChi, string chuyenMon, int? nguoiThucHien)
        {
            object id = DbHelper.ExecuteScalar(@"
                INSERT INTO NhanVien (HoTen, DienThoai, Email, TenDangNhap, MatKhauHash, VaiTro)
                VALUES (@HoTen, @DT, @Email, @TDN, @Hash, @VaiTro);
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                DbHelper.P("@HoTen", hoTen), DbHelper.P("@DT", dienThoai), DbHelper.P("@Email", email),
                DbHelper.P("@TDN", tenDangNhap), DbHelper.P("@Hash", DbHelper.HashPassword(matKhau)), DbHelper.P("@VaiTro", vaiTro));
            int maNV = Convert.ToInt32(id);

            if (string.Equals(vaiTro, "BacSi", StringComparison.OrdinalIgnoreCase))
            {
                DbHelper.ExecuteNonQuery(
                    "INSERT INTO BacSi (MaNhanVien, SoChungChi, ChuyenMon) VALUES (@Ma, @SoCC, @CM);",
                    DbHelper.P("@Ma", maNV), DbHelper.P("@SoCC", soChungChi), DbHelper.P("@CM", chuyenMon));
            }

            _auth.GhiNhatKy(nguoiThucHien, "ThemNhanVien", "NhanVien", maNV);
            return maNV;
        }

        public void DoiTrangThaiNhanVien(int maNhanVien, string trangThai, int? nguoiThucHien)
        {
            DbHelper.ExecuteNonQuery(
                "UPDATE NhanVien SET TrangThai=@TT WHERE MaNhanVien=@Ma;",
                DbHelper.P("@TT", trangThai), DbHelper.P("@Ma", maNhanVien));
            _auth.GhiNhatKy(nguoiThucHien, "DoiTrangThaiNhanVien:" + trangThai, "NhanVien", maNhanVien);
        }

        public void DatLaiMatKhau(int maNhanVien, string matKhauMoi, int? nguoiThucHien)
        {
            DbHelper.ExecuteNonQuery(
                "UPDATE NhanVien SET MatKhauHash=@Hash WHERE MaNhanVien=@Ma;",
                DbHelper.P("@Hash", DbHelper.HashPassword(matKhauMoi)), DbHelper.P("@Ma", maNhanVien));
            _auth.GhiNhatKy(nguoiThucHien, "DatLaiMatKhau", "NhanVien", maNhanVien);
        }
    }
}
