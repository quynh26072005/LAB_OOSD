using System;
using System.Windows.Forms;

namespace QuanLyNhanSu.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnDanhMuc_Click(object sender, EventArgs e) => ShowForm(new DanhMucForm());
        private void BtnNhanVien_Click(object sender, EventArgs e) => ShowForm(new NhanVienForm());
        private void BtnQuyetDinh_Click(object sender, EventArgs e) => ShowForm(new QuyetDinhForm());
        private void BtnHopDong_Click(object sender, EventArgs e) => ShowForm(new HopDongForm());
        private void BtnChamCong_Click(object sender, EventArgs e) => ShowForm(new ChamCongForm());
        private void BtnBangLuong_Click(object sender, EventArgs e) => ShowForm(new BangLuongForm());
        private void BtnKhenThuong_Click(object sender, EventArgs e) => ShowForm(new KhenThuongKyLuatForm());

        private static void ShowForm(Form form)
        {
            form.ShowDialog();
        }
    }
}