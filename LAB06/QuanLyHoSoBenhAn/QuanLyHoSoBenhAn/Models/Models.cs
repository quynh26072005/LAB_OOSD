using System;

namespace QuanLyHoSoBenhAn.Models
{
    public class NhanVien
    {
        public int MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string TenDangNhap { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }

        public bool KiemTraQuyen(params string[] vaiTroChoPhep)
        {
            foreach (var vt in vaiTroChoPhep)
                if (string.Equals(vt, VaiTro, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }

        public override string ToString() => $"{HoTen} ({VaiTro})";
    }

    public class BacSi
    {
        public int MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string SoChungChi { get; set; }
        public string ChuyenMon { get; set; }

        public override string ToString() => HoTen;
    }

    public class Khoa
    {
        public int MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string DienThoai { get; set; }
        public string ViTri { get; set; }

        public override string ToString() => TenKhoa;
    }

    public class BenhNhan
    {
        public int MaBenhNhan { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string SoGiayTo { get; set; }
        public string Anh { get; set; }

        public override string ToString() => $"{HoTen} - {SoGiayTo}";
    }

    public class HoSoBenhAn
    {
        public int MaHoSo { get; set; }
        public int MaBenhNhan { get; set; }
        public DateTime NgayCap { get; set; }
        public string TrangThai { get; set; }
    }

    public class LePhiNam
    {
        public int MaLePhi { get; set; }
        public int MaBenhNhan { get; set; }
        public int NamApDung { get; set; }
        public decimal SoTien { get; set; }
        public DateTime NgayThu { get; set; }
        public string TrangThai { get; set; }
    }

    public class LichKhamDinhKy
    {
        public int MaLichHen { get; set; }
        public int MaBenhNhan { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
        public DateTime ThoiGianHen { get; set; }
        public string TrangThai { get; set; }
    }

    public class PhieuKham
    {
        public int MaPhieuKham { get; set; }
        public int MaHoSo { get; set; }
        public int MaKhoa { get; set; }
        public int MaNhanVien { get; set; }
        public int? MaLichHen { get; set; }
        public DateTime ThoiGian { get; set; }
        public string LoaiKham { get; set; }
        public string TrieuChung { get; set; }
        public string SinhHieu { get; set; }
        public string ChanDoan { get; set; }
        public string TrangThai { get; set; }
    }

    public class XetNghiem
    {
        public int MaXetNghiem { get; set; }
        public string TenXetNghiem { get; set; }
        public string DonVi { get; set; }
        public string MoTa { get; set; }
        public decimal DonGia { get; set; }

        public override string ToString() => TenXetNghiem;
    }

    public class ChiDinhXetNghiem
    {
        public int MaChiDinh { get; set; }
        public int MaPhieuKham { get; set; }
        public int MaXetNghiem { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayChiDinh { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }

    public class KetQuaXetNghiem
    {
        public int MaKetQua { get; set; }
        public int MaChiDinh { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime ThoiGian { get; set; }
        public string KetQua { get; set; }
        public string DonVi { get; set; }
        public string KhoangThamChieu { get; set; }
        public string NhanXet { get; set; }
    }

    public class Thuoc
    {
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string HamLuong { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public string TrangThai { get; set; }

        public override string ToString() => $"{TenThuoc} {HamLuong}";
    }

    public class ToaThuoc
    {
        public int MaToaThuoc { get; set; }
        public int MaPhieuKham { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayKe { get; set; }
        public string LoiDan { get; set; }
        public string TrangThai { get; set; }
    }

    public class ChiTietToaThuoc
    {
        public int MaChiTietToa { get; set; }
        public int MaToaThuoc { get; set; }
        public int MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public string LieuDung { get; set; }
        public string TanSuat { get; set; }
        public int SoNgay { get; set; }
        public string CachDung { get; set; }
    }

    public class HoaDonVienPhi
    {
        public int MaHoaDon { get; set; }
        public int MaPhieuKham { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
    }

    public class ChiTietVienPhi
    {
        public int MaChiTietVP { get; set; }
        public int MaHoaDon { get; set; }
        public string NoiDung { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }

    public class ThanhToan
    {
        public int MaThanhToan { get; set; }
        public int MaHoaDon { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc { get; set; }
        public string MaGiaoDich { get; set; }
    }

    public class NhatKyTruyCap
    {
        public int MaNhatKy { get; set; }
        public int? MaNhanVien { get; set; }
        public DateTime ThoiGian { get; set; }
        public string HanhDong { get; set; }
        public string DoiTuong { get; set; }
        public int? MaDoiTuong { get; set; }
    }
}
