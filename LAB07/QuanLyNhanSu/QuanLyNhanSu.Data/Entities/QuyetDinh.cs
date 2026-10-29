using System;

namespace QuanLyNhanSu.Data
{
    /// <summary>Các loại quyết định (bộ phân biệt trong bảng QuyetDinh).</summary>
    public static class LoaiQuyetDinh
    {
        public const string TuyenDung = "TuyenDung";
        public const string ChucVu = "ChucVu";
        public const string Luong = "Luong";
        public const string KhenThuong = "KhenThuong";
        public const string KyLuat = "KyLuat";
    }

    /// <summary>Lớp cha (abstract) của các loại quyết định - ánh xạ bảng QuyetDinh.</summary>
    [Table("QuyetDinh")]
    public class QuyetDinh
    {
        [Key, Label("Số quyết định")]
        public string SoQD { get; set; }

        [Label("Loại quyết định")]
        public string LoaiQD { get; set; }

        [Label("Ngày lập")]
        public DateTime NgayLap { get; set; }

        [Label("Ngày hiệu lực")]
        public DateTime? NgayHieuLuc { get; set; }

        [Label("Nội dung")]
        public string NoiDung { get; set; }

        [Label("Người ký")]
        public string NguoiKy { get; set; }
    }

    [Table("QD_TuyenDung")]
    public class QdTuyenDung
    {
        [Key, Label("Số quyết định")]
        public string SoQD { get; set; }

        [Label("Thời gian thử việc (tháng)")]
        public int ThoiGianThuViec { get; set; }

        [Label("Lương thử việc")]
        public decimal LuongThuViec { get; set; }

        [Label("Điều khoản")]
        public string DieuKhoan { get; set; }

        [Column("MaNV"), Label("Mã nhân viên")]
        public string MaNV { get; set; }

        [Column("MaPB"), Label("Mã phòng ban")]
        public string MaPB { get; set; }
    }

    [Table("QD_ChucVu")]
    public class QdChucVu
    {
        [Key, Label("Số quyết định")]
        public string SoQD { get; set; }

        [Column("MaNV"), Label("Mã nhân viên")]
        public string MaNV { get; set; }

        [Column("MaCV"), Label("Mã chức vụ")]
        public string MaCV { get; set; }

        [Label("Phụ cấp chức vụ")]
        public decimal PhuCapChucVu { get; set; }
    }

    [Table("QD_Luong")]
    public class QdLuong
    {
        [Key, Label("Số quyết định")]
        public string SoQD { get; set; }

        [Column("MaNV"), Label("Mã nhân viên")]
        public string MaNV { get; set; }

        [Label("Lương cơ bản")]
        public decimal LuongCoBan { get; set; }

        [Label("Phụ cấp lương")]
        public decimal PhuCapLuong { get; set; }

        [Label("Phụ cấp độc hại")]
        public decimal PhuCapDocHai { get; set; }
    }

    [Table("QD_KhenThuong")]
    public class QdKhenThuong
    {
        [Key, Label("Số quyết định")]
        public string SoQD { get; set; }

        [Label("Loại khen thưởng")]
        public string LoaiKhenThuong { get; set; }

        [Label("Số tiền")]
        public decimal SoTien { get; set; }

        [Label("Đối tượng")]
        public string DoiTuong { get; set; }
    }

    [Table("QD_KyLuat")]
    public class QdKyLuat
    {
        [Key, Label("Số quyết định")]
        public string SoQD { get; set; }

        [Label("Hình thức kỷ luật")]
        public string HinhThucKyLuat { get; set; }

        [Label("Số tiền phạt")]
        public decimal SoTienPhat { get; set; }

        [Label("Đối tượng")]
        public string DoiTuong { get; set; }
    }
}