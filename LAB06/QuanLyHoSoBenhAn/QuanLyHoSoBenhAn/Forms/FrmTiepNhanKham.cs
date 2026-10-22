using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmTiepNhanKham: tìm hồ sơ, tạo phiếu khám và xếp hàng (mục 3.7, mục UC01 bước 4-5).</summary>
    public partial class FrmTiepNhanKham : Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly BenhNhanService _bnService = new BenhNhanService();
        private readonly KhamBenhService _khamService = new KhamBenhService();
        private readonly DanhMucService _dmService = new DanhMucService();

        private int? _maHoSoDangChon;

        public FrmTiepNhanKham(NhanVien nguoiDung)
        {
            _nguoiDung = nguoiDung;
            Text = "Tiếp nhận khám";
            InitializeComponent();
            TaiDanhMuc();
            TaiHangDoi();
        }

        private void TaiDanhMuc()
        {
            cboKhoa.DataSource = _dmService.LayDanhSachKhoa();
            cboBacSi.DataSource = _dmService.LayDanhSachBacSi();
        }

        private void TaiDanhSachBenhNhan()
        {
            gridBenhNhan.DataSource = _bnService.TimKiem(txtTimBenhNhan.Text.Trim());
            foreach (string c in new[] { "DiaChi", "Email", "Anh" })
                if (gridBenhNhan.Columns[c] != null) gridBenhNhan.Columns[c].Visible = false;
        }

        private void GridBenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridBenhNhan.Rows[e.RowIndex];
            object maHoSoObj = row.Cells["MaHoSo"].Value;
            string hoTen = row.Cells["HoTen"].Value.ToString();

            if (maHoSoObj == null || maHoSoObj == DBNull.Value)
            {
                _maHoSoDangChon = null;
                lblBenhNhanDangChon.Text = $"Bệnh nhân: {hoTen} — CHƯA CÓ HỒ SƠ (yêu cầu thu lệ phí ở màn hình Lệ phí & Hồ sơ trước).";
                lblBenhNhanDangChon.ForeColor = Color.Red;
                return;
            }

            _maHoSoDangChon = Convert.ToInt32(maHoSoObj);
            lblBenhNhanDangChon.Text = $"Bệnh nhân: {hoTen} - Mã hồ sơ: {_maHoSoDangChon}";
            lblBenhNhanDangChon.ForeColor = Color.Black;
        }

        private void BtnTao_Click(object sender, EventArgs e)
        {
            if (_maHoSoDangChon == null)
            {
                MessageBox.Show("Bệnh nhân chưa có hồ sơ bệnh án hợp lệ để tiếp nhận.", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboKhoa.SelectedValue == null || cboBacSi.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khoa và bác sĩ.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maPhieu = _khamService.TiepNhan(
                    _maHoSoDangChon.Value,
                    Convert.ToInt32(cboKhoa.SelectedValue),
                    Convert.ToInt32(cboBacSi.SelectedValue),
                    null,
                    cboLoaiKham.SelectedItem.ToString(),
                    _nguoiDung.MaNhanVien);

                MessageBox.Show("Tạo phiếu khám thành công. Mã phiếu: " + maPhieu, "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiHangDoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tạo phiếu khám: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaiHangDoi()
        {
            gridHangDoi.DataSource = _khamService.LayHangDoiTiepNhan();
        }
    }
}
