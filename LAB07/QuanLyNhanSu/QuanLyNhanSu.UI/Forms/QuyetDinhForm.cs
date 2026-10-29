using System;
using System.Windows.Forms;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Service;

namespace QuanLyNhanSu.UI
{
    /// <summary>
    /// Quản lý quyết định: tuyển dụng (thử việc), chức vụ, lương.
    /// Khen thưởng / kỷ luật được thao tác ở form riêng nhưng vẫn hiển thị
    /// trong danh sách tổng hợp.
    /// </summary>
    public partial class QuyetDinhForm : Form
    {
        private readonly QuyetDinhService _service = new QuyetDinhService();
        private readonly NhanVienService _nvSvc = new NhanVienService();
        private readonly PhongBanService _pbSvc = new PhongBanService();
        private readonly ChucVuService _cvSvc = new ChucVuService();

        public QuyetDinhForm()
        {
            InitializeComponent();
            if (GridHelper.IsDesignTime) return;
            LoadNhanVien(_tdNV); LoadNhanVien(_cvNV); LoadNhanVien(_lNV);
            LoadPhongBan(_tdPB);
            LoadChucVu(_cvCV);
            LoadData();
            SwitchType();
        }

        private void SwitchType()
        {
            ComboItem sel = _cboLoai.SelectedItem as ComboItem;
            string loai = sel?.Value ?? LoaiQuyetDinh.TuyenDung;
            _pnlTD.Visible = loai == LoaiQuyetDinh.TuyenDung;
            _pnlCV.Visible = loai == LoaiQuyetDinh.ChucVu;
            _pnlL.Visible = loai == LoaiQuyetDinh.Luong;
        }

        private void LoadData() => GridHelper.Bind(_grid, _service.GetDanhSach());

        private void LoadNhanVien(ComboBox cbo)
        {
            cbo.DataSource = _nvSvc.GetAll();
            cbo.DisplayMember = "HoTen";
            cbo.ValueMember = "MaNV";
        }

        private void LoadPhongBan(ComboBox cbo)
        {
            cbo.DataSource = _pbSvc.GetAll();
            cbo.DisplayMember = "TenPB";
            cbo.ValueMember = "MaPB";
        }

        private void LoadChucVu(ComboBox cbo)
        {
            cbo.DataSource = _cvSvc.GetAll();
            cbo.DisplayMember = "TenCV";
            cbo.ValueMember = "MaCV";
        }

        private void Them()
        {
            try
            {
                QuyetDinh qd = new QuyetDinh
                {
                    SoQD = _txtSoQD.Text.Trim(),
                    LoaiQD = ((ComboItem)_cboLoai.SelectedItem).Value,
                    NgayLap = _dtLap.Value,
                    NgayHieuLuc = _dtHieuLuc.Value,
                    NoiDung = _txtNoiDung.Text.Trim(),
                    NguoiKy = _txtNguoiKy.Text.Trim()
                };

                SqlCommandSpec detail = null;
                switch (qd.LoaiQD)
                {
                    case LoaiQuyetDinh.TuyenDung:
                        detail = _service.BuildInsertTuyenDung(new QdTuyenDung
                        {
                            SoQD = qd.SoQD,
                            ThoiGianThuViec = Int(_tdTg.Text.Trim()),
                            LuongThuViec = Dec(_tdLuong),
                            DieuKhoan = _tdDK.Text.Trim(),
                            MaNV = Val(_tdNV),
                            MaPB = Val(_tdPB)
                        });
                        break;
                    case LoaiQuyetDinh.ChucVu:
                        detail = _service.BuildInsertChucVu(new QdChucVu
                        {
                            SoQD = qd.SoQD,
                            MaNV = Val(_cvNV),
                            MaCV = Val(_cvCV),
                            PhuCapChucVu = Dec(_cvPc)
                        });
                        break;
                    case LoaiQuyetDinh.Luong:
                        detail = _service.BuildInsertLuong(new QdLuong
                        {
                            SoQD = qd.SoQD,
                            MaNV = Val(_lNV),
                            LuongCoBan = Dec(_lLcb),
                            PhuCapLuong = Dec(_lPcl),
                            PhuCapDocHai = Dec(_lPcdh)
                        });
                        break;
                }

                _service.TaoQD(qd, detail);
                LoadData();
                MessageBox.Show("Đã lập quyết định " + qd.SoQD);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Xoa()
        {
            if (_grid.CurrentRow?.DataBoundItem is QuyetDinhView v)
            {
                if (MessageBox.Show("Xóa quyết định " + v.SoQD + "?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try { _service.XoaQD(v.SoQD); LoadData(); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void LamMoi()
        {
            _txtSoQD.Clear(); _txtNoiDung.Clear(); _txtNguoiKy.Clear();
            _tdTg?.Clear(); _tdLuong?.Clear(); _tdDK?.Clear();
            _cvPc?.Clear(); _lLcb?.Clear(); _lPcl?.Clear(); _lPcdh?.Clear();
        }

        private static string Val(ComboBox c) => c.SelectedValue?.ToString();
        private static decimal Dec(TextBox t) => decimal.TryParse(t.Text.Trim(), out decimal v) ? v : 0m;
        private static int Int(string s) => int.TryParse(s, out int v) ? v : 0;

        private class ComboItem
        {
            public string Value { get; }
            public string Text { get; }
            public ComboItem(string value, string text) { Value = value; Text = text; }
            public override string ToString() => Text;
        }
    }
}