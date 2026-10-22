using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmTiepNhanKham
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtTimBenhNhan;
        private DataGridView gridBenhNhan;
        private Label lblBenhNhanDangChon;
        private ComboBox cboKhoa, cboBacSi, cboLoaiKham;
        private DataGridView gridHangDoi;

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
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, SplitterDistance = 480 };

            // ----- Trái: tìm bệnh nhân + tạo phiếu -----
            var panelTim = new Panel { Dock = DockStyle.Top, Height = 34 };
            var lblTim = new Label { Text = "Tìm bệnh nhân:", Location = new Point(10, 7), AutoSize = true };
            txtTimBenhNhan = new TextBox { Location = new Point(110, 4), Size = new Size(200, 23) };
            var btnTim = new Button { Text = "Tìm", Location = new Point(320, 3), Size = new Size(60, 25) };
            btnTim.Click += (s, e) => TaiDanhSachBenhNhan();
            txtTimBenhNhan.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) TaiDanhSachBenhNhan(); };
            panelTim.Controls.AddRange(new Control[] { lblTim, txtTimBenhNhan, btnTim });

            gridBenhNhan = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            gridBenhNhan.CellClick += GridBenhNhan_CellClick;

            var panelTao = new Panel { Dock = DockStyle.Bottom, Height = 180, Padding = new Padding(10) };
            lblBenhNhanDangChon = new Label { Text = "Bệnh nhân: (chưa chọn)", Location = new Point(10, 5), Size = new Size(440, 23), Font = new Font("Segoe UI", 9, FontStyle.Bold) };

            var lblKhoa = new Label { Text = "Khoa:", Location = new Point(10, 40), Size = new Size(80, 23) };
            cboKhoa = new ComboBox { Location = new Point(100, 38), Size = new Size(240, 23), DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "TenKhoa", ValueMember = "MaKhoa" };

            var lblBacSi = new Label { Text = "Bác sĩ:", Location = new Point(10, 75), Size = new Size(80, 23) };
            cboBacSi = new ComboBox { Location = new Point(100, 73), Size = new Size(240, 23), DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "HoTen", ValueMember = "MaNhanVien" };

            var lblLoai = new Label { Text = "Loại khám:", Location = new Point(10, 110), Size = new Size(80, 23) };
            cboLoaiKham = new ComboBox { Location = new Point(100, 108), Size = new Size(240, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiKham.Items.AddRange(new object[] { "ThuongQuy", "DinhKy", "CapCuu" });
            cboLoaiKham.SelectedIndex = 0;

            var btnTao = new Button { Text = "Tạo phiếu khám (Tiếp nhận)", Location = new Point(10, 145), Size = new Size(240, 30) };
            btnTao.Click += BtnTao_Click;

            panelTao.Controls.AddRange(new Control[] { lblBenhNhanDangChon, lblKhoa, cboKhoa, lblBacSi, cboBacSi, lblLoai, cboLoaiKham, btnTao });

            var panelTrai = new Panel { Dock = DockStyle.Fill };
            panelTrai.Controls.Add(gridBenhNhan);
            panelTrai.Controls.Add(panelTao);
            panelTrai.Controls.Add(panelTim);
            split.Panel1.Controls.Add(panelTrai);

            // ----- Phải: hàng đợi -----
            var groupHangDoi = new GroupBox { Text = "Hàng đợi khám hôm nay", Dock = DockStyle.Fill, Padding = new Padding(10) };
            gridHangDoi = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            var btnLamMoi = new Button { Text = "Làm mới hàng đợi", Dock = DockStyle.Bottom, Height = 32 };
            btnLamMoi.Click += (s, e) => TaiHangDoi();
            groupHangDoi.Controls.Add(gridHangDoi);
            groupHangDoi.Controls.Add(btnLamMoi);

            split.Panel2.Controls.Add(groupHangDoi);

            Controls.Add(split);
        }
    }
}
