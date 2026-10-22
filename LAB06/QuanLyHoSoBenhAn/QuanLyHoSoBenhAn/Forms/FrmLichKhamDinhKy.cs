using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmLichKhamDinhKy: kiểm tra quyền quý, đặt/đổi/hủy lịch (mục 3.7, BR09/BR10, TC06-TC08).</summary>
    public partial class FrmLichKhamDinhKy : Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly LichKhamService _lichService = new LichKhamService();
        private readonly BenhNhanService _bnService = new BenhNhanService();

        private int? _maBenhNhanDangChon;
        private int? _maLichDangChon;

        public FrmLichKhamDinhKy(NhanVien nguoiDung)
        {
            _nguoiDung = nguoiDung;
            Text = "Lịch khám định kỳ";
            InitializeComponent();
            TaiDanhSachLich();
        }

        private void TaiDanhSachLich()
        {
            gridLich.DataSource = _lichService.TimKiem(txtTimLich.Text.Trim(), (int)numNamLoc.Value);
            if (gridLich.Columns["MaBenhNhan"] != null) gridLich.Columns["MaBenhNhan"].Visible = false;
        }

        private void GridLich_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            _maLichDangChon = Convert.ToInt32(gridLich.Rows[e.RowIndex].Cells["MaLichHen"].Value);
        }

        private void TaiDanhSachBenhNhan()
        {
            gridBenhNhan.DataSource = _bnService.TimKiem(txtTimBenhNhan.Text.Trim());
            foreach (string c in new[] { "DiaChi", "Email", "Anh", "MaHoSo", "TrangThaiHoSo" })
                if (gridBenhNhan.Columns[c] != null) gridBenhNhan.Columns[c].Visible = false;
        }

        private void GridBenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            _maBenhNhanDangChon = Convert.ToInt32(gridBenhNhan.Rows[e.RowIndex].Cells["MaBenhNhan"].Value);
            var bn = _bnService.LayTheoMa(_maBenhNhanDangChon.Value);
            lblBenhNhanDangChon.Text = $"Bệnh nhân: {bn.HoTen} (mã {bn.MaBenhNhan})";
        }

        private void BtnDat_Click(object sender, EventArgs e)
        {
            if (_maBenhNhanDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn bệnh nhân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nam = (int)numNam.Value;
            int quy = (int)cboQuy.SelectedItem;

            // TC06: đã dùng quyền lợi quý đó -> từ chối trước khi gọi DB (kiểm tra sớm, DB vẫn là chốt chặn cuối - TC08)
            if (!_lichService.ConSuatQuy(_maBenhNhanDangChon.Value, nam, quy))
            {
                MessageBox.Show($"Bệnh nhân đã dùng suất khám định kỳ quý {quy}/{nam}.", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maLich = _lichService.DatLich(_maBenhNhanDangChon.Value, nam, quy, dtpThoiGian.Value, _nguoiDung.MaNhanVien);
                MessageBox.Show("Đặt lịch thành công. Mã lịch hẹn: " + maLich, "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachLich();
            }
            catch (InvalidOperationException ex)
            {
                // TC08: hai nhân viên đặt cùng lúc -> UNIQUE filtered index chặn, chỉ 1 giao dịch thành công.
                MessageBox.Show(ex.Message, "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đặt lịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDoiLich_Click(object sender, EventArgs e)
        {
            if (_maLichDangChon == null) { Thong("Vui lòng chọn một lịch hẹn ở bảng trên."); return; }
            try
            {
                _lichService.DoiLich(_maLichDangChon.Value, dtpThoiGian.Value, _nguoiDung.MaNhanVien);
                MessageBox.Show("Đổi lịch thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachLich();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnHuyLich_Click(object sender, EventArgs e)
        {
            if (_maLichDangChon == null) { Thong("Vui lòng chọn một lịch hẹn ở bảng trên."); return; }
            try
            {
                _lichService.HuyLich(_maLichDangChon.Value, _nguoiDung.MaNhanVien);
                MessageBox.Show("Đã hủy lịch.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachLich();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnTiepNhan_Click(object sender, EventArgs e)
        {
            if (_maLichDangChon == null) { Thong("Vui lòng chọn một lịch hẹn ở bảng trên."); return; }
            _lichService.TiepNhanLich(_maLichDangChon.Value, _nguoiDung.MaNhanVien);
            MessageBox.Show("Đã tiếp nhận lịch. Vào màn hình Tiếp nhận khám để tạo phiếu khám.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            TaiDanhSachLich();
        }

        private void Thong(string msg) => MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
