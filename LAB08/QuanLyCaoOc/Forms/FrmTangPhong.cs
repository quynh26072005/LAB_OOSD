using System;
using System.Windows.Forms;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmTangPhong : Form
    {
        private readonly TangPhongService s = new TangPhongService();

        public FrmTangPhong() { InitializeComponent(); }

        private void FrmTangPhong_Load(object sender, EventArgs e)
        {
            cboHuong.Items.AddRange(new object[] { "Đông", "Tây", "Nam", "Bắc", "Đông Nam", "Tây Bắc" });
            cboHuong.SelectedIndex = 0;
            dtTuNgay.Value = DateTime.Today;
            dtDenNgay.Value = DateTime.Today.AddMonths(6);
            Tai();
        }

        private void Tai()
        {
            dgvTang.DataSource = s.LayTang();
            dgvPhong.DataSource = s.LayPhong();
            UiHelper.NapCombo(cboTang, s.LayTang(), "TenTang", "MaTang");
        }

        private void H(KetQuaXuLy kq) { if (UiHelper.ThongBao(kq)) Tai(); }

        private void btnThemTang_Click(object sender, EventArgs e)
        {
            H(s.ThemTang(txtTangMa.Text.Trim(), txtTangTen.Text.Trim(), (int)numSoTang.Value, numDoCao.Value));
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            H(s.ThemPhong(txtPhongMa.Text.Trim(), UiHelper.GiaTri(cboTang), numDienTich.Value, cboHuong.Text, numDonGia.Value));
        }

        private void btnCapNhatGia_Click(object sender, EventArgs e)
        {
            H(s.CapNhatDonGia(UiHelper.Cell(dgvPhong, "MaPhong"), numDonGia.Value));
        }

        private void btnBaoTri_Click(object sender, EventArgs e)
        {
            H(s.DatBaoTri(UiHelper.Cell(dgvPhong, "MaPhong"), true));
        }

        private void btnBoBaoTri_Click(object sender, EventArgs e)
        {
            H(s.DatBaoTri(UiHelper.Cell(dgvPhong, "MaPhong"), false));
        }

        private void btnPhongTrong_Click(object sender, EventArgs e)
        {
            if (dtDenNgay.Value.Date < dtTuNgay.Value.Date)
            {
                MessageBox.Show("Đến ngày không được trước từ ngày.");
                return;
            }
            dgvPhong.DataSource = s.LayPhongTrong(dtTuNgay.Value, dtDenNgay.Value);
        }

        private void btnTatCa_Click(object sender, EventArgs e) { dgvPhong.DataSource = s.LayPhong(); }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
