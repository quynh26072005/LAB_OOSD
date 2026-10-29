using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Service;

namespace QuanLyNhanSu.UI
{
    /// <summary>Lập quyết định khen thưởng / kỷ luật (cá nhân hoặc tập thể) kèm phiếu chi / phiếu thu.</summary>
    public partial class KhenThuongKyLuatForm : Form
    {
        private readonly KhenThuongKyLuatService _service = new KhenThuongKyLuatService();
        private readonly QuyetDinhService _qdSvc = new QuyetDinhService();
        private readonly NhanVienService _nvSvc = new NhanVienService();

        public KhenThuongKyLuatForm()
        {
            InitializeComponent();
            if (GridHelper.IsDesignTime) return;
            LoadDoiTuong(_cboDoiTuongKT);
            LoadDoiTuong(_cboDoiTuongKL);
            LoadKhenThuong();
            LoadKyLuat();
        }

        /* ==================== Khen thưởng ==================== */
        private void LoadKhenThuong()
        {
            GridHelper.Bind(_gridKT, _qdSvc.GetDanhSach().Where(v => v.LoaiQD == LoaiQuyetDinh.KhenThuong).ToList());
            GridHelper.Bind(_gridPC, _service.GetPhieuChi());
        }

        private void LapKhenThuong()
        {
            try
            {
                QuyetDinh qd = new QuyetDinh
                {
                    SoQD = _txtSoQDKT.Text.Trim(),
                    LoaiQD = LoaiQuyetDinh.KhenThuong,
                    NgayLap = _dtLapKT.Value,
                    NgayHieuLuc = _dtLapKT.Value,
                    NoiDung = _txtNoiDungKT.Text.Trim(),
                    NguoiKy = _txtNguoiKyKT.Text.Trim()
                };
                QdKhenThuong detail = new QdKhenThuong
                {
                    SoQD = qd.SoQD,
                    LoaiKhenThuong = _txtLoaiKT.Text.Trim(),
                    SoTien = Dec(_txtTienKT),
                    DoiTuong = ExtractMa(_cboDoiTuongKT.Text)
                };
                PhieuChiKhenThuong phieu = new PhieuChiKhenThuong
                {
                    SoPhieuChi = _txtPhieuChi.Text.Trim(),
                    NgayChi = DateTime.TryParse(_txtNgayChi.Text.Trim(), out DateTime d) ? d : DateTime.Today,
                    SoTien = detail.SoTien,
                    SoQD = qd.SoQD
                };
                _service.LapQuyetDinhKhenThuong(qd, detail, phieu);
                LoadKhenThuong();
                MessageBox.Show("Đã lập quyết định khen thưởng " + qd.SoQD);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /* ==================== Kỷ luật ==================== */
        private void LoadKyLuat()
        {
            GridHelper.Bind(_gridKL, _qdSvc.GetDanhSach().Where(v => v.LoaiQD == LoaiQuyetDinh.KyLuat).ToList());
            GridHelper.Bind(_gridPT, _service.GetPhieuThu());
        }

        private void LapKyLuat()
        {
            try
            {
                QuyetDinh qd = new QuyetDinh
                {
                    SoQD = _txtSoQDKL.Text.Trim(),
                    LoaiQD = LoaiQuyetDinh.KyLuat,
                    NgayLap = _dtLapKL.Value,
                    NgayHieuLuc = _dtLapKL.Value,
                    NoiDung = _txtNoiDungKL.Text.Trim(),
                    NguoiKy = _txtNguoiKyKL.Text.Trim()
                };
                QdKyLuat detail = new QdKyLuat
                {
                    SoQD = qd.SoQD,
                    HinhThucKyLuat = _txtHinhThucKL.Text.Trim(),
                    SoTienPhat = Dec(_txtTienKL),
                    DoiTuong = ExtractMa(_cboDoiTuongKL.Text)
                };
                PhieuThuNopPhat phieu = new PhieuThuNopPhat
                {
                    SoPhieuThu = _txtPhieuThu.Text.Trim(),
                    NgayThu = DateTime.Today,
                    SoTien = detail.SoTienPhat,
                    HinhThuc = _cboHinhThucThu.SelectedItem?.ToString(),
                    SoQD = qd.SoQD
                };
                _service.LapQuyetDinhKyLuat(qd, detail, phieu);
                LoadKyLuat();
                MessageBox.Show("Đã lập quyết định kỷ luật " + qd.SoQD);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /* ==================== Tiện ích ==================== */
        private void LoadDoiTuong(ComboBox cbo)
        {
            foreach (NhanVien nv in _nvSvc.GetAll())
            {
                cbo.Items.Add($"{nv.MaNV} - {nv.HoTen}");
            }
            cbo.Items.Add("PB01 - Phòng Tài chính - Kế toán");
            cbo.Items.Add("PB02 - Phòng Kỹ thuật");
            cbo.Items.Add("PB03 - Phòng Sản xuất");
            cbo.Items.Add("PB04 - Phòng Nhân sự");
        }

        private static decimal Dec(TextBox t) => decimal.TryParse(t.Text.Trim(), out decimal v) ? v : 0m;

        /// <summary>Tách mã đối tượng (trước dấu " - "), ví dụ "NV003 - Nguyễn Văn An" -> "NV003".</summary>
        private static string ExtractMa(string s)
        {
            s = (s ?? "").Trim();
            int idx = s.IndexOf(" - ", StringComparison.Ordinal);
            return idx > 0 ? s.Substring(0, idx).Trim() : s;
        }
    }
}