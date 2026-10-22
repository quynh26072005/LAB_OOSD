using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView gridKhoa;
        private TextBox txtTenKhoa, txtDienThoaiKhoa, txtViTriKhoa;

        private DataGridView gridThuoc;
        private TextBox txtTenThuoc, txtHamLuong, txtDonViTinh;
        private NumericUpDown numGiaThuoc;

        private DataGridView gridXetNghiem;
        private TextBox txtTenXN, txtDonViXN, txtMoTaXN;
        private NumericUpDown numGiaXN;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private TabControl InitializeComponent()
        {
            var tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(TabKhoa());
            tabs.TabPages.Add(TabThuoc());
            tabs.TabPages.Add(TabXetNghiem());
            Controls.Add(tabs);
            return tabs;
        }

        private TabPage TabKhoa()
        {
            var tab = new TabPage("Khoa");
            gridKhoa = new DataGridView { Dock = DockStyle.Top, Height = 280, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            gridKhoa.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                var r = gridKhoa.Rows[e.RowIndex];
                txtTenKhoa.Text = r.Cells["TenKhoa"].Value?.ToString();
                txtDienThoaiKhoa.Text = r.Cells["DienThoai"].Value?.ToString();
                txtViTriKhoa.Text = r.Cells["ViTri"].Value?.ToString();
                tab.Tag = Convert.ToInt32(r.Cells["MaKhoa"].Value);
            };

            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            var l1 = new Label { Text = "Tên khoa:", Location = new Point(10, 15), Size = new Size(90, 23) };
            txtTenKhoa = new TextBox { Location = new Point(110, 12), Size = new Size(220, 23) };
            var l2 = new Label { Text = "Điện thoại:", Location = new Point(10, 48), Size = new Size(90, 23) };
            txtDienThoaiKhoa = new TextBox { Location = new Point(110, 45), Size = new Size(220, 23) };
            var l3 = new Label { Text = "Vị trí:", Location = new Point(10, 81), Size = new Size(90, 23) };
            txtViTriKhoa = new TextBox { Location = new Point(110, 78), Size = new Size(220, 23) };

            var btnThem = new Button { Text = "Thêm mới", Location = new Point(10, 115), Size = new Size(100, 30) };
            btnThem.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTenKhoa.Text)) return;
                try { _service.ThemKhoa(txtTenKhoa.Text, txtDienThoaiKhoa.Text, txtViTriKhoa.Text); TaiKhoa(); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            };
            var btnSua = new Button { Text = "Cập nhật", Location = new Point(120, 115), Size = new Size(100, 30) };
            btnSua.Click += (s, e) =>
            {
                if (tab.Tag == null) { MessageBox.Show("Vui lòng chọn khoa cần sửa.", "Thông báo"); return; }
                try { _service.CapNhatKhoa((int)tab.Tag, txtTenKhoa.Text, txtDienThoaiKhoa.Text, txtViTriKhoa.Text); TaiKhoa(); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            };

            panel.Controls.AddRange(new Control[] { l1, txtTenKhoa, l2, txtDienThoaiKhoa, l3, txtViTriKhoa, btnThem, btnSua });
            tab.Controls.Add(panel);
            tab.Controls.Add(gridKhoa);
            return tab;
        }

        private TabPage TabThuoc()
        {
            var tab = new TabPage("Thuốc");
            gridThuoc = new DataGridView { Dock = DockStyle.Top, Height = 280, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            gridThuoc.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                var r = gridThuoc.Rows[e.RowIndex];
                txtTenThuoc.Text = r.Cells["TenThuoc"].Value?.ToString();
                txtHamLuong.Text = r.Cells["HamLuong"].Value?.ToString();
                txtDonViTinh.Text = r.Cells["DonViTinh"].Value?.ToString();
                numGiaThuoc.Value = Convert.ToDecimal(r.Cells["DonGia"].Value);
                tab.Tag = Convert.ToInt32(r.Cells["MaThuoc"].Value);
            };

            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            var l1 = new Label { Text = "Tên thuốc:", Location = new Point(10, 15), Size = new Size(90, 23) };
            txtTenThuoc = new TextBox { Location = new Point(110, 12), Size = new Size(220, 23) };
            var l2 = new Label { Text = "Hàm lượng:", Location = new Point(10, 48), Size = new Size(90, 23) };
            txtHamLuong = new TextBox { Location = new Point(110, 45), Size = new Size(220, 23) };
            var l3 = new Label { Text = "Đơn vị tính:", Location = new Point(10, 81), Size = new Size(90, 23) };
            txtDonViTinh = new TextBox { Location = new Point(110, 78), Size = new Size(220, 23) };
            var l4 = new Label { Text = "Đơn giá:", Location = new Point(10, 114), Size = new Size(90, 23) };
            numGiaThuoc = new NumericUpDown { Location = new Point(110, 111), Size = new Size(220, 23), Maximum = 100000000, ThousandsSeparator = true };

            var btnThem = new Button { Text = "Thêm mới", Location = new Point(10, 148), Size = new Size(100, 30) };
            btnThem.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTenThuoc.Text)) return;
                _service.ThemThuoc(txtTenThuoc.Text, txtHamLuong.Text, txtDonViTinh.Text, numGiaThuoc.Value);
                TaiThuoc();
            };
            var btnSua = new Button { Text = "Cập nhật", Location = new Point(120, 148), Size = new Size(100, 30) };
            btnSua.Click += (s, e) =>
            {
                if (tab.Tag == null) { MessageBox.Show("Vui lòng chọn thuốc cần sửa.", "Thông báo"); return; }
                _service.CapNhatThuoc((int)tab.Tag, txtTenThuoc.Text, txtHamLuong.Text, txtDonViTinh.Text, numGiaThuoc.Value, "ConKinhDoanh");
                TaiThuoc();
            };

            panel.Controls.AddRange(new Control[] { l1, txtTenThuoc, l2, txtHamLuong, l3, txtDonViTinh, l4, numGiaThuoc, btnThem, btnSua });
            tab.Controls.Add(panel);
            tab.Controls.Add(gridThuoc);
            return tab;
        }

        private TabPage TabXetNghiem()
        {
            var tab = new TabPage("Xét nghiệm");
            gridXetNghiem = new DataGridView { Dock = DockStyle.Top, Height = 280, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            gridXetNghiem.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                var r = gridXetNghiem.Rows[e.RowIndex];
                txtTenXN.Text = r.Cells["TenXetNghiem"].Value?.ToString();
                txtDonViXN.Text = r.Cells["DonVi"].Value?.ToString();
                txtMoTaXN.Text = r.Cells["MoTa"].Value?.ToString();
                numGiaXN.Value = Convert.ToDecimal(r.Cells["DonGia"].Value);
                tab.Tag = Convert.ToInt32(r.Cells["MaXetNghiem"].Value);
            };

            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            var l1 = new Label { Text = "Tên xét nghiệm:", Location = new Point(10, 15), Size = new Size(100, 23) };
            txtTenXN = new TextBox { Location = new Point(120, 12), Size = new Size(220, 23) };
            var l2 = new Label { Text = "Đơn vị:", Location = new Point(10, 48), Size = new Size(100, 23) };
            txtDonViXN = new TextBox { Location = new Point(120, 45), Size = new Size(220, 23) };
            var l3 = new Label { Text = "Mô tả:", Location = new Point(10, 81), Size = new Size(100, 23) };
            txtMoTaXN = new TextBox { Location = new Point(120, 78), Size = new Size(220, 23) };
            var l4 = new Label { Text = "Đơn giá:", Location = new Point(10, 114), Size = new Size(100, 23) };
            numGiaXN = new NumericUpDown { Location = new Point(120, 111), Size = new Size(220, 23), Maximum = 100000000, ThousandsSeparator = true };

            var btnThem = new Button { Text = "Thêm mới", Location = new Point(10, 148), Size = new Size(100, 30) };
            btnThem.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTenXN.Text)) return;
                _service.ThemXetNghiem(txtTenXN.Text, txtDonViXN.Text, txtMoTaXN.Text, numGiaXN.Value);
                TaiXetNghiem();
            };
            var btnSua = new Button { Text = "Cập nhật", Location = new Point(120, 148), Size = new Size(100, 30) };
            btnSua.Click += (s, e) =>
            {
                if (tab.Tag == null) { MessageBox.Show("Vui lòng chọn xét nghiệm cần sửa.", "Thông báo"); return; }
                _service.CapNhatXetNghiem((int)tab.Tag, txtTenXN.Text, txtDonViXN.Text, txtMoTaXN.Text, numGiaXN.Value);
                TaiXetNghiem();
            };

            panel.Controls.AddRange(new Control[] { l1, txtTenXN, l2, txtDonViXN, l3, txtMoTaXN, l4, numGiaXN, btnThem, btnSua });
            tab.Controls.Add(panel);
            tab.Controls.Add(gridXetNghiem);
            return tab;
        }
    }
}
