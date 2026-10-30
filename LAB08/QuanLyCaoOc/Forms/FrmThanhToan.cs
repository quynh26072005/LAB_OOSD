using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmThanhToan : Form
    {
        private readonly ThanhToanService s = new ThanhToanService();
        private readonly HopDongService hd = new HopDongService();
        private readonly DanhMucService dm = new DanhMucService();

        public FrmThanhToan() { InitializeComponent(); }

        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            UiHelper.NapCombo(cboNV, dm.LayNhanVien(), "HoTen", "MaNV");
            cboHinhThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản" });
            cboHinhThuc.SelectedIndex = 0;
            numThang.Value = DateTime.Today.Month;
            numNam.Value = DateTime.Today.Year;

            DataTable dv = s.LayDichVu();
            dv.Columns.Add("SoLuong", typeof(decimal));          // cot nhap so luong dung trong thang
            dgvDichVu.DataSource = dv;
            foreach (string c in new[] { "MaDV", "TenDV", "DonViTinh", "DonGia" })
                dgvDichVu.Columns[c].ReadOnly = true;

            UiHelper.NapCombo(cboHopDong, hd.LayHopDong(), "SoHopDong", "SoHopDong");
            cboHopDong_SelectedIndexChanged(null, null);
        }

        private string SoHD { get { return UiHelper.GiaTri(cboHopDong); } }

        private void TaiKy() { dgvKy.DataSource = SoHD == "" ? null : s.LayKy(SoHD); }
        private void TaiHDDV() { dgvHDDV.DataSource = SoHD == "" ? null : s.LayHoaDonDV(SoHD); }

        private void cboHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SoHD == "") return;
            TaiKy();
            TaiHDDV();
            UiHelper.NapCombo(cboPhongDV, hd.LayChiTiet(SoHD), "MaPhong", "MaPhong");
        }

        // ----- Thu tien thue -----
        private void dgvKy_SelectionChanged(object sender, EventArgs e)
        {
            decimal no;
            if (decimal.TryParse(UiHelper.Cell(dgvKy, "ConNo"), out no) && no > 0 && no <= numSoTienKy.Maximum)
                numSoTienKy.Value = no;                          // goi y thu du phan con no
        }

        private void btnThuThue_Click(object sender, EventArgs e)
        {
            int maKy;
            if (!int.TryParse(UiHelper.Cell(dgvKy, "MaKy"), out maKy)) { MessageBox.Show("Chưa chọn kỳ thanh toán."); return; }
            var kq = s.ThuTienThue(txtSoPhieuKy.Text.Trim(), maKy, numSoTienKy.Value, cboHinhThuc.Text, UiHelper.GiaTri(cboNV));
            if (UiHelper.ThongBao(kq)) TaiKy();
        }

        // ----- Hoa don dich vu cuoi thang -----
        private void btnLapHDDV_Click(object sender, EventArgs e)
        {
            var ds = new List<DichVuDungItem>();
            foreach (DataGridViewRow r in dgvDichVu.Rows)
            {
                if (r.IsNewRow || r.Cells["SoLuong"].Value == null || r.Cells["SoLuong"].Value == DBNull.Value) continue;
                decimal sl = Convert.ToDecimal(r.Cells["SoLuong"].Value);
                if (sl <= 0) continue;
                ds.Add(new DichVuDungItem
                {
                    MaDV = Convert.ToString(r.Cells["MaDV"].Value),
                    SoLuong = sl,
                    DonGia = Convert.ToDecimal(r.Cells["DonGia"].Value)
                });
            }
            var kq = s.LapHoaDonDV(txtSoHDDV.Text.Trim(), SoHD, UiHelper.GiaTri(cboPhongDV),
                                   (int)numThang.Value, (int)numNam.Value, UiHelper.GiaTri(cboNV), ds);
            if (UiHelper.ThongBao(kq)) TaiHDDV();
        }

        private void dgvHDDV_SelectionChanged(object sender, EventArgs e)
        {
            string so = UiHelper.Cell(dgvHDDV, "SoHDDV");
            if (so == "") return;
            dgvCTDV.DataSource = s.LayChiTietHoaDonDV(so);
            decimal tong, da;
            if (decimal.TryParse(UiHelper.Cell(dgvHDDV, "TongTien"), out tong) &&
                decimal.TryParse(UiHelper.Cell(dgvHDDV, "DaThu"), out da) &&
                tong - da > 0 && tong - da <= numSoTienDV.Maximum)
                numSoTienDV.Value = tong - da;
        }

        private void btnThuDV_Click(object sender, EventArgs e)
        {
            string so = UiHelper.Cell(dgvHDDV, "SoHDDV");
            if (so == "") { MessageBox.Show("Chưa chọn hóa đơn dịch vụ."); return; }
            var kq = s.ThuTienDichVu(txtSoPhieuDV.Text.Trim(), so, numSoTienDV.Value, cboHinhThuc.Text, UiHelper.GiaTri(cboNV));
            if (UiHelper.ThongBao(kq)) TaiHDDV();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
