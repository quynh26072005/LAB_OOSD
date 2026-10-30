using System;
using System.Windows.Forms;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain() { InitializeComponent(); }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try { new HopDongService().CapNhatTrangThai(); }   // hop dong het han -> phong Trong
            catch (Exception ex) { MessageBox.Show("Không kết nối được CSDL: " + ex.Message); }
        }

        private void Mo(Form f) { f.ShowDialog(this); f.Dispose(); }

        private void btnDanhMuc_Click(object sender, EventArgs e) { Mo(new FrmDanhMuc()); }
        private void btnTangPhong_Click(object sender, EventArgs e) { Mo(new FrmTangPhong()); }
        private void btnHopDong_Click(object sender, EventArgs e) { Mo(new FrmHopDong()); }
        private void btnThanhToan_Click(object sender, EventArgs e) { Mo(new FrmThanhToan()); }
        private void btnGiaHan_Click(object sender, EventArgs e) { Mo(new FrmGiaHan()); }
        private void btnNhanVienCT_Click(object sender, EventArgs e) { Mo(new FrmNhanVienCongTy()); }
        private void btnKiemTra_Click(object sender, EventArgs e) { Mo(new FrmKiemTra()); }
        private void btnThongKe_Click(object sender, EventArgs e) { Mo(new FrmThongKe()); }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
    }
}
