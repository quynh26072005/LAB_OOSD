using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmLePhiHoSo
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtTimKiem;
        private DataGridView grid;
        private Label lblBenhNhanDangChon, lblTrangThaiHoSo;
        private NumericUpDown numNam, numSoTien;
        private ComboBox cboPhuongThuc;

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
            var lblTim = new Label { Text = "Tìm bệnh nhân:", Location = new Point(10, 10), AutoSize = true };
            txtTimKiem = new TextBox { Location = new Point(130, 7), Size = new Size(250, 23) };
            var btnTim = new Button { Text = "Tìm kiếm", Location = new Point(390, 6), Size = new Size(90, 25) };
            btnTim.Click += (s, e) => TaiDanhSach();
            txtTimKiem.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) TaiDanhSach(); };
            panelTop.Controls.AddRange(new Control[] { lblTim, txtTimKiem, btnTim });

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 280,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            grid.CellClick += Grid_CellClick;

            var group = new GroupBox { Text = "Thu lệ phí năm / Cấp hồ sơ", Dock = DockStyle.Fill, Padding = new Padding(10) };

            lblBenhNhanDangChon = new Label { Text = "Bệnh nhân: (chưa chọn)", Location = new Point(10, 25), Size = new Size(500, 23), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            lblTrangThaiHoSo = new Label { Text = "Trạng thái hồ sơ: -", Location = new Point(10, 50), Size = new Size(500, 23), ForeColor = Color.DarkBlue };

            var lblNam = new Label { Text = "Năm áp dụng:", Location = new Point(10, 90), Size = new Size(110, 23) };
            numNam = new NumericUpDown { Location = new Point(130, 88), Size = new Size(100, 23), Minimum = 2000, Maximum = 2100, Value = DateTime.Today.Year };

            var lblSoTien = new Label { Text = "Số tiền (VNĐ):", Location = new Point(10, 125), Size = new Size(110, 23) };
            numSoTien = new NumericUpDown { Location = new Point(130, 123), Size = new Size(140, 23), Minimum = 1000, Maximum = 100000000, Increment = 10000, Value = 200000, ThousandsSeparator = true };

            var lblPt = new Label { Text = "Phương thức:", Location = new Point(10, 160), Size = new Size(110, 23) };
            cboPhuongThuc = new ComboBox { Location = new Point(130, 158), Size = new Size(140, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cboPhuongThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ" });
            cboPhuongThuc.SelectedIndex = 0;

            var btnThu = new Button { Text = "Thu lệ phí && Cấp/Cập nhật hồ sơ", Location = new Point(10, 200), Size = new Size(260, 36) };
            btnThu.Click += BtnThu_Click;

            group.Controls.AddRange(new Control[] { lblBenhNhanDangChon, lblTrangThaiHoSo, lblNam, numNam, lblSoTien, numSoTien, lblPt, cboPhuongThuc, btnThu });

            Controls.Add(group);
            Controls.Add(grid);
            Controls.Add(panelTop);
        }
    }
}
