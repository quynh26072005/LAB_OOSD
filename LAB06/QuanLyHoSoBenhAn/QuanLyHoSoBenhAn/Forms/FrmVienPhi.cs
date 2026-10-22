using System;
using System.Data;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmVienPhi: hóa đơn, thanh toán, hoàn tất (mục 3.7, BR11/BR17, TC14/TC15).</summary>
    public partial class FrmVienPhi : Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly VienPhiService _service = new VienPhiService();

        private int? _maHoaDonDangChon;

        public FrmVienPhi(NhanVien nguoiDung)
        {
            _nguoiDung = nguoiDung;
            Text = "Viện phí";
            InitializeComponent();
            TaiDanhSach();
        }

        private void TaiDanhSach()
        {
            gridChoLapHoaDon.DataSource = _service.LayPhieuChoLapHoaDon();
            gridHoaDonChuaXong.DataSource = _service.LayHangDoiHoaDonChuaXong();
        }

        private void BtnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (gridChoLapHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu khám.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int maPhieu = Convert.ToInt32(gridChoLapHoaDon.CurrentRow.Cells["MaPhieuKham"].Value);
            try
            {
                int maHoaDon = _service.LapHoaDonTuDong(maPhieu, _nguoiDung.MaNhanVien);
                MessageBox.Show("Đã lập hóa đơn #" + maHoaDon, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lập hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridHoaDonChuaXong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            _maHoaDonDangChon = Convert.ToInt32(gridHoaDonChuaXong.Rows[e.RowIndex].Cells["MaHoaDon"].Value);
            NapHoaDon();
        }

        private void NapHoaDon()
        {
            if (_maHoaDonDangChon == null) return;
            DataRow r = _service.LayHoaDonChiTiet(_maHoaDonDangChon.Value);
            if (r == null) return;

            decimal tongTien = Convert.ToDecimal(r["TongTien"]);
            decimal daThanhToan = Convert.ToDecimal(r["DaThanhToan"]);
            decimal conLai = tongTien - daThanhToan;

            lblHoaDonDangChon.Text =
                $"Hóa đơn #{_maHoaDonDangChon} - BN: {r["TenBenhNhan"]}\n" +
                $"Tổng tiền: {tongTien:N0}đ - Đã thu: {daThanhToan:N0}đ - Còn lại: {conLai:N0}đ - Trạng thái: {r["TrangThai"]}";

            gridChiTietHoaDon.DataSource = _service.LayChiTietHoaDon(_maHoaDonDangChon.Value);
            numSoTienThanhToan.Maximum = Math.Max(conLai, 0) == 0 ? 1 : conLai;
            numSoTienThanhToan.Value = Math.Max(conLai, 0) == 0 ? 0 : conLai;
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            if (_maHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numSoTienThanhToan.Value <= 0)
            {
                MessageBox.Show("Số tiền thanh toán phải lớn hơn 0.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool duTien = _service.GhiThanhToanVaHoanTatNeuDu(
                    _maHoaDonDangChon.Value, numSoTienThanhToan.Value, cboPhuongThuc.SelectedItem.ToString(), null, _nguoiDung.MaNhanVien);

                // BR11/TC14/TC15: chỉ hoàn tất phiếu khám khi tổng đã thu >= tổng phải thu.
                MessageBox.Show(
                    duTien ? "Thanh toán đủ. Phiếu khám đã chuyển sang Hoàn tất." : "Đã ghi nhận thanh toán một phần. Phiếu khám CHƯA hoàn tất.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                TaiDanhSach();
                NapHoaDon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể ghi nhận thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
