using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly AuthService _authService = new AuthService();

        public NhanVien NhanVienDangNhap { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            lblLoi.Text = "";

            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                lblLoi.Text = "Vui lòng nhập tên đăng nhập.";
                return;
            }

            if (!DbHelper.TestConnection(out string loiKetNoi))
            {
                lblLoi.Text = "Không kết nối được CSDL. Kiểm tra lại chuỗi kết nối trong App.config.\n" + loiKetNoi;
                return;
            }

            try
            {
                var nv = _authService.DangNhap(txtTenDangNhap.Text.Trim(), txtMatKhau.Text);
                if (nv == null)
                {
                    lblLoi.Text = "Sai tên đăng nhập/mật khẩu hoặc tài khoản đã ngừng hoạt động.";
                    return;
                }

                NhanVienDangNhap = nv;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblLoi.Text = "Lỗi đăng nhập: " + ex.Message;
            }
        }
    }
}
