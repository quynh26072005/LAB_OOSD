using System;
using System.Windows.Forms;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Service;

namespace QuanLyNhanSu.UI
{
    /// <summary>Quản lý hợp đồng lao động và bảo hiểm (BHYT, BHXH).</summary>
    public partial class HopDongForm : Form
    {
        private readonly HopDongService _hdSvc = new HopDongService();
        private readonly BaoHiemService _bhSvc = new BaoHiemService();
        private readonly NhanVienService _nvSvc = new NhanVienService();

        public HopDongForm()
        {
            InitializeComponent();
            if (GridHelper.IsDesignTime) return;
            LoadNV();
            LoadHD();
            LoadBH();
        }

        private void KyHopDong()
        {
            try
            {
                HopDong hd = new HopDong
                {
                    SoHD = _txtSoHD.Text.Trim(),
                    LoaiHD = _cboLoaiHD.SelectedItem?.ToString(),
                    NgayKy = _dtKy.Value,
                    NgayBatDau = _dtBatDau.Value,
                    NgayKetThuc = _chkKetThuc.Checked ? _dtKetThuc.Value : (DateTime?)null,
                    MaNV = _cboNV.SelectedValue?.ToString()
                };
                _hdSvc.KyHopDongChinhThuc(hd,
                    ParseDec(_txtMucBHYT.Text, 45000m),
                    ParseDec(_txtMucBHXH.Text, 1050000m));
                LoadHD();
                LoadBH();
                MessageBox.Show("Đã ký hợp đồng " + hd.SoHD + (hd.LoaiHD == LoaiHopDong.DaiHan ? " và lập BHYT/BHXH." : "."));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadNV()
        {
            _cboNV.DataSource = _nvSvc.GetAll();
            _cboNV.DisplayMember = "HoTen";
            _cboNV.ValueMember = "MaNV";
        }

        private void LoadHD() => GridHelper.Bind(_gridHD, _hdSvc.GetDanhSach());
        private void LoadBH() => GridHelper.Bind(_gridBH, _bhSvc.GetDanhSach());

        private static decimal ParseDec(string s, decimal def)
        {
            return decimal.TryParse(s.Trim(), out decimal v) ? v : def;
        }
    }
}