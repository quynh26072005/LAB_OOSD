using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmHopDong : Form
    {
        private readonly HopDongService s = new HopDongService();
        private readonly TangPhongService tp = new TangPhongService();
        private readonly DanhMucService dm = new DanhMucService();
        private readonly BindingList<PhongThueItem> chon = new BindingList<PhongThueItem>();

        public FrmHopDong() { InitializeComponent(); }

        private void FrmHopDong_Load(object sender, EventArgs e)
        {
            UiHelper.NapCombo(cboNV, dm.LayNhanVien(), "HoTen", "MaNV");
            dgvChon.DataSource = chon;
            dtLap.Value = DateTime.Today;
            dtBatDau.Value = DateTime.Today;
            numSoThang.Value = 6;
            Tai();
        }

        private void Tai()
        {
            UiHelper.NapCombo(cboKhach, s.LayKhach(), "TenKhach", "MaKhach");
            dgvHopDong.DataSource = s.LayHopDong();
        }

        private void H(KetQuaXuLy kq) { if (UiHelper.ThongBao(kq)) Tai(); }

        private void btnThemKhach_Click(object sender, EventArgs e)
        {
            H(s.ThemKhach(txtKHMa.Text.Trim(), txtKHTen.Text.Trim(), txtKHGiayTo.Text.Trim(),
                          txtKHDaiDien.Text.Trim(), txtKHSDT.Text.Trim(), txtKHDiaChi.Text.Trim()));
        }

        // Tim phong trong theo thoi gian thue du kien
        private void btnTimPhong_Click(object sender, EventArgs e)
        {
            DateTime kt = HopDongService.TinhNgayKetThuc(dtBatDau.Value, (int)numSoThang.Value);
            dgvPhongTrong.DataSource = tp.LayPhongTrong(dtBatDau.Value, kt);
            lblKetThuc.Text = "Kết thúc dự kiến: " + kt.ToString("dd/MM/yyyy");
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            string p = UiHelper.Cell(dgvPhongTrong, "MaPhong");
            if (p == "") return;
            foreach (var x in chon)
                if (x.MaPhong == p) { MessageBox.Show("Phòng đã có trong hợp đồng."); return; }
            chon.Add(new PhongThueItem
            {
                MaPhong = p,
                DonGiaThang = Convert.ToDecimal(dgvPhongTrong.CurrentRow.Cells["DonGiaThang"].Value)
            });
        }

        private void btnBoPhong_Click(object sender, EventArgs e)
        {
            if (dgvChon.CurrentRow != null && dgvChon.CurrentRow.Index >= 0 && dgvChon.CurrentRow.Index < chon.Count)
                chon.RemoveAt(dgvChon.CurrentRow.Index);
        }

        private void btnLapHD_Click(object sender, EventArgs e)
        {
            var kq = s.TaoHopDong(txtSoHD.Text.Trim(), UiHelper.GiaTri(cboKhach), UiHelper.GiaTri(cboNV),
                                  dtLap.Value, dtBatDau.Value, (int)numSoThang.Value, txtNoiDung.Text.Trim(),
                                  new List<PhongThueItem>(chon));
            if (UiHelper.ThongBao(kq)) { chon.Clear(); Tai(); }
        }

        private void dgvHopDong_SelectionChanged(object sender, EventArgs e)
        {
            string so = UiHelper.Cell(dgvHopDong, "SoHopDong");
            if (so == "") return;
            txtHDChon.Text = so;
            dgvChiTiet.DataSource = s.LayChiTiet(so);
            dgvLich.DataSource = s.LayLichThanhToan(so);
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
