using System;

namespace QuanLyNhanSu.Data
{
    [Table("PhongBan")]
    public class PhongBan
    {
        [Key, Label("Mã phòng ban")]
        public string MaPB { get; set; }

        [Label("Tên phòng ban")]
        public string TenPB { get; set; }

        [Label("Mô tả")]
        public string MoTa { get; set; }

        public override string ToString() => TenPB;
    }

    [Table("ChucVu")]
    public class ChucVu
    {
        [Key, Label("Mã chức vụ")]
        public string MaCV { get; set; }

        [Label("Tên chức vụ")]
        public string TenCV { get; set; }

        [Label("Phụ cấp chức vụ")]
        public decimal PhuCapChucVu { get; set; }

        public override string ToString() => TenCV;
    }

    [Table("CaLamViec")]
    public class CaLamViec
    {
        [Key, Label("Mã ca")]
        public string MaCa { get; set; }

        [Label("Tên ca")]
        public string TenCa { get; set; }

        [Label("Giờ bắt đầu")]
        public TimeSpan GioBatDau { get; set; }

        [Label("Giờ kết thúc")]
        public TimeSpan GioKetThuc { get; set; }

        public override string ToString() => $"{TenCa} ({GioBatDau:hh\\:mm} - {GioKetThuc:hh\\:mm})";
    }
}