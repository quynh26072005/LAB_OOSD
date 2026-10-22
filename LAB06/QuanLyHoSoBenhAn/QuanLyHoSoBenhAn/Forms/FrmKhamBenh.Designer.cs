using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmKhamBenh
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView gridHangDoi;
        private Label lblPhieuDangChon;

        private TextBox txtTrieuChung, txtSinhHieu, txtChanDoan;

        private ComboBox cboXetNghiem;
        private TextBox txtGhiChuChiDinh;
        private DataGridView gridChiDinh;

        private DataGridView gridToaMoi;
        private ComboBox cboThuoc;
        private TextBox txtLoiDan;
        private BindingList<DongThuoc> _dsThuocMoi;
        private DataGridView gridToaDaLuu;

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
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 190 };

            // ----- Hàng đợi -----
            gridHangDoi = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            gridHangDoi.CellClick += GridHangDoi_CellClick;
            var panelHD = new Panel { Dock = DockStyle.Top, Height = 32 };
            var btnLamMoi = new Button { Text = "Làm mới hàng đợi", Location = new Point(10, 3), Size = new Size(150, 25) };
            btnLamMoi.Click += (s, e) => TaiHangDoi();
            panelHD.Controls.Add(btnLamMoi);

            var panelTren = new Panel { Dock = DockStyle.Fill };
            panelTren.Controls.Add(gridHangDoi);
            panelTren.Controls.Add(panelHD);
            split.Panel1.Controls.Add(panelTren);

            // ----- Chi tiết khám -----
            var panelDuoi = new Panel { Dock = DockStyle.Fill };
            lblPhieuDangChon = new Label
            {
                Dock = DockStyle.Top, Height = 28, Text = "Chưa chọn phiếu khám.",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), Padding = new Padding(10, 5, 0, 0)
            };

            var tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(TabKhamBenh());
            tabs.TabPages.Add(TabChiDinh());
            tabs.TabPages.Add(TabToaThuoc());

            var panelHoanTat = new Panel { Dock = DockStyle.Bottom, Height = 44 };
            var btnHoanTat = new Button
            {
                Text = "Hoàn tất chuyên môn (chuyển Chờ viện phí)",
                Location = new Point(10, 5), Size = new Size(320, 34),
                BackColor = Color.FromArgb(40, 120, 70), ForeColor = Color.White
            };
            btnHoanTat.Click += BtnHoanTat_Click;
            panelHoanTat.Controls.Add(btnHoanTat);

            panelDuoi.Controls.Add(tabs);
            panelDuoi.Controls.Add(panelHoanTat);
            panelDuoi.Controls.Add(lblPhieuDangChon);
            split.Panel2.Controls.Add(panelDuoi);

            Controls.Add(split);
        }

        private TabPage TabKhamBenh()
        {
            var tab = new TabPage("1. Khám bệnh");
            var lbl1 = new Label { Text = "Triệu chứng:", Location = new Point(10, 15), Size = new Size(100, 23) };
            txtTrieuChung = new TextBox { Location = new Point(120, 12), Size = new Size(500, 23) };
            var lbl2 = new Label { Text = "Sinh hiệu:", Location = new Point(10, 48), Size = new Size(100, 23) };
            txtSinhHieu = new TextBox { Location = new Point(120, 45), Size = new Size(500, 23) };
            var lbl3 = new Label { Text = "Chẩn đoán:", Location = new Point(10, 81), Size = new Size(100, 23) };
            txtChanDoan = new TextBox { Location = new Point(120, 78), Size = new Size(500, 60), Multiline = true };
            var btnLuu = new Button { Text = "Lưu thông tin khám", Location = new Point(120, 150), Size = new Size(180, 32) };
            btnLuu.Click += BtnLuuKham_Click;
            tab.Controls.AddRange(new Control[] { lbl1, txtTrieuChung, lbl2, txtSinhHieu, lbl3, txtChanDoan, btnLuu });
            return tab;
        }

        private TabPage TabChiDinh()
        {
            var tab = new TabPage("2. Chỉ định xét nghiệm");
            var lbl1 = new Label { Text = "Xét nghiệm:", Location = new Point(10, 15), Size = new Size(90, 23) };
            cboXetNghiem = new ComboBox { Location = new Point(105, 12), Size = new Size(260, 23), DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "TenXetNghiem", ValueMember = "MaXetNghiem" };
            var lbl2 = new Label { Text = "Ghi chú:", Location = new Point(380, 15), Size = new Size(60, 23) };
            txtGhiChuChiDinh = new TextBox { Location = new Point(445, 12), Size = new Size(200, 23) };
            var btnChiDinh = new Button { Text = "Tạo chỉ định", Location = new Point(655, 10), Size = new Size(120, 27) };
            btnChiDinh.Click += BtnChiDinh_Click;

            gridChiDinh = new DataGridView
            {
                Location = new Point(10, 50), Size = new Size(765, 300), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            tab.Controls.AddRange(new Control[] { lbl1, cboXetNghiem, lbl2, txtGhiChuChiDinh, btnChiDinh, gridChiDinh });
            return tab;
        }

        private TabPage TabToaThuoc()
        {
            var tab = new TabPage("3. Toa thuốc");

            var lblLoiDan = new Label { Text = "Lời dặn:", Location = new Point(10, 15), Size = new Size(60, 23) };
            txtLoiDan = new TextBox { Location = new Point(75, 12), Size = new Size(400, 23) };

            var lblThuoc = new Label { Text = "Thuốc:", Location = new Point(10, 48), Size = new Size(60, 23) };
            cboThuoc = new ComboBox { Location = new Point(75, 45), Size = new Size(230, 23), DropDownStyle = ComboBoxStyle.DropDownList, DisplayMember = "TenThuoc", ValueMember = "MaThuoc" };
            var numSoLuong = new NumericUpDown { Location = new Point(315, 45), Size = new Size(60, 23), Minimum = 1, Maximum = 1000, Value = 1, Tag = "SoLuong" };
            var txtLieuDung = new TextBox { Location = new Point(380, 45), Size = new Size(110, 23), Text = "1 viên/lần", Tag = "LieuDung" };
            var txtTanSuat = new TextBox { Location = new Point(495, 45), Size = new Size(90, 23), Text = "2 lần/ngày", Tag = "TanSuat" };
            var numSoNgay = new NumericUpDown { Location = new Point(590, 45), Size = new Size(55, 23), Minimum = 1, Maximum = 90, Value = 5, Tag = "SoNgay" };
            var txtCachDung = new TextBox { Location = new Point(650, 45), Size = new Size(125, 23), Text = "Sau ăn", Tag = "CachDung" };
            var btnThem = new Button { Text = "Thêm dòng thuốc vào toa", Location = new Point(75, 78), Size = new Size(200, 27) };
            btnThem.Click += (s, e) =>
            {
                if (cboThuoc.SelectedValue == null) return;
                _dsThuocMoi.Add(new DongThuoc
                {
                    MaThuoc = Convert.ToInt32(cboThuoc.SelectedValue),
                    TenThuoc = cboThuoc.Text,
                    SoLuong = (int)numSoLuong.Value,
                    LieuDung = txtLieuDung.Text,
                    TanSuat = txtTanSuat.Text,
                    SoNgay = (int)numSoNgay.Value,
                    CachDung = txtCachDung.Text
                });
            };

            gridToaMoi = new DataGridView { Location = new Point(10, 115), Size = new Size(765, 140), AllowUserToAddRows = false };
            _dsThuocMoi = new BindingList<DongThuoc>();
            gridToaMoi.DataSource = _dsThuocMoi;

            var btnXoaDong = new Button { Text = "Xóa dòng đang chọn", Location = new Point(10, 262), Size = new Size(160, 27) };
            btnXoaDong.Click += (s, e) =>
            {
                if (gridToaMoi.CurrentRow != null && gridToaMoi.CurrentRow.DataBoundItem is DongThuoc dt)
                    _dsThuocMoi.Remove(dt);
            };
            var btnLuuToa = new Button { Text = "Lưu toa thuốc (transaction)", Location = new Point(180, 262), Size = new Size(220, 30), BackColor = Color.FromArgb(40, 120, 70), ForeColor = Color.White };
            btnLuuToa.Click += BtnLuuToa_Click;

            var lblToaDaLuu = new Label { Text = "Toa đã lưu trên phiếu này:", Location = new Point(10, 300), Size = new Size(250, 23), Font = new Font("Segoe UI", 8.5f, FontStyle.Bold) };
            gridToaDaLuu = new DataGridView
            {
                Location = new Point(10, 325), Size = new Size(765, 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            tab.Controls.AddRange(new Control[]
            {
                lblLoiDan, txtLoiDan, lblThuoc, cboThuoc, numSoLuong, txtLieuDung, txtTanSuat, numSoNgay, txtCachDung,
                btnThem, gridToaMoi, btnXoaDong, btnLuuToa, lblToaDaLuu, gridToaDaLuu
            });
            return tab;
        }
    }
}
