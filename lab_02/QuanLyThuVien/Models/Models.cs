using System;

namespace QuanLyThuVien.Models
{
    /// <summary>
    /// Model Nhân viên
    /// </summary>
    public class NhanVien
    {
        public string MaNhanVien { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string Phai { get; set; }
        public DateTime NgaySinh { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }

        public string HoTen => $"{Ho} {Ten}";
    }

    /// <summary>
    /// Model Thể loại
    /// </summary>
    public class TheLoai
    {
        public string MaTheLoai { get; set; }
        public string TenTheLoai { get; set; }
    }

    /// <summary>
    /// Model Nhà xuất bản
    /// </summary>
    public class NhaXuatBan
    {
        public string MaNhaXuatBan { get; set; }
        public string TenNXB { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
    }

    /// <summary>
    /// Model Đầu sách
    /// </summary>
    public class DauSach
    {
        public string MaDauSach { get; set; }
        public string TenSach { get; set; }
        public int NamXuatBan { get; set; }
        public int SoLuongHienCo { get; set; }
        public string MaTheLoai { get; set; }
        public string MaNhaXuatBan { get; set; }

        // Thuộc tính mở rộng để hiển thị
        public string TenTheLoai { get; set; }
        public string TenNXB { get; set; }
    }

    /// <summary>
    /// Model Độc giả
    /// </summary>
    public class DocGia
    {
        public string MaDocGia { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public bool Phai { get; set; }  // true = Nam, false = Nữ
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string Anh3x4 { get; set; }

        public string HoTen => $"{Ho} {Ten}";
    }

    /// <summary>
    /// Model Thẻ độc giả
    /// </summary>
    public class TheDocGia
    {
        public string MaThe { get; set; }
        public string MaDocGia { get; set; }
        public DateTime NgayCap { get; set; }
        public DateTime HanSuDung { get; set; }
        public bool DaDongLePhi { get; set; }
        public int TrangThai { get; set; } // 1: Hoạt động, 0: Hết hạn

        public bool ConHieuLuc => TrangThai == 1 && HanSuDung >= DateTime.Today && DaDongLePhi;
    }

    /// <summary>
    /// Model Phiếu mượn
    /// </summary>
    public class PhieuMuon
    {
        public string MaPhieuMuon { get; set; }
        public string MaDocGia { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayHenTra { get; set; }
    }

    /// <summary>
    /// Model Chi tiết phiếu mượn
    /// </summary>
    public class ChiTietPhieuMuon
    {
        public string MaChiTiet { get; set; }
        public string MaPhieuMuon { get; set; }
        public string MaDauSach { get; set; }
        public DateTime? NgayTraThucTe { get; set; }
        public string TinhTrang { get; set; }

        // Thuộc tính mở rộng
        public string TenSach { get; set; }
        public DateTime NgayHenTra { get; set; }
        public bool DaTra => NgayTraThucTe.HasValue;
        public bool QuaHan => !DaTra && NgayHenTra < DateTime.Today;
    }

    /// <summary>
    /// Model Phiếu phạt
    /// </summary>
    public class PhieuPhat
    {
        public string MaPhieuPhat { get; set; }
        public string MaChiTiet { get; set; }
        public DateTime NgayPhat { get; set; }
        public string LyDo { get; set; }
        public decimal PhiPhat { get; set; }
        public string MaNhanVien { get; set; }
    }

    /// <summary>
    /// Kết quả xử lý nghiệp vụ
    /// </summary>
    public class KetQuaXuLy
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; }
        public object DuLieu { get; set; }

        public static KetQuaXuLy OK(string thongBao = "Thành công", object duLieu = null)
        {
            return new KetQuaXuLy { ThanhCong = true, ThongBao = thongBao, DuLieu = duLieu };
        }

        public static KetQuaXuLy Loi(string thongBao)
        {
            return new KetQuaXuLy { ThanhCong = false, ThongBao = thongBao };
        }
    }

    /// <summary>
    /// Model cho thống kê
    /// </summary>
    public class ThongKe
    {
        public int SoLuotMuon { get; set; }
        public int SachQuaHan { get; set; }
        public int SachMat { get; set; }
        public int SachHuHong { get; set; }
        public decimal TongPhiPhat { get; set; }
    }
}
