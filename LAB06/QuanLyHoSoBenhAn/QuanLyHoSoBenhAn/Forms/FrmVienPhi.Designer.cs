using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmVienPhi
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView gridChoLapHoaDon;
        private DataGridView gridHoaDonChuaXong;
        private Label lblHoaDonDangChon;
        private DataGridView gridChiTietHoaDon;
        private NumericUpDown numSoTienThanhToan;
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
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, SplitterDistance = 520 };

            // ----- Trái: 2 danh sách -----
            var panelTrai = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
            panelTrai.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            panelTrai.RowStyles.Add(new RowStyle(SizeType.Percent, 60));

            var groupCho = new GroupBox { Text = "Phiếu khám chờ lập hóa đơn", Dock = DockStyle.Fill };
            gridChoLapHoaDon = new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            var btnLap = new Button { Text = "Lập hóa đơn cho phiếu đang chọn", Dock = DockStyle.Bottom, Height = 32 };
            btnLap.Click += BtnLapHoaDon_Click;
            groupCho.Controls.Add(gridChoLapHoaDon);
            groupCho.Controls.Add(btnLap);

            var groupHD = new GroupBox { Text = "Hóa đơn chưa thanh toán xong", Dock = DockStyle.Fill };
            gridHoaDonChuaXong = new DataGridView
            {
                Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            gridHoaDonChuaXong.CellClick += GridHoaDonChuaXong_CellClick;
            groupHD.Controls.Add(gridHoaDonChuaXong);

            panelTrai.Controls.Add(groupCho, 0, 0);
            panelTrai.Controls.Add(groupHD, 0, 1);
            split.Panel1.Controls.Add(panelTrai);

            // ----- Phải: chi tiết + thanh toán -----
            var panelPhai = new Panel { Dock = DockStyle.Fill };
            lblHoaDonDangChon = new Label { Dock = DockStyle.Top, Height = 50, Text = "Chưa chọn hóa đơn.", Font = new Font("Segoe UI", 9, FontStyle.Bold), Padding = new Padding(10) };

            gridChiTietHoaDon = new DataGridView
            {
                Dock = DockStyle.Top, Height = 220, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var groupThanhToan = new GroupBox { Text = "Ghi nhận thanh toán", Dock = DockStyle.Fill, Padding = new Padding(10) };
            var lblSoTien = new Label { Text = "Số tiền (VNĐ):", Location = new Point(10, 25), Size = new Size(110, 23) };
            numSoTienThanhToan = new NumericUpDown { Location = new Point(130, 23), Size = new Size(140, 23), Minimum = 0, Maximum = 1000000000, Increment = 10000, ThousandsSeparator = true };
            var lblPt = new Label { Text = "Phương thức:", Location = new Point(10, 60), Size = new Size(110, 23) };
            cboPhuongThuc = new ComboBox { Location = new Point(130, 58), Size = new Size(140, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cboPhuongThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ" });
            cboPhuongThuc.SelectedIndex = 0;
            var btnThanhToan = new Button { Text = "Ghi nhận thanh toán", Location = new Point(10, 100), Size = new Size(200, 34), BackColor = Color.FromArgb(40, 120, 70), ForeColor = Color.White };
            btnThanhToan.Click += BtnThanhToan_Click;

            groupThanhToan.Controls.AddRange(new Control[] { lblSoTien, numSoTienThanhToan, lblPt, cboPhuongThuc, btnThanhToan });

            panelPhai.Controls.Add(groupThanhToan);
            panelPhai.Controls.Add(gridChiTietHoaDon);
            panelPhai.Controls.Add(lblHoaDonDangChon);
            split.Panel2.Controls.Add(panelPhai);

            Controls.Add(split);
        }
    }
}
