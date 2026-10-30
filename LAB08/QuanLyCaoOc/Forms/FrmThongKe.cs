using System;
using System.Windows.Forms;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmThongKe : Form
    {
        private readonly ThongKeService s = new ThongKeService();

        public FrmThongKe() { InitializeComponent(); }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            dtTu.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtDen.Value = DateTime.Today;
            dgvTinhTrang.DataSource = s.TinhTrangPhong();
            dgvCongNo.DataSource = s.CongNo();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            if (dtDen.Value.Date < dtTu.Value.Date)
            {
                MessageBox.Show("Đến ngày không được trước từ ngày.");
                return;
            }
            dgvTongHop.DataSource = s.TongHop(dtTu.Value, dtDen.Value);
            dgvThang.DataSource = s.DoanhThuTheoThang(dtTu.Value, dtDen.Value);
            dgvTinhTrang.DataSource = s.TinhTrangPhong();
            dgvCongNo.DataSource = s.CongNo();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
