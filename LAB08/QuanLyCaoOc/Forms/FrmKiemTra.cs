using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmKiemTra : Form
    {
        private readonly KiemTraService s = new KiemTraService();
        private readonly DanhMucService dm = new DanhMucService();
        private readonly BindingList<KiemTraPhongItem> ketQua = new BindingList<KiemTraPhongItem>();

        public FrmKiemTra() { InitializeComponent(); }

        private void FrmKiemTra_Load(object sender, EventArgs e)
        {
            UiHelper.NapCombo(cboNV, dm.LayNhanVien(), "HoTen", "MaNV");
            UiHelper.NapCombo(cboQuyDinh, dm.LayQuyDinhPhat(), "NoiDung", "MaQuyDinh");
            dgvKetQua.DataSource = ketQua;
            dtNgayKT.Value = DateTime.Today;
            Tai();
        }

        private void Tai()
        {
            dgvDenHan.DataSource = s.LayPhongDenHan();
            dgvDot.DataSource = s.LayDot();
            dgvBienBan.DataSource = s.LayBienBan();
        }

        private void btnThemVaoDot_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgvDenHan.SelectedRows)
            {
                string p = Convert.ToString(r.Cells["MaPhong"].Value);
                bool co = false;
                foreach (var x in ketQua) if (x.MaPhong == p) co = true;
                if (!co) ketQua.Add(new KiemTraPhongItem { MaPhong = p, TinhTrang = "Tốt" });
            }
        }

        private void btnLuuDot_Click(object sender, EventArgs e)
        {
            var kq = s.LapDotKiemTra(txtMaDot.Text.Trim(), dtNgayKT.Value, txtGhiChu.Text.Trim(),
                                     UiHelper.GiaTri(cboNV), new List<KiemTraPhongItem>(ketQua));
            if (UiHelper.ThongBao(kq)) { ketQua.Clear(); Tai(); }
        }

        private void dgvDot_SelectionChanged(object sender, EventArgs e)
        {
            string ma = UiHelper.Cell(dgvDot, "MaDot");
            if (ma != "") dgvCTDot.DataSource = s.LayChiTiet(ma);
        }

        private void btnLapBienBan_Click(object sender, EventArgs e)
        {
            var kq = s.LapBienBanPhat(txtSoBB.Text.Trim(), UiHelper.Cell(dgvDot, "MaDot"),
                                      UiHelper.Cell(dgvCTDot, "MaPhong"), UiHelper.GiaTri(cboQuyDinh), UiHelper.GiaTri(cboNV));
            if (UiHelper.ThongBao(kq)) Tai();
        }

        private void btnNopPhat_Click(object sender, EventArgs e)
        {
            if (UiHelper.ThongBao(s.NopPhat(UiHelper.Cell(dgvBienBan, "SoBienBan")))) Tai();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
