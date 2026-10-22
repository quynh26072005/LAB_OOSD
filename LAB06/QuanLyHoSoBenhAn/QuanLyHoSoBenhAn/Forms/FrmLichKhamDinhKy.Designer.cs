using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmLichKhamDinhKy
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView gridLich;
        private TextBox txtTimLich;
        private NumericUpDown numNamLoc;

        private TextBox txtTimBenhNhan;
        private DataGridView gridBenhNhan;
        private Label lblBenhNhanDangChon;
        private NumericUpDown numNam;
        private ComboBox cboQuy;
        private DateTimePicker dtpThoiGian;

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
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 300 };

            // ----- Phần trên: danh sách lịch hẹn -----
            var panelLichTop = new Panel { Dock = DockStyle.Top, Height = 36 };
            var lblTimLich = new Label { Text = "Tìm (tên/giấy tờ):", Location = new Point(10, 8), AutoSize = true };
            txtTimLich = new TextBox { Location = new Point(140, 5), Size = new Size(200, 23) };
            var lblNamLoc = new Label { Text = "Năm:", Location = new Point(360, 8), AutoSize = true };
            numNamLoc = new NumericUpDown { Location = new Point(400, 5), Size = new Size(80, 23), Minimum = 2000, Maximum = 2100, Value = DateTime.Today.Year };
            var btnTimLich = new Button { Text = "Tìm", Location = new Point(490, 4), Size = new Size(70, 25) };
            btnTimLich.Click += (s, e) => TaiDanhSachLich();
            panelLichTop.Controls.AddRange(new Control[] { lblTimLich, txtTimLich, lblNamLoc, numNamLoc, btnTimLich });

            gridLich = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            gridLich.CellClick += GridLich_CellClick;

            var panelLichBottom = new Panel { Dock = DockStyle.Bottom, Height = 40 };
            var btnDoiLich = new Button { Text = "Đổi giờ hẹn đã chọn sang thời gian bên dưới", Location = new Point(10, 5), Size = new Size(300, 30) };
            btnDoiLich.Click += BtnDoiLich_Click;
            var btnHuyLich = new Button { Text = "Hủy lịch đã chọn", Location = new Point(320, 5), Size = new Size(140, 30) };
            btnHuyLich.Click += BtnHuyLich_Click;
            var btnTiepNhan = new Button { Text = "Tiếp nhận lịch đã chọn", Location = new Point(470, 5), Size = new Size(160, 30) };
            btnTiepNhan.Click += BtnTiepNhan_Click;
            panelLichBottom.Controls.AddRange(new Control[] { btnDoiLich, btnHuyLich, btnTiepNhan });

            var panelTren = new Panel { Dock = DockStyle.Fill };
            panelTren.Controls.Add(gridLich);
            panelTren.Controls.Add(panelLichBottom);
            panelTren.Controls.Add(panelLichTop);
            split.Panel1.Controls.Add(panelTren);

            // ----- Phần dưới: đặt lịch mới -----
            var groupDat = new GroupBox { Text = "Đặt lịch khám định kỳ mới", Dock = DockStyle.Fill, Padding = new Padding(10) };

            var panelTimBn = new Panel { Dock = DockStyle.Top, Height = 34 };
            var lblTimBn = new Label { Text = "Tìm bệnh nhân:", Location = new Point(10, 7), AutoSize = true };
            txtTimBenhNhan = new TextBox { Location = new Point(120, 4), Size = new Size(200, 23) };
            var btnTimBn = new Button { Text = "Tìm", Location = new Point(330, 3), Size = new Size(60, 25) };
            btnTimBn.Click += (s, e) => TaiDanhSachBenhNhan();
            panelTimBn.Controls.AddRange(new Control[] { lblTimBn, txtTimBenhNhan, btnTimBn });

            gridBenhNhan = new DataGridView
            {
                Dock = DockStyle.Left,
                Width = 420,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            gridBenhNhan.CellClick += GridBenhNhan_CellClick;

            var panelDat = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15, 0, 0, 0) };
            lblBenhNhanDangChon = new Label { Text = "Bệnh nhân: (chưa chọn)", Location = new Point(15, 5), Size = new Size(380, 23), Font = new Font("Segoe UI", 9, FontStyle.Bold) };

            var lblNam = new Label { Text = "Năm:", Location = new Point(15, 40), Size = new Size(80, 23) };
            numNam = new NumericUpDown { Location = new Point(100, 38), Size = new Size(90, 23), Minimum = 2000, Maximum = 2100, Value = DateTime.Today.Year };

            var lblQuy = new Label { Text = "Quý:", Location = new Point(200, 40), Size = new Size(40, 23) };
            cboQuy = new ComboBox { Location = new Point(240, 38), Size = new Size(70, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cboQuy.Items.AddRange(new object[] { 1, 2, 3, 4 });
            cboQuy.SelectedIndex = (DateTime.Today.Month - 1) / 3;

            var lblTG = new Label { Text = "Thời gian hẹn:", Location = new Point(15, 75), Size = new Size(100, 23) };
            dtpThoiGian = new DateTimePicker { Location = new Point(120, 73), Size = new Size(190, 23), Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm", Value = DateTime.Now.AddDays(1) };

            var btnDat = new Button { Text = "Đặt lịch khám định kỳ", Location = new Point(15, 110), Size = new Size(200, 34) };
            btnDat.Click += BtnDat_Click;

            panelDat.Controls.AddRange(new Control[] { lblBenhNhanDangChon, lblNam, numNam, lblQuy, cboQuy, lblTG, dtpThoiGian, btnDat });

            groupDat.Controls.Add(panelDat);
            groupDat.Controls.Add(gridBenhNhan);
            groupDat.Controls.Add(panelTimBn);

            split.Panel2.Controls.Add(groupDat);

            Controls.Add(split);
        }
    }
}
