using System;

namespace QuanLyNhanSu.Data
{
    [Table("HopDong")]
    public class HopDong
    {
        [Key, Label("Số hợp đồng")]
        public string SoHD { get; set; }

        [Label("Loại hợp đồng")]
        public string LoaiHD { get; set; }

        [Label("Ngày ký")]
        public DateTime NgayKy { get; set; }

        [Label("Ngày bắt đầu")]
        public DateTime NgayBatDau { get; set; }

        [Label("Ngày kết thúc")]
        public DateTime? NgayKetThuc { get; set; }

        [Column("MaNV"), Label("Mã nhân viên")]
        public string MaNV { get; set; }

        [Ignore]
        public string HoTen { get; set; }
    }

    [Table("BaoHiem")]
    public class BaoHiem
    {
        [Key, Label("Mã bảo hiểm")]
        public string MaBH { get; set; }

        [Label("Loại bảo hiểm")]
        public string LoaiBH { get; set; }

        [Label("Số sổ")]
        public string SoSo { get; set; }

        [Label("Mức đóng")]
        public decimal MucDong { get; set; }

        [Label("Ngày bắt đầu")]
        public DateTime? NgayBatDau { get; set; }

        [Column("SoHD"), Label("Số hợp đồng")]
        public string SoHD { get; set; }

        [Ignore]
        public string HoTen { get; set; }
    }
}