using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmNguoiDung: tài khoản, quyền, nhật ký (mục 3.7). Chỉ Quản trị hệ thống được mở form này - TC16.</summary>
    public partial class FrmNguoiDung : Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly DanhMucService _dmService = new DanhMucService();
        private readonly AuthService _authService = new AuthService();

        private int? _maNhanVienDangChon;

        public FrmNguoiDung(NhanVien nguoiDung)
        {
            _nguoiDung = nguoiDung;

            // BR15/TC16: chỉ Quản trị hệ thống mới được quản lý tài khoản; từ chối và ghi nhật ký nếu không đủ quyền.
            if (!_nguoiDung.KiemTraQuyen("QuanTri"))
            {
                _authService.GhiNhatKy(_nguoiDung.MaNhanVien, "TuChoiTruyCap:FrmNguoiDung", "NhanVien", null);
                MessageBox.Show("Bạn không có quyền truy cập chức năng này. Hành động đã được ghi nhật ký.",
                    "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                BeginInvoke((Action)Close);
                return;
            }

            Text = "Tài khoản & Nhật ký truy cập";
            InitializeComponent();

            TaiNhanVien();
            TaiNhatKy();
        }

        private void TaiNhanVien() => gridNhanVien.DataSource = _dmService.LayDanhSachNhanVien();
        private void TaiNhatKy() => gridNhatKy.DataSource = _authService.LayNhatKy(dtpTu.Value.Date, dtpDen.Value.Date);

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ họ tên, tên đăng nhập và mật khẩu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _dmService.ThemNhanVien(txtHoTen.Text, txtDienThoai.Text, txtEmail.Text, txtTenDangNhap.Text, txtMatKhau.Text,
                    cboVaiTro.SelectedItem.ToString(), txtSoChungChi.Text, txtChuyenMon.Text, _nguoiDung.MaNhanVien);
                MessageBox.Show("Tạo tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tạo tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoiTrangThai(string trangThai)
        {
            if (_maNhanVienDangChon == null) { MessageBox.Show("Vui lòng chọn một tài khoản.", "Thông báo"); return; }
            _dmService.DoiTrangThaiNhanVien(_maNhanVienDangChon.Value, trangThai, _nguoiDung.MaNhanVien);
            TaiNhanVien();
        }

        private void BtnDatLaiMK_Click(object sender, EventArgs e)
        {
            if (_maNhanVienDangChon == null) { MessageBox.Show("Vui lòng chọn một tài khoản.", "Thông báo"); return; }
            _dmService.DatLaiMatKhau(_maNhanVienDangChon.Value, "123456", _nguoiDung.MaNhanVien);
            MessageBox.Show("Đã đặt lại mật khẩu về 123456.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
