using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private Button btnDangNhap;
        private Label lblLoi;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Text = "Đăng nhập - Quản lý Hồ sơ Bệnh án";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(360, 260);

            var lblTitle = new Label
            {
                Text = "QUẢN LÝ HỒ SƠ BỆNH ÁN",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 15),
                Size = new Size(360, 30)
            };

            var lblTenDangNhap = new Label { Text = "Tên đăng nhập:", Location = new Point(30, 65), Size = new Size(110, 23) };
            txtTenDangNhap = new TextBox { Location = new Point(150, 62), Size = new Size(170, 23), Text = "admin" };

            var lblMatKhau = new Label { Text = "Mật khẩu:", Location = new Point(30, 100), Size = new Size(110, 23) };
            txtMatKhau = new TextBox { Location = new Point(150, 97), Size = new Size(170, 23), PasswordChar = '*', Text = "123456" };

            lblLoi = new Label
            {
                ForeColor = Color.Red,
                Location = new Point(30, 130),
                Size = new Size(300, 40),
                Text = ""
            };

            btnDangNhap = new Button { Text = "Đăng nhập", Location = new Point(150, 180), Size = new Size(100, 32) };
            btnDangNhap.Click += BtnDangNhap_Click;

            var lblGhiChu = new Label
            {
                Text = "Tài khoản mẫu: admin / quanly / letan / bacsi1 / bacsi2 /\nxetnghiem / duocsi / vienphi - mật khẩu: 123456",
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 7.5f),
                Location = new Point(30, 220),
                Size = new Size(310, 35)
            };

            Controls.AddRange(new Control[] { lblTitle, lblTenDangNhap, txtTenDangNhap, lblMatKhau, txtMatKhau, lblLoi, btnDangNhap, lblGhiChu });

            AcceptButton = btnDangNhap;
            txtMatKhau.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnDangNhap_Click(s, e); };
        }
    }
}
