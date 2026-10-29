using System;
using System.Windows.Forms;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Service;

namespace QuanLyNhanSu.UI
{
    /// <summary>Quản lý các danh mục: Phòng ban, Chức vụ, Ca làm việc.</summary>
    public partial class DanhMucForm : Form
    {
        private readonly PhongBanService _phongBan = new PhongBanService();
        private readonly ChucVuService _chucVu = new ChucVuService();
        private readonly CaLamViecService _ca = new CaLamViecService();

        public DanhMucForm()
        {
            InitializeComponent();
            if (GridHelper.IsDesignTime) return;
            LoadPhongBan();
            LoadChucVu();
            LoadCa();
        }

        /* --------------------------- Phòng ban --------------------------- */
        private void LoadPhongBan() => GridHelper.Bind(_gridPB, _phongBan.GetAll());

        private void ThemPhongBan()
        {
            try
            {
                _phongBan.Insert(new PhongBan
                {
                    MaPB = _txtMaPB.Text.Trim(),
                    TenPB = _txtTenPB.Text.Trim(),
                    MoTa = _txtMoTa.Text.Trim()
                });
                LoadPhongBan();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void SuaPhongBan()
        {
            try
            {
                _phongBan.Update(new PhongBan
                {
                    MaPB = _txtMaPB.Text.Trim(),
                    TenPB = _txtTenPB.Text.Trim(),
                    MoTa = _txtMoTa.Text.Trim()
                });
                LoadPhongBan();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaPhongBan()
        {
            try
            {
                _phongBan.Delete(_txtMaPB.Text.Trim());
                Clear(_txtMaPB, _txtTenPB, _txtMoTa);
                LoadPhongBan();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LamMoiPhongBan() => Clear(_txtMaPB, _txtTenPB, _txtMoTa);

        /* --------------------------- Chức vụ --------------------------- */
        private void LoadChucVu() => GridHelper.Bind(_gridCV, _chucVu.GetAll());

        private ChucVu ReadChucVu() => new ChucVu
        {
            MaCV = _txtMaCV.Text.Trim(),
            TenCV = _txtTenCV.Text.Trim(),
            PhuCapChucVu = ParseDecimal(_txtPCCV.Text)
        };

        private void ThemChucVu()
        {
            try { _chucVu.Insert(ReadChucVu()); LoadChucVu(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void SuaChucVu()
        {
            try { _chucVu.Update(ReadChucVu()); LoadChucVu(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaChucVu()
        {
            try { _chucVu.Delete(_txtMaCV.Text.Trim()); Clear(_txtMaCV, _txtTenCV, _txtPCCV); LoadChucVu(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LamMoiChucVu() => Clear(_txtMaCV, _txtTenCV, _txtPCCV);

        /* --------------------------- Ca làm việc --------------------------- */
        private void LoadCa() => GridHelper.Bind(_gridCA, _ca.GetAll());

        private CaLamViec ReadCa() => new CaLamViec
        {
            MaCa = _txtMaCa.Text.Trim(),
            TenCa = _txtTenCa.Text.Trim(),
            GioBatDau = ParseTime(_txtBD.Text),
            GioKetThuc = ParseTime(_txtKT.Text)
        };

        private void ThemCa()
        {
            try { _ca.Insert(ReadCa()); LoadCa(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void SuaCa()
        {
            try { _ca.Update(ReadCa()); LoadCa(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XoaCa()
        {
            try { _ca.Delete(_txtMaCa.Text.Trim()); Clear(_txtMaCa, _txtTenCa, _txtBD, _txtKT); LoadCa(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LamMoiCa() => Clear(_txtMaCa, _txtTenCa, _txtBD, _txtKT);

        /* --------------------------- Tiện ích --------------------------- */
        private static void Clear(params TextBox[] boxes)
        {
            foreach (TextBox t in boxes) t.Clear();
        }

        private static decimal ParseDecimal(string s)
        {
            return decimal.TryParse(s.Trim(), out decimal v) ? v : 0m;
        }

        private static TimeSpan ParseTime(string s)
        {
            return TimeSpan.TryParse(s.Trim(), out TimeSpan t) ? t : TimeSpan.Zero;
        }
    }
}