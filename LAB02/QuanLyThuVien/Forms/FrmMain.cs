using System;
using System.Windows.Forms;

namespace QuanLyThuVien.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Text = "Quản lý thư viện";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 800;
            this.Height = 500;

            Label lblTitle = new Label { Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN", Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold), AutoSize = true, Location = new System.Drawing.Point(220, 30) };
            Button btnDanhMuc = new Button { Text = "Danh mục / Nhân viên", Location = new System.Drawing.Point(100, 100), Size = new System.Drawing.Size(240, 60) };
            Button btnSach = new Button { Text = "Quản lý đầu sách", Location = new System.Drawing.Point(420, 100), Size = new System.Drawing.Size(240, 60) };
            Button btnDocGia = new Button { Text = "Độc giả và thẻ", Location = new System.Drawing.Point(100, 190), Size = new System.Drawing.Size(240, 60) };
            Button btnMuonTra = new Button { Text = "Mượn - Trả sách", Location = new System.Drawing.Point(420, 190), Size = new System.Drawing.Size(240, 60) };
            Button btnThongKe = new Button { Text = "Thống kê", Location = new System.Drawing.Point(100, 280), Size = new System.Drawing.Size(240, 60) };
            Button btnThoat = new Button { Text = "Thoát", Location = new System.Drawing.Point(420, 280), Size = new System.Drawing.Size(240, 60) };

            btnDanhMuc.Click += (s, e) => { using (FrmDanhMuc f = new FrmDanhMuc()) f.ShowDialog(this); };
            btnSach.Click += (s, e) => { using (FrmSach f = new FrmSach()) f.ShowDialog(this); };
            btnDocGia.Click += (s, e) => { using (FrmDocGia f = new FrmDocGia()) f.ShowDialog(this); };
            btnMuonTra.Click += (s, e) => { using (FrmMuonTra f = new FrmMuonTra()) f.ShowDialog(this); };
            btnThongKe.Click += (s, e) => { using (FrmThongKe f = new FrmThongKe()) f.ShowDialog(this); };
            btnThoat.Click += (s, e) => { if (MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) Close(); };

            Controls.AddRange(new Control[] { lblTitle, btnDanhMuc, btnSach, btnDocGia, btnMuonTra, btnThongKe, btnThoat });
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }
    }
}