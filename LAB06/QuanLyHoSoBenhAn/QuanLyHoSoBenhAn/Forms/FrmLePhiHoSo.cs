using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmLePhiHoSo: thu lệ phí năm, cấp/tra hồ sơ (mục 3.7). Thực hiện BR02/BR12, TC01-TC04.</summary>
    public partial class FrmLePhiHoSo : System.Windows.Forms.Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly BenhNhanService _service = new BenhNhanService();

        private int? _maBenhNhanDangChon;

        public FrmLePhiHoSo(NhanVien nguoiDung)
        {
            _nguoiDung = nguoiDung;
            Text = "Lệ phí năm & Cấp hồ sơ bệnh án";
            InitializeComponent();
            TaiDanhSach();
        }

        private void TaiDanhSach()
        {
            grid.DataSource = _service.TimKiem(txtTimKiem.Text.Trim());
            if (grid.Columns["MaHoSo"] != null) grid.Columns["MaHoSo"].Visible = false;
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = grid.Rows[e.RowIndex];
            _maBenhNhanDangChon = Convert.ToInt32(row.Cells["MaBenhNhan"].Value);
            CapNhatThongTinChon();
        }

        private void CapNhatThongTinChon()
        {
            if (_maBenhNhanDangChon == null) return;
            var bn = _service.LayTheoMa(_maBenhNhanDangChon.Value);
            lblBenhNhanDangChon.Text = $"Bệnh nhân: {bn.HoTen} (mã {bn.MaBenhNhan}) - Giấy tờ: {bn.SoGiayTo}";

            var hs = _service.LayHoSoTheoBenhNhan(bn.MaBenhNhan);
            bool daThuNamNay = _service.DaThuLePhiNam(bn.MaBenhNhan, (int)numNam.Value);
            lblTrangThaiHoSo.Text = hs == null
                ? $"Trạng thái hồ sơ: CHƯA CÓ (sẽ được cấp ngay sau khi thu đủ lệ phí năm {numNam.Value})"
                : $"Trạng thái hồ sơ: {hs.TrangThai} - Mã hồ sơ: {hs.MaHoSo}" +
                  (daThuNamNay ? $" | Đã thu lệ phí năm {numNam.Value}" : $" | CHƯA thu lệ phí năm {numNam.Value}");
        }

        private void BtnThu_Click(object sender, EventArgs e)
        {
            if (_maBenhNhanDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn bệnh nhân từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var kq = _service.ThuLePhiVaCapHoSo(
                    _maBenhNhanDangChon.Value, (int)numNam.Value, numSoTien.Value,
                    cboPhuongThuc.SelectedItem?.ToString(), _nguoiDung.MaNhanVien);

                string thongBao = kq.DaTaoHoSoMoi
                    ? $"Thu lệ phí thành công và đã CẤP HỒ SƠ MỚI (mã hồ sơ {kq.MaHoSo})."
                    : $"Thu lệ phí năm {numNam.Value} thành công cho hồ sơ hiện có (mã hồ sơ {kq.MaHoSo}).";
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CapNhatThongTinChon();
                TaiDanhSach();
            }
            catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                // TC03: đã có lệ phí cùng năm -> UNIQUE(MaBenhNhan, NamApDung) từ chối.
                MessageBox.Show("Bệnh nhân đã thu lệ phí năm này rồi (không thể thu trùng).", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thực hiện: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
