using System;
using System.Windows.Forms;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService s = new DanhMucService();

        public FrmDanhMuc() { InitializeComponent(); }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            cboNVVaiTro.Items.AddRange(new object[] { "NV kinh doanh", "NV kế toán", "NV an ninh", "NV kỹ thuật", "Quản lý" });
            cboNVVaiTro.SelectedIndex = 0;
            Tai();
        }

        private void Tai()
        {
            dgvNV.DataSource = s.LayNhanVien();
            dgvDV.DataSource = s.LayDichVu();
            dgvQD.DataSource = s.LayQuyDinhPhat();
        }

        private void H(QuanLyCaoOc.Data.KetQuaXuLy kq) { if (UiHelper.ThongBao(kq)) Tai(); }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            H(s.ThemNhanVien(txtNVMa.Text.Trim(), txtNVTen.Text.Trim(), cboNVVaiTro.Text, txtNVSDT.Text.Trim()));
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            H(s.ThemDichVu(txtDVMa.Text.Trim(), txtDVTen.Text.Trim(), txtDVDonVi.Text.Trim(), numDVGia.Value));
        }

        private void btnCapNhatGiaDV_Click(object sender, EventArgs e)
        {
            H(s.CapNhatGiaDichVu(UiHelper.Cell(dgvDV, "MaDV"), numDVGia.Value));
        }

        private void btnThemQD_Click(object sender, EventArgs e)
        {
            H(s.ThemQuyDinhPhat(txtQDMa.Text.Trim(), txtQDNoiDung.Text.Trim(), numQDMuc.Value));
        }

        private void dgvDV_SelectionChanged(object sender, EventArgs e)
        {
            string ma = UiHelper.Cell(dgvDV, "MaDV");
            if (ma == "") return;
            txtDVMa.Text = ma;
            txtDVTen.Text = UiHelper.Cell(dgvDV, "TenDV");
            txtDVDonVi.Text = UiHelper.Cell(dgvDV, "DonViTinh");
            decimal g;
            if (decimal.TryParse(UiHelper.Cell(dgvDV, "DonGia"), out g) && g <= numDVGia.Maximum) numDVGia.Value = g;
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
