namespace QuanLyCaoOc.Data
{
    /// <summary>Ket qua tra ve cua moi nghiep vu: thanh cong hay ly do tu choi.</summary>
    public class KetQuaXuLy
    {
        public bool ThanhCong { get; private set; }
        public string ThongBao { get; private set; }

        public static KetQuaXuLy Ok(string thongBao)
        {
            return new KetQuaXuLy { ThanhCong = true, ThongBao = thongBao };
        }

        public static KetQuaXuLy Fail(string thongBao)
        {
            return new KetQuaXuLy { ThanhCong = false, ThongBao = thongBao };
        }
    }

    /// <summary>Mot phong duoc chon vao hop dong.</summary>
    public class PhongThueItem
    {
        public string MaPhong { get; set; }
        public decimal DonGiaThang { get; set; }
    }

    /// <summary>Mot dong dich vu trong hoa don cuoi thang.</summary>
    public class DichVuDungItem
    {
        public string MaDV { get; set; }
        public decimal SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }

    /// <summary>Ket qua kiem tra mot phong trong dot kiem tra.</summary>
    public class KiemTraPhongItem
    {
        public string MaPhong { get; set; }
        public string TinhTrang { get; set; }
        public bool ThayDoiKetCau { get; set; }
        public string KeHoachTuSua { get; set; }
    }

    /// <summary>Cong no cua mot phong thue tai thoi diem kiem tra gia han.</summary>
    public class CongNoPhong
    {
        public decimal NoTienThue { get; set; }
        public decimal NoDichVu { get; set; }
        public bool ConNo { get { return NoTienThue > 0 || NoDichVu > 0; } }
    }
}
