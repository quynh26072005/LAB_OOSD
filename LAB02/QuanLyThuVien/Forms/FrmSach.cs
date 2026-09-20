using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Forms
{
    public partial class FrmSach : Form
    {
        private readonly SachService service = new SachService();
        private readonly DanhMucService danhMuc = new DanhMucService();
        private TextBox txtMa, txtTen, txtTim;
        private NumericUpDown numNam, numSoLuong;
        private ComboBox cboTheLoai, cboNXB;
        private DataGridView dgvSach;

        public FrmSach()
        {
            InitializeComponent();
            this.Text = "Quản lý đầu sách";
            this.Size = new System.Drawing.Size(1020, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            KhoiTaoUI();
            LoadData();
        }

        private void FrmSach_Load(object sender, EventArgs e)
        {
        }

        private void KhoiTaoUI()
        {
            // CỘT 1: Nhãn X=20, Ô nhập liệu lùi hẳn sang X=140
            txtMa = new TextBox { Location = new System.Drawing.Point(140, 20), Width = 200 };
            txtTen = new TextBox { Location = new System.Drawing.Point(140, 50), Width = 200 };
            numNam = new NumericUpDown { Location = new System.Drawing.Point(140, 80), Width = 120, Minimum = 1000, Maximum = 3000, Value = DateTime.Today.Year };
            numSoLuong = new NumericUpDown { Location = new System.Drawing.Point(140, 110), Width = 120, Maximum = 10000 };

            // CỘT 2: Nhãn X=370, Ô nhập liệu lùi hẳn sang X=520 (Cách nhãn 150px)
            cboTheLoai = new ComboBox { Location = new System.Drawing.Point(520, 20), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cboNXB = new ComboBox { Location = new System.Drawing.Point(520, 50), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            txtTim = new TextBox { Location = new System.Drawing.Point(520, 110), Width = 130 };
            Button btnTim = new Button { Text = "Tìm", Location = new System.Drawing.Point(660, 108), Width = 60 };

            // NÚT THAO TÁC
            Button btnThem = new Button { Text = "Thêm", Location = new System.Drawing.Point(750, 20), Width = 90 };
            Button btnCapNhat = new Button { Text = "Cập nhật", Location = new System.Drawing.Point(850, 20), Width = 90 };
            Button btnXoa = new Button { Text = "Xóa", Location = new System.Drawing.Point(750, 60), Width = 90 };
            Button btnMoi = new Button { Text = "Làm mới", Location = new System.Drawing.Point(850, 60), Width = 90 };

            dgvSach = new DataGridView { Location = new System.Drawing.Point(20, 160), Size = new System.Drawing.Size(960, 380), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            // Tạo các Label
            Label lblMa = new Label { Text = "Mã đầu sách:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            Label lblTen = new Label { Text = "Tên sách:", Location = new System.Drawing.Point(20, 50), AutoSize = true };
            Label lblNam = new Label { Text = "Năm XB:", Location = new System.Drawing.Point(20, 80), AutoSize = true };
            Label lblSL = new Label { Text = "Số lượng:", Location = new System.Drawing.Point(20, 110), AutoSize = true };

            Label lblTL = new Label { Text = "Thể loại:", Location = new System.Drawing.Point(370, 20), AutoSize = true };
            Label lblNXB = new Label { Text = "Nhà xuất bản:", Location = new System.Drawing.Point(370, 50), AutoSize = true };
            Label lblTim = new Label { Text = "Từ khóa:", Location = new System.Drawing.Point(370, 110), AutoSize = true };

            // Thêm vào Form (Ô nhập liệu thêm trước để đảm bảo nằm trên Z-Order)
            Controls.AddRange(new Control[] {
                txtMa, txtTen, numNam, numSoLuong,
                cboTheLoai, cboNXB, txtTim,
                lblMa, lblTen, lblNam, lblSL, lblTL, lblNXB, lblTim,
                btnTim, btnThem, btnCapNhat, btnXoa, btnMoi, dgvSach
            });

            cboTheLoai.DataSource = danhMuc.LayTheLoai();
            cboTheLoai.DisplayMember = "TenTheLoai";
            cboTheLoai.ValueMember = "MaTheLoai";

            cboNXB.DataSource = danhMuc.LayNhaXuatBan();
            cboNXB.DisplayMember = "MaNhaXuatBan";
            cboNXB.ValueMember = "MaNhaXuatBan";

            btnTim.Click += (s, e) => LoadData();
            btnThem.Click += (s, e) => ShowRes(service.Luu(GetForm(), false));
            btnCapNhat.Click += (s, e) => ShowRes(service.Luu(GetForm(), true));
            btnXoa.Click += (s, e) => ShowRes(service.Xoa(txtMa.Text.Trim()));
            btnMoi.Click += (s, e) => { txtMa.Clear(); txtTen.Clear(); txtMa.ReadOnly = false; };

            dgvSach.SelectionChanged += (s, e) => {
                if (dgvSach.CurrentRow == null) return;
                DataRowView r = dgvSach.CurrentRow.DataBoundItem as DataRowView;
                if (r == null) return;
                txtMa.Text = Convert.ToString(r["MaDauSach"]);
                txtTen.Text = Convert.ToString(r["TenSach"]);
                numNam.Value = Convert.ToDecimal(r["NamXuatBan"]);
                numSoLuong.Value = Convert.ToDecimal(r["SoLuongHienCo"]);
                cboTheLoai.SelectedValue = Convert.ToString(r["MaTheLoai"]);
                cboNXB.SelectedValue = Convert.ToString(r["MaNhaXuatBan"]);
                txtMa.ReadOnly = true;
            };
        }

        private DauSach GetForm()
        {
            return new DauSach
            {
                MaDauSach = txtMa.Text.Trim(),
                TenSach = txtTen.Text.Trim(),
                NamXuatBan = (int)numNam.Value,
                SoLuongHienCo = (int)numSoLuong.Value,
                MaTheLoai = Convert.ToString(cboTheLoai.SelectedValue),
                MaNhaXuatBan = Convert.ToString(cboNXB.SelectedValue)
            };
        }

        private void LoadData() { dgvSach.DataSource = service.LayDanhSach(txtTim.Text.Trim()); }
        private void ShowRes(KetQuaXuLy kq) { MessageBox.Show(kq.ThongBao, kq.ThanhCong ? "Thông báo" : "Lỗi"); if (kq.ThanhCong) LoadData(); }
    }
}