using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmGiaHan : Form
    {
        private readonly GiaHanService s = new GiaHanService();
        private readonly HopDongService hd = new HopDongService();
        private readonly DanhMucService dm = new DanhMucService();

        public FrmGiaHan() { InitializeComponent(); }

        private void FrmGiaHan_Load(object sender, EventArgs e)
        {
            UiHelper.NapCombo(cboNV, dm.LayNhanVien(), "HoTen", "MaNV");
            UiHelper.NapCombo(cboHopDong, hd.LayHopDong(), "SoHopDong", "SoHopDong");
            dtNgayLap.Value = DateTime.Today;
            dgvPhieu.DataSource = s.LayPhieuGiaHan();
            cboHopDong_SelectedIndexChanged(null, null);
        }

        private void cboHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string so = UiHelper.GiaTri(cboHopDong);
            if (so != "") dgvPhong.DataSource = hd.LayChiTiet(so);
        }

        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            int soThang;
            if (int.TryParse(UiHelper.Cell(dgvPhong, "SoThang"), out soThang))
            {
                lblToiDa.Text = "Gia hạn tối đa: " + (soThang / 2) + " tháng (≤ 1/2 thời gian thuê " + soThang + " tháng)";
                numSoThangGH.Maximum = Math.Max(1, soThang / 2);
            }
            lblNoThue.Text = lblNoDV.Text = lblKetLuan.Text = "";
        }

        // Kiem tra truoc khi lap phieu: khong con no tien thue + da thanh toan day du dich vu
        private void btnKiemTraNo_Click(object sender, EventArgs e)
        {
            string so = UiHelper.GiaTri(cboHopDong), phong = UiHelper.Cell(dgvPhong, "MaPhong");
            if (so == "" || phong == "") { MessageBox.Show("Chọn hợp đồng và phòng cần gia hạn."); return; }
            CongNoPhong no = s.TinhCongNo(so, phong, dtNgayLap.Value);
            lblNoThue.Text = "Nợ tiền thuê đến hạn: " + no.NoTienThue.ToString("N0");
            lblNoDV.Text = "Nợ dịch vụ: " + no.NoDichVu.ToString("N0");
            lblKetLuan.Text = no.ConNo ? "KHÔNG đủ điều kiện gia hạn" : "Đủ điều kiện gia hạn";
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            var kq = s.LapPhieuGiaHan(txtSoPhieu.Text.Trim(), UiHelper.GiaTri(cboHopDong), UiHelper.Cell(dgvPhong, "MaPhong"),
                                      (int)numSoThangGH.Value, dtNgayLap.Value, UiHelper.GiaTri(cboNV));
            if (UiHelper.ThongBao(kq))
            {
                hd.CapNhatTrangThai();
                dgvPhieu.DataSource = s.LayPhieuGiaHan();
                cboHopDong_SelectedIndexChanged(null, null);
            }
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
