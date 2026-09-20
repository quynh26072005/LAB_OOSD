using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Forms
{
    public partial class FrmDocGia : Form
    {
        private readonly DocGiaService service = new DocGiaService();
        private TextBox txtMa, txtHo, txtTen, txtSDT, txtDiaChi, txtEmail;
        private ComboBox cboPhai;
        private DateTimePicker dtNgaySinh, dtNgayCap, dtHan;
        private CheckBox chkLePhi;
        private DataGridView dgvDocGia;

        public FrmDocGia()
        {
            InitializeComponent();
            this.Text = "Độc giả và thẻ thư viện";
            this.Size = new System.Drawing.Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            KhoiTaoUI();
            LoadData();
        }

        private void FrmDocGia_Load(object sender, EventArgs e)
        {
        }

        private void KhoiTaoUI()
        {
            txtMa = new TextBox { Location = new System.Drawing.Point(110, 20), Width = 140 };
            txtHo = new TextBox { Location = new System.Drawing.Point(110, 50), Width = 140 };
            txtTen = new TextBox { Location = new System.Drawing.Point(110, 80), Width = 140 };
            dtNgaySinh = new DateTimePicker { Location = new System.Drawing.Point(110, 110), Width = 140, Format = DateTimePickerFormat.Short };
            cboPhai = new ComboBox { Location = new System.Drawing.Point(110, 140), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            cboPhai.Items.AddRange(new object[] { "Nam", "Nữ" }); cboPhai.SelectedIndex = 0;

            txtSDT = new TextBox { Location = new System.Drawing.Point(360, 20), Width = 160 };
            txtDiaChi = new TextBox { Location = new System.Drawing.Point(360, 50), Width = 160 };
            txtEmail = new TextBox { Location = new System.Drawing.Point(360, 80), Width = 160 };

            dtNgayCap = new DateTimePicker { Location = new System.Drawing.Point(360, 110), Width = 160, Format = DateTimePickerFormat.Short };
            dtHan = new DateTimePicker { Location = new System.Drawing.Point(360, 140), Width = 160, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddYears(1) };
            chkLePhi = new CheckBox { Text = "Đã đóng lệ phí", Location = new System.Drawing.Point(540, 110), AutoSize = true, Checked = true };

            // Nhóm Nút Thao tác CRUD Đầy Đủ
            Button btnThem = new Button { Text = "Thêm độc giả", Location = new System.Drawing.Point(680, 20), Width = 120 };
            Button btnCapNhat = new Button { Text = "Cập nhật", Location = new System.Drawing.Point(810, 20), Width = 110 };
            Button btnXoa = new Button { Text = "Xóa", Location = new System.Drawing.Point(930, 20), Width = 90 };

            Button btnCapThe = new Button { Text = "Cấp thẻ mới", Location = new System.Drawing.Point(680, 60), Width = 120 };
            Button btnGiaHan = new Button { Text = "Gia hạn thẻ", Location = new System.Drawing.Point(810, 60), Width = 110 };
            Button btnMoi = new Button { Text = "Làm mới", Location = new System.Drawing.Point(930, 60), Width = 90 };

            dgvDocGia = new DataGridView { Location = new System.Drawing.Point(20, 180), Size = new System.Drawing.Size(1040, 410), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            Controls.AddRange(new Control[] {
                new Label{Text="Mã độc giả:", Location=new System.Drawing.Point(20,20), AutoSize=true}, txtMa,
                new Label{Text="Họ:", Location=new System.Drawing.Point(20,50), AutoSize=true}, txtHo,
                new Label{Text="Tên:", Location=new System.Drawing.Point(20,80), AutoSize=true}, txtTen,
                new Label{Text="Ngày sinh:", Location=new System.Drawing.Point(20,110), AutoSize=true}, dtNgaySinh,
                new Label{Text="Phái:", Location=new System.Drawing.Point(20,140), AutoSize=true}, cboPhai,
                new Label{Text="SĐT:", Location=new System.Drawing.Point(280,20), AutoSize=true}, txtSDT,
                new Label{Text="Địa chỉ:", Location=new System.Drawing.Point(280,50), AutoSize=true}, txtDiaChi,
                new Label{Text="Email:", Location=new System.Drawing.Point(280,80), AutoSize=true}, txtEmail,
                new Label{Text="Ngày cấp:", Location=new System.Drawing.Point(280,110), AutoSize=true}, dtNgayCap,
                new Label{Text="Hạn thẻ:", Location=new System.Drawing.Point(280,140), AutoSize=true}, dtHan,
                chkLePhi, btnThem, btnCapNhat, btnXoa, btnCapThe, btnGiaHan, btnMoi, dgvDocGia
            });

            btnThem.Click += (s, e) => ShowRes(service.Luu(GetForm(), false));
            btnCapNhat.Click += (s, e) => ShowRes(service.Luu(GetForm(), true));
            btnXoa.Click += (s, e) => ShowRes(service.Xoa(txtMa.Text.Trim()));
            btnCapThe.Click += (s, e) => ShowRes(service.CapThe(txtMa.Text.Trim(), dtNgayCap.Value, dtHan.Value, chkLePhi.Checked));
            btnGiaHan.Click += (s, e) => ShowRes(service.GiaHanThe(txtMa.Text.Trim(), dtHan.Value, chkLePhi.Checked));
            btnMoi.Click += (s, e) => { txtMa.Clear(); txtHo.Clear(); txtTen.Clear(); txtSDT.Clear(); txtDiaChi.Clear(); txtEmail.Clear(); txtMa.ReadOnly = false; };

            dgvDocGia.SelectionChanged += (s, e) => {
                if (dgvDocGia.CurrentRow == null || dgvDocGia.CurrentRow.Index < 0) return;
                DataRowView r = dgvDocGia.CurrentRow.DataBoundItem as DataRowView;
                if (r == null) return;
                txtMa.Text = Convert.ToString(r["MaDocGia"]);
                txtHo.Text = Convert.ToString(r["Ho"]);
                txtTen.Text = Convert.ToString(r["Ten"]);
                if (r["NgaySinh"] != DBNull.Value) dtNgaySinh.Value = Convert.ToDateTime(r["NgaySinh"]);
                if (r["Phai"] != DBNull.Value)
                {
                    string phaiVal = Convert.ToString(r["Phai"]);
                    if (phaiVal == "True" || phaiVal == "1") cboPhai.SelectedItem = "Nam";
                    else if (phaiVal == "False" || phaiVal == "0") cboPhai.SelectedItem = "Nữ";
                    else cboPhai.SelectedItem = phaiVal;
                }
                txtSDT.Text = Convert.ToString(r["SoDienThoai"]);
                txtDiaChi.Text = Convert.ToString(r["DiaChi"]);
                txtEmail.Text = Convert.ToString(r["Email"]);
                if (r["NgayCap"] != DBNull.Value) dtNgayCap.Value = Convert.ToDateTime(r["NgayCap"]);
                if (r["HanSuDung"] != DBNull.Value) dtHan.Value = Convert.ToDateTime(r["HanSuDung"]);
                if (r["DaDongLePhi"] != DBNull.Value) chkLePhi.Checked = Convert.ToBoolean(r["DaDongLePhi"]);
                txtMa.ReadOnly = true;
            };
        }

        private DocGia GetForm()
        {
            return new DocGia
            {
                MaDocGia = txtMa.Text.Trim(),
                Ho = txtHo.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                NgaySinh = dtNgaySinh.Value,
                Phai = Convert.ToString(cboPhai.SelectedItem),
                SoDienThoai = txtSDT.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
        }

        private void LoadData() { dgvDocGia.DataSource = service.LayDanhSach(); }
        private void ShowRes(KetQuaXuLy kq) { MessageBox.Show(kq.ThongBao, kq.ThanhCong ? "Thông báo" : "Lỗi"); if (kq.ThanhCong) LoadData(); }
    }
}