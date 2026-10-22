using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmDanhMuc: Khoa, thuốc, xét nghiệm, mức phí (mục 3.7). Quản lý tài khoản nằm ở FrmNguoiDung.</summary>
    public partial class FrmDanhMuc : Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly DanhMucService _service = new DanhMucService();

        public FrmDanhMuc(NhanVien nguoiDung, int tabMacDinh)
        {
            _nguoiDung = nguoiDung;
            Text = "Danh mục hệ thống";
            var tabs = InitializeComponent();
            TaiTatCa();
            if (tabMacDinh >= 0 && tabMacDinh < tabs.TabCount) tabs.SelectedIndex = tabMacDinh;
        }

        private void TaiTatCa() { TaiKhoa(); TaiThuoc(); TaiXetNghiem(); }
        private void TaiKhoa() => gridKhoa.DataSource = _service.LayDanhSachKhoa();
        private void TaiThuoc() => gridThuoc.DataSource = _service.LayDanhSachThuoc();
        private void TaiXetNghiem() => gridXetNghiem.DataSource = _service.LayDanhSachXetNghiem();
    }
}
