using System;

namespace QuanLyNhanSu.Data
{
    [Table("KeHoachPhanCong")]
    public class KeHoachPhanCong
    {
        [Key, Label("Mã kế hoạch")]
        public string MaKH { get; set; }

        [Label("Tuần")]
        public string Tuan { get; set; }

        [Column("MaPB"), Label("Mã phòng ban")]
        public string MaPB { get; set; }

        [Label("Người lập")]
        public string NguoiLap { get; set; }

        [Ignore]
        public string TenPB { get; set; }
    }

    [Table("ChiTietPhanCong")]
    public class ChiTietPhanCong
    {
        [Key, Column("MaKH"), Label("Mã kế hoạch")]
        public string MaKH { get; set; }

        [Key, Column("MaNV")]
        public string MaNV { get; set; }

        [Key, Label("Ngày")]
        public DateTime Ngay { get; set; }

        [Column("MaCa"), Label("Mã ca")]
        public string MaCa { get; set; }

        [Label("Có phụ trội")]
        public bool CoPhuTroi { get; set; }

        [Ignore]
        public string HoTen { get; set; }

        [Ignore]
        public string TenCa { get; set; }
    }

    [Table("BangChamCong")]
    public class BangChamCong
    {
        [Key, Label("Mã bảng chấm công")]
        public string MaCC { get; set; }

        [Label("Tháng")]
        public int Thang { get; set; }

        [Label("Năm")]
        public int Nam { get; set; }

        [Column("MaPB"), Label("Mã phòng ban")]
        public string MaPB { get; set; }

        [Ignore]
        public string TenPB { get; set; }
    }

    [Table("ChiTietChamCong")]
    public class ChiTietChamCong
    {
        [Key, Column("MaCC"), Label("Mã bảng chấm công")]
        public string MaCC { get; set; }

        [Key, Column("MaNV")]
        public string MaNV { get; set; }

        [Key, Label("Ngày")]
        public DateTime Ngay { get; set; }

        [Column("MaCa"), Label("Mã ca")]
        public string MaCa { get; set; }

        [Label("Trạng thái")]
        public string TrangThai { get; set; }

        [Ignore]
        public string HoTen { get; set; }

        [Ignore]
        public string TenCa { get; set; }
    }

    [Table("PhieuPhuTroi")]
    public class PhieuPhuTroi
    {
        [Key, Label("Số phiếu")]
        public string SoPhieu { get; set; }

        [Label("Ngày phụ trội")]
        public DateTime NgayPhuTroi { get; set; }

        [Label("Số giờ phụ trội")]
        public double SoGioPhuTroi { get; set; }

        [Label("Hình thức")]
        public string HinhThuc { get; set; }

        [Label("Hệ số")]
        public double HeSo { get; set; }
    }

    [Table("ChiTietPhuTroi")]
    public class ChiTietPhuTroi
    {
        [Key, Column("SoPhieu"), Label("Số phiếu")]
        public string SoPhieu { get; set; }

        [Key, Column("MaNV"), Label("Mã nhân viên")]
        public string MaNV { get; set; }

        [Label("Số giờ")]
        public double SoGio { get; set; }

        [Ignore]
        public string HoTen { get; set; }
    }
}