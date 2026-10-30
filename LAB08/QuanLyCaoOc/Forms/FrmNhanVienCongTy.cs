using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyCaoOc.Data;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc.Forms
{
    public partial class FrmNhanVienCongTy : Form
    {
        private readonly NhanVienCongTyService s = new NhanVienCongTyService();
        private readonly HopDongService hd = new HopDongService();
        private readonly DanhMucService dm = new DanhMucService();

        public FrmNhanVienCongTy() { InitializeComponent(); }

        private void FrmNhanVienCongTy_Load(object sender, EventArgs e)
        {
            UiHelper.NapCombo(cboNV, dm.LayNhanVien(), "HoTen", "MaNV");
            UiHelper.NapCombo(cboCongTy, hd.LayKhach(), "TenKhach", "MaKhach");
            cboLoaiXe.Items.AddRange(new object[] { "Xe máy", "Xe đạp điện", "Ô tô" });
            cboLoaiXe.SelectedIndex = 0;
            dtVaoLam.Value = DateTime.Today;
            Tai();
            cboCongTy_SelectedIndexChanged(null, null);
        }

        private void Tai()
        {
            dgvNVCT.DataSource = s.LayNhanVienCongTy();
            dgvThe.DataSource = s.LayTheGiuXe();
        }

        // Chi cho chon cac phong cong ty dang thue
        private void cboCongTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string k = UiHelper.GiaTri(cboCongTy);
            if (k != "") UiHelper.NapCombo(cboPhong, s.LayPhongDangThue(k), "MaPhong", "MaPhong");
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (dlg.ShowDialog() != DialogResult.OK) return;
                txtHinhAnh.Text = dlg.FileName;
                HienAnh(dlg.FileName);
            }
        }

        private void HienAnh(string path)
        {
            if (picAnh.Image != null) { picAnh.Image.Dispose(); picAnh.Image = null; }
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                picAnh.Image = new Bitmap(fs);          // doc xong dong file, khong khoa file anh
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            var kq = s.KhaiBaoNhanVien(txtMaNVCT.Text.Trim(), txtHoTenNV.Text.Trim(), txtHinhAnh.Text.Trim(),
                                       txtChucVu.Text.Trim(), UiHelper.GiaTri(cboCongTy), UiHelper.GiaTri(cboPhong),
                                       dtVaoLam.Value, UiHelper.GiaTri(cboNV));
            if (UiHelper.ThongBao(kq)) Tai();
        }

        private void dgvNVCT_SelectionChanged(object sender, EventArgs e)
        {
            string ma = UiHelper.Cell(dgvNVCT, "MaNVCT");
            if (ma == "") return;
            txtNVChon.Text = ma;
            HienAnh(UiHelper.Cell(dgvNVCT, "HinhAnh"));
        }

        private void btnNghi_Click(object sender, EventArgs e)
        {
            if (UiHelper.ThongBao(s.DanhDauNghi(txtNVChon.Text))) Tai();
        }

        private void btnCapThe_Click(object sender, EventArgs e)
        {
            var kq = s.CapThe(txtMaThe.Text.Trim(), txtNVChon.Text, cboLoaiXe.Text, txtBienSo.Text.Trim(),
                              txtNhanHieu.Text.Trim(), txtMauSac.Text.Trim(), UiHelper.GiaTri(cboNV));
            if (UiHelper.ThongBao(kq)) Tai();
        }

        private void btnThuHoi_Click(object sender, EventArgs e)
        {
            if (UiHelper.ThongBao(s.ThuHoiThe(UiHelper.Cell(dgvThe, "MaThe")))) Tai();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
