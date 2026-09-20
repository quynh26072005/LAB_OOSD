using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Forms
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService service = new DanhMucService();
        private DataGridView dgvNV, dgvTL, dgvNXB;
        private TextBox txtNVMa, txtNVHo, txtNVTen, txtNVChucVu, txtNVSDT, txtTLMa, txtTLTen, txtNXBMa, txtNXBDiaChi, txtNXBSDT;
        private ComboBox cboNVPhai;
        private DateTimePicker dtNVNgaySinh;
        private Button btnNVThem, btnNVCapNhat, btnNVXoa, btnTLThem, btnTLCapNhat, btnTLXoa, btnNXBThem, btnNXBCapNhat, btnNXBXoa;

        public FrmDanhMuc()
        {
            InitializeComponent();
            this.Text = "Danh mục và Nhân viên";
            this.Size = new System.Drawing.Size(1020, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            KhoiTaoGiaoDien();
            LoadData();
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
        }

        private void KhoiTaoGiaoDien()
        {
            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            TabPage tabNV = new TabPage("Nhân viên");
            TabPage tabTL = new TabPage("Thể loại");
            TabPage tabNXB = new TabPage("Nhà xuất bản");

            // ================= TAB NHÂN VIÊN =================
            txtNVMa = new TextBox { Location = new System.Drawing.Point(110, 20), Width = 130 };
            txtNVHo = new TextBox { Location = new System.Drawing.Point(110, 50), Width = 130 };
            txtNVTen = new TextBox { Location = new System.Drawing.Point(110, 80), Width = 130 };

            cboNVPhai = new ComboBox { Location = new System.Drawing.Point(340, 20), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cboNVPhai.Items.AddRange(new object[] { "Nam", "Nữ" }); cboNVPhai.SelectedIndex = 0;
            dtNVNgaySinh = new DateTimePicker { Location = new System.Drawing.Point(340, 50), Width = 140, Format = DateTimePickerFormat.Short };
            txtNVChucVu = new TextBox { Location = new System.Drawing.Point(340, 80), Width = 140 };

            txtNVSDT = new TextBox { Location = new System.Drawing.Point(550, 20), Width = 130 };

            btnNVThem = new Button { Text = "Thêm", Location = new System.Drawing.Point(710, 20), Width = 85 };
            btnNVCapNhat = new Button { Text = "Cập nhật", Location = new System.Drawing.Point(805, 20), Width = 85 };
            btnNVXoa = new Button { Text = "Xóa", Location = new System.Drawing.Point(710, 60), Width = 85 };
            Button btnNVMoi = new Button { Text = "Làm mới", Location = new System.Drawing.Point(805, 60), Width = 85 };

            dgvNV = new DataGridView { Location = new System.Drawing.Point(20, 130), Size = new System.Drawing.Size(950, 420), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            tabNV.Controls.AddRange(new Control[] {
                new Label{Text="Mã NV:", Location=new System.Drawing.Point(20,20), AutoSize=true}, txtNVMa,
                new Label{Text="Họ:", Location=new System.Drawing.Point(20,50), AutoSize=true}, txtNVHo,
                new Label{Text="Tên:", Location=new System.Drawing.Point(20,80), AutoSize=true}, txtNVTen,
                new Label{Text="Phái:", Location=new System.Drawing.Point(260,20), AutoSize=true}, cboNVPhai,
                new Label{Text="Ngày sinh:", Location=new System.Drawing.Point(260,50), AutoSize=true}, dtNVNgaySinh,
                new Label{Text="Chức vụ:", Location=new System.Drawing.Point(260,80), AutoSize=true}, txtNVChucVu,
                new Label{Text="SĐT:", Location=new System.Drawing.Point(500,20), AutoSize=true}, txtNVSDT,
                btnNVThem, btnNVCapNhat, btnNVXoa, btnNVMoi, dgvNV
            });

            // ================= TAB THỂ LOẠI =================
            txtTLMa = new TextBox { Location = new System.Drawing.Point(130, 20), Width = 220 };
            txtTLTen = new TextBox { Location = new System.Drawing.Point(130, 60), Width = 250 };

            btnTLThem = new Button { Text = "Thêm", Location = new System.Drawing.Point(420, 20), Width = 90 };
            btnTLCapNhat = new Button { Text = "Cập nhật", Location = new System.Drawing.Point(520, 20), Width = 90 };
            btnTLXoa = new Button { Text = "Xóa", Location = new System.Drawing.Point(420, 60), Width = 90 };
            Button btnTLMoi = new Button { Text = "Làm mới", Location = new System.Drawing.Point(520, 60), Width = 90 };

            dgvTL = new DataGridView { Location = new System.Drawing.Point(20, 110), Size = new System.Drawing.Size(950, 440), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            tabTL.Controls.AddRange(new Control[] {
                new Label{Text="Mã thể loại:", Location=new System.Drawing.Point(20,20), AutoSize=true}, txtTLMa,
                new Label{Text="Tên thể loại:", Location=new System.Drawing.Point(20,60), AutoSize=true}, txtTLTen,
                btnTLThem, btnTLCapNhat, btnTLXoa, btnTLMoi, dgvTL
            });

            // ================= TAB NHÀ XUẤT BẢN =================
            txtNXBMa = new TextBox { Location = new System.Drawing.Point(130, 20), Width = 220 };
            txtNXBDiaChi = new TextBox { Location = new System.Drawing.Point(130, 60), Width = 250 };
            txtNXBSDT = new TextBox { Location = new System.Drawing.Point(460, 20), Width = 150 };

            btnNXBThem = new Button { Text = "Thêm", Location = new System.Drawing.Point(650, 20), Width = 90 };
            btnNXBCapNhat = new Button { Text = "Cập nhật", Location = new System.Drawing.Point(750, 20), Width = 90 };
            btnNXBXoa = new Button { Text = "Xóa", Location = new System.Drawing.Point(650, 60), Width = 90 };
            Button btnNXBMoi = new Button { Text = "Làm mới", Location = new System.Drawing.Point(750, 60), Width = 90 };

            dgvNXB = new DataGridView { Location = new System.Drawing.Point(20, 110), Size = new System.Drawing.Size(950, 440), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            tabNXB.Controls.AddRange(new Control[] {
                new Label{Text="Mã NXB:", Location=new System.Drawing.Point(20,20), AutoSize=true}, txtNXBMa,
                new Label{Text="Địa chỉ:", Location=new System.Drawing.Point(20,60), AutoSize=true}, txtNXBDiaChi,
                new Label{Text="SĐT:", Location=new System.Drawing.Point(400,20), AutoSize=true}, txtNXBSDT,
                btnNXBThem, btnNXBCapNhat, btnNXBXoa, btnNXBMoi, dgvNXB
            });

            tabs.TabPages.AddRange(new TabPage[] { tabNV, tabTL, tabNXB });
            Controls.Add(tabs);

            // Xử lý sự kiện
            btnNVThem.Click += (s, e) => ShowRes(service.LuuNhanVien(new NhanVien { MaNhanVien = txtNVMa.Text.Trim(), Ho = txtNVHo.Text.Trim(), Ten = txtNVTen.Text.Trim(), Phai = Convert.ToString(cboNVPhai.SelectedItem), NgaySinh = dtNVNgaySinh.Value, ChucVu = txtNVChucVu.Text.Trim(), SoDienThoai = txtNVSDT.Text.Trim() }, false));
            btnNVCapNhat.Click += (s, e) => ShowRes(service.LuuNhanVien(new NhanVien { MaNhanVien = txtNVMa.Text.Trim(), Ho = txtNVHo.Text.Trim(), Ten = txtNVTen.Text.Trim(), Phai = Convert.ToString(cboNVPhai.SelectedItem), NgaySinh = dtNVNgaySinh.Value, ChucVu = txtNVChucVu.Text.Trim(), SoDienThoai = txtNVSDT.Text.Trim() }, true));
            btnNVXoa.Click += (s, e) => ShowRes(service.Xoa("NhanVien", "MaNhanVien", txtNVMa.Text.Trim()));
            btnNVMoi.Click += (s, e) => { txtNVMa.Clear(); txtNVHo.Clear(); txtNVTen.Clear(); txtNVChucVu.Clear(); txtNVSDT.Clear(); txtNVMa.ReadOnly = false; };

            dgvNV.SelectionChanged += (s, e) => {
                if (dgvNV.CurrentRow == null) return;
                DataRowView r = dgvNV.CurrentRow.DataBoundItem as DataRowView; if (r == null) return;
                txtNVMa.Text = Convert.ToString(r["MaNhanVien"]); txtNVHo.Text = Convert.ToString(r["Ho"]); txtNVTen.Text = Convert.ToString(r["Ten"]); cboNVPhai.SelectedItem = Convert.ToString(r["Phai"]); dtNVNgaySinh.Value = Convert.ToDateTime(r["NgaySinh"]); txtNVChucVu.Text = Convert.ToString(r["ChucVu"]); txtNVSDT.Text = Convert.ToString(r["SoDienThoai"]); txtNVMa.ReadOnly = true;
            };

            btnTLThem.Click += (s, e) => ShowRes(service.LuuTheLoai(txtTLMa.Text, txtTLTen.Text, false));
            btnTLCapNhat.Click += (s, e) => ShowRes(service.LuuTheLoai(txtTLMa.Text, txtTLTen.Text, true));
            btnTLXoa.Click += (s, e) => ShowRes(service.Xoa("TheLoai", "MaTheLoai", txtTLMa.Text.Trim()));
            btnTLMoi.Click += (s, e) => { txtTLMa.Clear(); txtTLTen.Clear(); txtTLMa.ReadOnly = false; };
            dgvTL.SelectionChanged += (s, e) => { if (dgvTL.CurrentRow == null) return; DataRowView r = dgvTL.CurrentRow.DataBoundItem as DataRowView; if (r == null) return; txtTLMa.Text = Convert.ToString(r["MaTheLoai"]); txtTLTen.Text = Convert.ToString(r["TenTheLoai"]); txtTLMa.ReadOnly = true; };

            btnNXBThem.Click += (s, e) => ShowRes(service.LuuNhaXuatBan(txtNXBMa.Text, txtNXBDiaChi.Text, txtNXBSDT.Text, false));
            btnNXBCapNhat.Click += (s, e) => ShowRes(service.LuuNhaXuatBan(txtNXBMa.Text, txtNXBDiaChi.Text, txtNXBSDT.Text, true));
            btnNXBXoa.Click += (s, e) => ShowRes(service.Xoa("NhaXuatBan", "MaNhaXuatBan", txtNXBMa.Text.Trim()));
            btnNXBMoi.Click += (s, e) => { txtNXBMa.Clear(); txtNXBDiaChi.Clear(); txtNXBSDT.Clear(); txtNXBMa.ReadOnly = false; };
            dgvNXB.SelectionChanged += (s, e) => { if (dgvNXB.CurrentRow == null) return; DataRowView r = dgvNXB.CurrentRow.DataBoundItem as DataRowView; if (r == null) return; txtNXBMa.Text = Convert.ToString(r["MaNhaXuatBan"]); txtNXBDiaChi.Text = Convert.ToString(r["DiaChi"]); txtNXBSDT.Text = Convert.ToString(r["SoDienThoai"]); txtNXBMa.ReadOnly = true; };
        }

        private void LoadData()
        {
            dgvNV.DataSource = service.LayNhanVien();
            dgvTL.DataSource = service.LayTheLoai();
            dgvNXB.DataSource = service.LayNhaXuatBan();
        }

        private void ShowRes(KetQuaXuLy kq)
        {
            MessageBox.Show(kq.ThongBao, kq.ThanhCong ? "Thông báo" : "Lỗi");
            if (kq.ThanhCong) LoadData();
        }
    }
}