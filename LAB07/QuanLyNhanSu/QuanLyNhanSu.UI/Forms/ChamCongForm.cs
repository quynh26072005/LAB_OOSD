using System;
using System.Windows.Forms;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Service;

namespace QuanLyNhanSu.UI
{
    /// <summary>Phân công ca, chấm công và ghi nhận giờ phụ trội.</summary>
    public partial class ChamCongForm : Form
    {
        private readonly KeHoachPhanCongService _khSvc = new KeHoachPhanCongService();
        private readonly ChiTietPhanCongService _ctPcSvc = new ChiTietPhanCongService();
        private readonly BangChamCongService _ccSvc = new BangChamCongService();
        private readonly ChiTietChamCongService _ctCcSvc = new ChiTietChamCongService();
        private readonly PhieuPhuTroiService _ptSvc = new PhieuPhuTroiService();
        private readonly ChiTietPhuTroiService _ctPtSvc = new ChiTietPhuTroiService();
        private readonly NhanVienService _nvSvc = new NhanVienService();
        private readonly PhongBanService _pbSvc = new PhongBanService();
        private readonly CaLamViecService _caSvc = new CaLamViecService();

        public ChamCongForm()
        {
            InitializeComponent();
            if (GridHelper.IsDesignTime) return;

            LoadNV(_cboNV);
            LoadCa(_cboCa);
            LoadPB(_cboKH_PB);
            LoadPB(_cboCC_PB);
            LoadNV(_cboNV_CC);
            LoadCa(_cboCa_CC);
            LoadNV(_cboNV_PT);
            LoadKH();
            LoadCC();
            LoadPT();
        }

        /* --------------------------- Phân công --------------------------- */
        private void LoadKH() => GridHelper.Bind(_gridKH, _khSvc.GetDanhSach());
        private void LoadCTPC()
        {
            string maKH = GridHelper.SelectedKey(_gridKH, "MaKH")?.ToString();
            if (maKH == null) return;
            _txtMaKH.Text = maKH;
            GridHelper.Bind(_gridCTPC, _ctPcSvc.GetByKeHoach(maKH));
        }

        private void ThemKH()
        {
            try
            {
                _khSvc.Insert(new KeHoachPhanCong
                {
                    MaKH = _txtMaKH.Text.Trim(),
                    Tuan = _txtTuan.Text.Trim(),
                    MaPB = _cboKH_PB.SelectedValue?.ToString(),
                    NguoiLap = _txtNguoiLap.Text.Trim()
                });
                LoadKH();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaKH()
        {
            string maKH = _txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(maKH)) return;
            try { _khSvc.Delete(maKH); LoadKH(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ThemCTPC()
        {
            try
            {
                _ctPcSvc.Insert(new ChiTietPhanCong
                {
                    MaKH = _txtMaKH.Text.Trim(),
                    MaNV = _cboNV.SelectedValue?.ToString(),
                    Ngay = _dtNgayPC.Value,
                    MaCa = _cboCa.SelectedValue?.ToString(),
                    CoPhuTroi = _chkPhuTroi.Checked
                });
                LoadCTPC();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaCTPC()
        {
            if (_gridCTPC.CurrentRow?.DataBoundItem is ChiTietPhanCong d)
            {
                try
                {
                    _ctPcSvc.Delete(d.MaKH, d.MaNV, d.Ngay);
                    LoadCTPC();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        /* --------------------------- Chấm công --------------------------- */
        private void LoadCC() => GridHelper.Bind(_gridCC, _ccSvc.GetDanhSach());
        private void LoadCTCC()
        {
            string maCC = GridHelper.SelectedKey(_gridCC, "MaCC")?.ToString();
            if (maCC == null) return;
            _txtMaCC.Text = maCC;
            GridHelper.Bind(_gridCTCC, _ctCcSvc.GetByBang(maCC));
        }

        private void ThemCC()
        {
            try
            {
                _ccSvc.Insert(new BangChamCong
                {
                    MaCC = _txtMaCC.Text.Trim(),
                    Thang = (int)_numThang.Value,
                    Nam = (int)_numNam.Value,
                    MaPB = _cboCC_PB.SelectedValue?.ToString()
                });
                LoadCC();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaCC()
        {
            string maCC = _txtMaCC.Text.Trim();
            if (string.IsNullOrEmpty(maCC)) return;
            try { _ccSvc.Delete(maCC); LoadCC(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ThemCTCC()
        {
            try
            {
                _ctCcSvc.Insert(new ChiTietChamCong
                {
                    MaCC = _txtMaCC.Text.Trim(),
                    MaNV = _cboNV_CC.SelectedValue?.ToString(),
                    Ngay = _dtNgayCC.Value,
                    MaCa = _cboCa_CC.SelectedValue?.ToString(),
                    TrangThai = _cboTrangThai.SelectedItem?.ToString()
                });
                LoadCTCC();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaCTCC()
        {
            if (_gridCTCC.CurrentRow?.DataBoundItem is ChiTietChamCong d)
            {
                try { _ctCcSvc.Delete(d.MaCC, d.MaNV, d.Ngay); LoadCTCC(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        /* --------------------------- Phụ trội --------------------------- */
        private void LoadPT() => GridHelper.Bind(_gridPT, _ptSvc.GetAll());
        private void LoadCTPT()
        {
            string soPhieu = GridHelper.SelectedKey(_gridPT, "SoPhieu")?.ToString();
            if (soPhieu == null) return;
            _txtSoPhieu.Text = soPhieu;
            GridHelper.Bind(_gridCTPT, _ctPtSvc.GetByPhieu(soPhieu));
        }

        private void ThemPT()
        {
            try
            {
                _ptSvc.Insert(new PhieuPhuTroi
                {
                    SoPhieu = _txtSoPhieu.Text.Trim(),
                    NgayPhuTroi = _dtNgayPT.Value,
                    SoGioPhuTroi = 0,
                    HinhThuc = _cboHinhThuc.SelectedItem?.ToString(),
                    HeSo = double.TryParse(_txtHeSo.Text.Trim(), out double hs) ? hs : 1.5
                });
                LoadPT();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaPT()
        {
            string soPhieu = _txtSoPhieu.Text.Trim();
            if (string.IsNullOrEmpty(soPhieu)) return;
            try { _ptSvc.Delete(soPhieu); LoadPT(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ThemCTPT()
        {
            try
            {
                _ctPtSvc.Insert(new ChiTietPhuTroi
                {
                    SoPhieu = _txtSoPhieu.Text.Trim(),
                    MaNV = _cboNV_PT.SelectedValue?.ToString(),
                    SoGio = double.TryParse(_txtSoGioPT.Text.Trim(), out double g) ? g : 0
                });
                LoadCTPT();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaCTPT()
        {
            if (_gridCTPT.CurrentRow?.DataBoundItem is ChiTietPhuTroi d)
            {
                try { _ctPtSvc.Delete(d.SoPhieu, d.MaNV); LoadCTPT(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        /* --------------------------- Tiện ích --------------------------- */
        private void LoadNV(ComboBox cbo)
        {
            cbo.DataSource = _nvSvc.GetAll();
            cbo.DisplayMember = "HoTen";
            cbo.ValueMember = "MaNV";
        }

        private void LoadPB(ComboBox cbo)
        {
            cbo.DataSource = _pbSvc.GetAll();
            cbo.DisplayMember = "TenPB";
            cbo.ValueMember = "MaPB";
        }

        private void LoadCa(ComboBox cbo)
        {
            cbo.DataSource = _caSvc.GetAll();
            cbo.DisplayMember = "TenCa";
            cbo.ValueMember = "MaCa";
        }
    }
}