using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmBenhNhan
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtTimKiem;
        private DataGridView grid;
        private TextBox txtHoTen, txtDiaChi, txtDienThoai, txtEmail, txtSoGiayTo, txtAnh;
        private DateTimePicker dtpNgaySinh;
        private ComboBox cboGioiTinh;
        private Label lblTrangThaiHoSo;

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
            var panelTop = new Panel { Dock = DockStyle.Top, Height = 40 };
            var lblTim = new Label { Text = "Tìm (họ tên/giấy tờ/SĐT):", Location = new Point(10, 10), AutoSize = true };
            txtTimKiem = new TextBox { Location = new Point(190, 7), Size = new Size(250, 23) };
            var btnTim = new Button { Text = "Tìm kiếm", Location = new Point(450, 6), Size = new Size(90, 25) };
            btnTim.Click += (s, e) => TaiDanhSach();
            txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) TaiDanhSach(); };
            panelTop.Controls.AddRange(new Control[] { lblTim, txtTimKiem, btnTim });

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 260,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            grid.CellClick += Grid_CellClick;

            var groupChiTiet = new GroupBox { Text = "Thông tin bệnh nhân", Dock = DockStyle.Fill, Padding = new Padding(10) };
            BuildFormChiTiet(groupChiTiet);

            Controls.Add(groupChiTiet);
            Controls.Add(grid);
            Controls.Add(panelTop);
        }

        private void BuildFormChiTiet(Control parent)
        {
            int labelX = 10, fieldX = 140, w = 250, y = 20, step = 32;

            Label L(string text, int yy) => new Label { Text = text, Location = new Point(labelX, yy), Size = new Size(120, 23) };

            parent.Controls.Add(L("Họ tên (*):", y));
            txtHoTen = new TextBox { Location = new Point(fieldX, y), Size = new Size(w, 23) }; parent.Controls.Add(txtHoTen); y += step;

            parent.Controls.Add(L("Ngày sinh (*):", y));
            dtpNgaySinh = new DateTimePicker { Location = new Point(fieldX, y), Size = new Size(w, 23), Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddYears(-30) };
            parent.Controls.Add(dtpNgaySinh); y += step;

            parent.Controls.Add(L("Giới tính (*):", y));
            cboGioiTinh = new ComboBox { Location = new Point(fieldX, y), Size = new Size(w, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
            parent.Controls.Add(cboGioiTinh); y += step;

            parent.Controls.Add(L("Số giấy tờ:", y));
            txtSoGiayTo = new TextBox { Location = new Point(fieldX, y), Size = new Size(w, 23) }; parent.Controls.Add(txtSoGiayTo); y += step;

            parent.Controls.Add(L("Điện thoại:", y));
            txtDienThoai = new TextBox { Location = new Point(fieldX, y), Size = new Size(w, 23) }; parent.Controls.Add(txtDienThoai); y += step;

            parent.Controls.Add(L("Email:", y));
            txtEmail = new TextBox { Location = new Point(fieldX, y), Size = new Size(w, 23) }; parent.Controls.Add(txtEmail); y += step;

            parent.Controls.Add(L("Địa chỉ:", y));
            txtDiaChi = new TextBox { Location = new Point(fieldX, y), Size = new Size(w, 23) }; parent.Controls.Add(txtDiaChi); y += step;

            parent.Controls.Add(L("Ảnh (đường dẫn):", y));
            txtAnh = new TextBox { Location = new Point(fieldX, y), Size = new Size(w - 70, 23) };
            var btnChonAnh = new Button { Text = "Chọn...", Location = new Point(fieldX + w - 65, y - 1), Size = new Size(65, 25) };
            btnChonAnh.Click += (s, e) =>
            {
                using (var ofd = new OpenFileDialog { Filter = "Hình ảnh|*.jpg;*.jpeg;*.png;*.bmp" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK) txtAnh.Text = ofd.FileName;
                }
            };
            parent.Controls.Add(txtAnh); parent.Controls.Add(btnChonAnh); y += step;

            lblTrangThaiHoSo = new Label { Text = "Trạng thái hồ sơ: (chưa chọn bệnh nhân)", Location = new Point(labelX, y), Size = new Size(400, 23), ForeColor = Color.DarkBlue };
            parent.Controls.Add(lblTrangThaiHoSo); y += step;

            var btnMoi = new Button { Text = "Làm mới / Đăng ký mới", Location = new Point(labelX, y), Size = new Size(180, 32) };
            btnMoi.Click += (s, e) => LamMoiForm();

            var btnLuu = new Button { Text = "Lưu (Thêm/Cập nhật)", Location = new Point(labelX + 190, y), Size = new Size(180, 32) };
            btnLuu.Click += BtnLuu_Click;

            var btnLichSu = new Button { Text = "Xem lịch sử khám", Location = new Point(labelX + 380, y), Size = new Size(160, 32) };
            btnLichSu.Click += BtnLichSu_Click;

            parent.Controls.Add(btnMoi);
            parent.Controls.Add(btnLuu);
            parent.Controls.Add(btnLichSu);
        }
    }
}
