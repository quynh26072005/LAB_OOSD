using System;
using System.Windows.Forms;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Service;

namespace QuanLyNhanSu.UI
{
    public partial class NhanVienForm : Form
    {
        private readonly NhanVienService _service = new NhanVienService();
        private readonly PhongBanService _pbSvc = new PhongBanService();
        private readonly ChucVuService _cvSvc = new ChucVuService();

        public NhanVienForm()
        {
            InitializeComponent();
            if (GridHelper.IsDesignTime) return;
            LoadPhongBan();
            LoadChucVu();
            LoadData();
        }

        private void LoadPhongBan()
        {
            _cboPB.DataSource = _pbSvc.GetAll();
            _cboPB.DisplayMember = "TenPB";
            _cboPB.ValueMember = "MaPB";
        }

        private void LoadChucVu()
        {
            _cboCV.DataSource = _cvSvc.GetAll();
            _cboCV.DisplayMember = "TenCV";
            _cboCV.ValueMember = "MaCV";
        }

        private void LoadData() => GridHelper.Bind(_grid, _service.TimKiem(_txtTim?.Text));

        private NhanVien ReadForm() => new NhanVien
        {
            MaNV = _txtMaNV.Text.Trim(),
            HoTen = _txtHoTen.Text.Trim(),
            NgaySinh = _dtNgaySinh.Checked ? _dtNgaySinh.Value : (DateTime?)null,
            GioiTinh = _cboGioiTinh.SelectedItem?.ToString(),
            DiaChi = _txtDiaChi.Text.Trim(),
            SDT = _txtSDT.Text.Trim(),
            CCCD = _txtCCCD.Text.Trim(),
            MaPB = _cboPB.SelectedValue?.ToString(),
            MaCV = _cboCV.SelectedValue?.ToString()
        };

        private void Them()
        {
            try { _service.Insert(ReadForm()); LoadData(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Sua()
        {
            try { _service.Update(ReadForm()); LoadData(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Xoa()
        {
            try { _service.Delete(_txtMaNV.Text.Trim()); LamMoi(); LoadData(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LamMoi()
        {
            _txtMaNV.Clear(); _txtHoTen.Clear(); _txtDiaChi.Clear();
            _txtSDT.Clear(); _txtCCCD.Clear();
            _dtNgaySinh.Value = DateTime.Today;
        }
    }
}