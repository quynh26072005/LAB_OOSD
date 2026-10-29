using System;

namespace QuanLyNhanSu.Data
{
    [Table("BangLuong")]
    public class BangLuong
    {
        [Key, Label("Mã bảng lương")]
        public string MaBL { get; set; }

        [Label("Tháng")]
        public int Thang { get; set; }

        [Label("Năm")]
        public int Nam { get; set; }

        [Label("Ngày lập")]
        public DateTime NgayLap { get; set; }
    }

    [Table("ChiTietBangLuong")]
    public class ChiTietBangLuong
    {
        [Key, Column("MaBL"), Label("Mã bảng lương")]
        public string MaBL { get; set; }

        [Key, Column("MaNV"), Label("Mã nhân viên")]
        public string MaNV { get; set; }

        [Label("Lương cơ bản")]
        public decimal LuongCB { get; set; }

        [Label("Phụ cấp")]
        public decimal PhuCap { get; set; }

        [Label("Tiền phụ trội")]
        public decimal TienPhuTroi { get; set; }

        [Label("Tiền thưởng")]
        public decimal TienThuong { get; set; }

        [Label("Tiền phạt")]
        public decimal TienPhat { get; set; }

        [Label("BHYT")]
        public decimal BHYT { get; set; }

        [Label("BHXH")]
        public decimal BHXH { get; set; }

        [Label("Thực lãnh")]
        public decimal ThucLanh { get; set; }

        [Ignore]
        public string HoTen { get; set; }
    }

    [Table("PhieuChiKhenThuong")]
    public class PhieuChiKhenThuong
    {
        [Key, Label("Số phiếu chi")]
        public string SoPhieuChi { get; set; }

        [Label("Ngày chi")]
        public DateTime NgayChi { get; set; }

        [Label("Số tiền")]
        public decimal SoTien { get; set; }

        [Column("SoQD"), Label("Số quyết định")]
        public string SoQD { get; set; }
    }

    [Table("PhieuThuNopPhat")]
    public class PhieuThuNopPhat
    {
        [Key, Label("Số phiếu thu")]
        public string SoPhieuThu { get; set; }

        [Label("Ngày thu")]
        public DateTime NgayThu { get; set; }

        [Label("Số tiền")]
        public decimal SoTien { get; set; }

        [Label("Hình thức")]
        public string HinhThuc { get; set; }

        [Column("SoQD"), Label("Số quyết định")]
        public string SoQD { get; set; }
    }
}