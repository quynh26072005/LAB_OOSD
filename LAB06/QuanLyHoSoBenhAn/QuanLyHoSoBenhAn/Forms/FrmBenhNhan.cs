using System;
using System.Data;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmBenhNhan: đăng ký, cập nhật, tìm bệnh nhân/ảnh (mục 3.7).</summary>
    public partial class FrmBenhNhan : Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly BenhNhanService _service = new BenhNhanService();

        private int? _maBenhNhanDangChon;

        public FrmBenhNhan(NhanVien nguoiDung)
        {
            _nguoiDung = nguoiDung;
            Text = "Bệnh nhân - Đăng ký / Tra cứu";
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
            int maBenhNhan = Convert.ToInt32(row.Cells["MaBenhNhan"].Value);
            NapBenhNhan(maBenhNhan);
        }

        private void NapBenhNhan(int maBenhNhan)
        {
            var bn = _service.LayTheoMa(maBenhNhan);
            if (bn == null) return;

            _maBenhNhanDangChon = bn.MaBenhNhan;
            txtHoTen.Text = bn.HoTen;
            dtpNgaySinh.Value = bn.NgaySinh;
            cboGioiTinh.SelectedItem = bn.GioiTinh;
            txtDiaChi.Text = bn.DiaChi;
            txtDienThoai.Text = bn.DienThoai;
            txtEmail.Text = bn.Email;
            txtSoGiayTo.Text = bn.SoGiayTo;
            txtAnh.Text = bn.Anh;

            var hs = _service.LayHoSoTheoBenhNhan(bn.MaBenhNhan);
            lblTrangThaiHoSo.Text = hs == null
                ? "Trạng thái hồ sơ: Chưa có hồ sơ (chưa thu lệ phí năm đầu)"
                : $"Trạng thái hồ sơ: {hs.TrangThai} - Mã hồ sơ: {hs.MaHoSo} - Ngày cấp: {hs.NgayCap:dd/MM/yyyy}";
        }

        private void LamMoiForm()
        {
            _maBenhNhanDangChon = null;
            txtHoTen.Text = txtDiaChi.Text = txtDienThoai.Text = txtEmail.Text = txtSoGiayTo.Text = txtAnh.Text = "";
            dtpNgaySinh.Value = DateTime.Today.AddYears(-30);
            cboGioiTinh.SelectedIndex = 0;
            lblTrangThaiHoSo.Text = "Trạng thái hồ sơ: (chưa chọn bệnh nhân)";
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bn = new BenhNhan
            {
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Khác",
                DiaChi = txtDiaChi.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                SoGiayTo = txtSoGiayTo.Text.Trim(),
                Anh = txtAnh.Text.Trim()
            };

            try
            {
                // TC04: nếu đang thêm mới mà số giấy tờ trùng với bệnh nhân đã có -> mở hồ sơ cũ, không tạo trùng.
                if (_maBenhNhanDangChon == null && !string.IsNullOrWhiteSpace(bn.SoGiayTo))
                {
                    var trung = _service.LayTheoSoGiayTo(bn.SoGiayTo);
                    if (trung != null)
                    {
                        MessageBox.Show(
                            $"Số giấy tờ đã tồn tại cho bệnh nhân \"{trung.HoTen}\" (mã {trung.MaBenhNhan}).\nHệ thống sẽ mở hồ sơ hiện có thay vì tạo bệnh nhân mới.",
                            "Trùng giấy tờ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NapBenhNhan(trung.MaBenhNhan);
                        TaiDanhSach();
                        return;
                    }
                }

                if (_maBenhNhanDangChon == null)
                {
                    int id = _service.ThemMoi(bn, _nguoiDung.MaNhanVien);
                    MessageBox.Show("Đăng ký bệnh nhân thành công. Mã bệnh nhân: " + id, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NapBenhNhan(id);
                }
                else
                {
                    bn.MaBenhNhan = _maBenhNhanDangChon.Value;
                    _service.CapNhat(bn, _nguoiDung.MaNhanVien);
                    MessageBox.Show("Cập nhật thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                TaiDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLichSu_Click(object sender, EventArgs e)
        {
            if (_maBenhNhanDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một bệnh nhân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hs = _service.LayHoSoTheoBenhNhan(_maBenhNhanDangChon.Value);
            if (hs == null)
            {
                MessageBox.Show("Bệnh nhân chưa có hồ sơ bệnh án.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable lichSu = _service.LayLichSuKham(hs.MaHoSo);
            using (var f = new FrmGridView("Lịch sử khám - Hồ sơ #" + hs.MaHoSo, lichSu))
            {
                f.ShowDialog(this);
            }
        }
    }
}
