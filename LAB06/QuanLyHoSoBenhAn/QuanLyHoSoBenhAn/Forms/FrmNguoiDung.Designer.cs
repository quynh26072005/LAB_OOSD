using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmNguoiDung
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView gridNhanVien;
        private TextBox txtHoTen, txtDienThoai, txtEmail, txtTenDangNhap, txtMatKhau, txtSoChungChi, txtChuyenMon;
        private ComboBox cboVaiTro;

        private DateTimePicker dtpTu, dtpDen;
        private DataGridView gridNhatKy;

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
            var tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(TabTaiKhoan());
            tabs.TabPages.Add(TabNhatKy());
            Controls.Add(tabs);
        }

        private TabPage TabTaiKhoan()
        {
            var tab = new TabPage("Tài khoản nhân viên");

            gridNhanVien = new DataGridView
            {
                Dock = DockStyle.Top, Height = 260, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            gridNhanVien.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                var r = gridNhanVien.Rows[e.RowIndex];
                _maNhanVienDangChon = Convert.ToInt32(r.Cells["MaNhanVien"].Value);
            };

            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            var l1 = new Label { Text = "Họ tên (*):", Location = new Point(10, 15), Size = new Size(100, 23) };
            txtHoTen = new TextBox { Location = new Point(120, 12), Size = new Size(220, 23) };
            var l2 = new Label { Text = "Điện thoại:", Location = new Point(360, 15), Size = new Size(100, 23) };
            txtDienThoai = new TextBox { Location = new Point(470, 12), Size = new Size(180, 23) };

            var l3 = new Label { Text = "Email:", Location = new Point(10, 48), Size = new Size(100, 23) };
            txtEmail = new TextBox { Location = new Point(120, 45), Size = new Size(220, 23) };
            var l4 = new Label { Text = "Vai trò (*):", Location = new Point(360, 48), Size = new Size(100, 23) };
            cboVaiTro = new ComboBox { Location = new Point(470, 45), Size = new Size(180, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cboVaiTro.Items.AddRange(new object[] { "LeTan", "BacSi", "XetNghiem", "DuocSi", "VienPhi", "QuanLy", "QuanTri" });
            cboVaiTro.SelectedIndex = 0;

            var l5 = new Label { Text = "Tên đăng nhập (*):", Location = new Point(10, 81), Size = new Size(100, 23) };
            txtTenDangNhap = new TextBox { Location = new Point(120, 78), Size = new Size(220, 23) };
            var l6 = new Label { Text = "Mật khẩu (*):", Location = new Point(360, 81), Size = new Size(100, 23) };
            txtMatKhau = new TextBox { Location = new Point(470, 78), Size = new Size(180, 23), PasswordChar = '*' };

            var l7 = new Label { Text = "Số chứng chỉ (nếu là Bác sĩ):", Location = new Point(10, 114), Size = new Size(180, 23) };
            txtSoChungChi = new TextBox { Location = new Point(200, 111), Size = new Size(140, 23) };
            var l8 = new Label { Text = "Chuyên môn:", Location = new Point(360, 114), Size = new Size(100, 23) };
            txtChuyenMon = new TextBox { Location = new Point(470, 111), Size = new Size(180, 23) };

            var btnThem = new Button { Text = "Tạo tài khoản mới", Location = new Point(10, 150), Size = new Size(160, 32) };
            btnThem.Click += BtnThem_Click;
            var btnKhoa = new Button { Text = "Khóa tài khoản đang chọn", Location = new Point(180, 150), Size = new Size(190, 32) };
            btnKhoa.Click += (s, e) => DoiTrangThai("NghiViec");
            var btnMoKhoa = new Button { Text = "Mở lại tài khoản đang chọn", Location = new Point(380, 150), Size = new Size(200, 32) };
            btnMoKhoa.Click += (s, e) => DoiTrangThai("DangLamViec");
            var btnDatLaiMK = new Button { Text = "Đặt lại mật khẩu = 123456", Location = new Point(590, 150), Size = new Size(200, 32) };
            btnDatLaiMK.Click += BtnDatLaiMK_Click;

            panel.Controls.AddRange(new Control[] { l1, txtHoTen, l2, txtDienThoai, l3, txtEmail, l4, cboVaiTro,
                l5, txtTenDangNhap, l6, txtMatKhau, l7, txtSoChungChi, l8, txtChuyenMon, btnThem, btnKhoa, btnMoKhoa, btnDatLaiMK });

            tab.Controls.Add(panel);
            tab.Controls.Add(gridNhanVien);
            return tab;
        }

        private TabPage TabNhatKy()
        {
            var tab = new TabPage("Nhật ký truy cập");
            var panelTop = new Panel { Dock = DockStyle.Top, Height = 40 };
            var lblTu = new Label { Text = "Từ ngày:", Location = new Point(10, 10), AutoSize = true };
            dtpTu = new DateTimePicker { Location = new Point(70, 7), Size = new Size(120, 23), Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(-7) };
            var lblDen = new Label { Text = "Đến ngày:", Location = new Point(210, 10), AutoSize = true };
            dtpDen = new DateTimePicker { Location = new Point(275, 7), Size = new Size(120, 23), Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            var btnXem = new Button { Text = "Xem", Location = new Point(410, 6), Size = new Size(80, 25) };
            btnXem.Click += (s, e) => TaiNhatKy();
            panelTop.Controls.AddRange(new Control[] { lblTu, dtpTu, lblDen, dtpDen, btnXem });

            gridNhatKy = new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            tab.Controls.Add(gridNhatKy);
            tab.Controls.Add(panelTop);
            return tab;
        }
    }
}
