using System;

namespace QuanLyNhanSu.Data
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key, Label("Mã nhân viên")]
        public string MaNV { get; set; }

        [Label("Họ và tên")]
        public string HoTen { get; set; }

        [Label("Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [Label("Giới tính")]
        public string GioiTinh { get; set; }

        [Label("Địa chỉ")]
        public string DiaChi { get; set; }

        [Label("Số điện thoại")]
        public string SDT { get; set; }

        [Label("CCCD")]
        public string CCCD { get; set; }

        [Column("MaPB"), Label("Mã phòng ban")]
        public string MaPB { get; set; }

        [Column("MaCV"), Label("Mã chức vụ")]
        public string MaCV { get; set; }

        [Ignore]
        public string TenPB { get; set; }

        [Ignore]
        public string TenCV { get; set; }
    }
}