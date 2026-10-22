using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmXetNghiem: tiếp nhận chỉ định và cập nhật kết quả (mục 3.7, TC09-TC11).</summary>
    public partial class FrmXetNghiem : Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly KhamBenhService _khamService = new KhamBenhService();

        private int? _maChiDinhDangChon;

        public FrmXetNghiem(NhanVien nguoiDung)
        {
            _nguoiDung = nguoiDung;
            Text = "Xét nghiệm";
            InitializeComponent();
            TaiHangDoi();
        }

        private void TaiHangDoi()
        {
            gridHangDoi.DataSource = _khamService.LayHangDoiXetNghiem();
            if (gridHangDoi.Columns["MaPhieuKham"] != null) gridHangDoi.Columns["MaPhieuKham"].Visible = false;
        }

        private void GridHangDoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = gridHangDoi.Rows[e.RowIndex];
            _maChiDinhDangChon = Convert.ToInt32(row.Cells["MaChiDinh"].Value);
            lblChiDinhDangChon.Text = $"Chỉ định #{_maChiDinhDangChon}\nBệnh nhân: {row.Cells["HoTen"].Value}\nXét nghiệm: {row.Cells["TenXetNghiem"].Value}";
            txtKetQua.Text = txtDonVi.Text = txtKhoangThamChieu.Text = txtNhanXet.Text = "";
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (_maChiDinhDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một chỉ định ở danh sách bên trái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtKetQua.Text))
            {
                MessageBox.Show("Vui lòng nhập kết quả.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _khamService.NhapKetQua(_maChiDinhDangChon.Value, _nguoiDung.MaNhanVien,
                    txtKetQua.Text, txtDonVi.Text, txtKhoangThamChieu.Text, txtNhanXet.Text, _nguoiDung.MaNhanVien);
                MessageBox.Show("Đã lưu kết quả xét nghiệm.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _maChiDinhDangChon = null;
                lblChiDinhDangChon.Text = "Chưa chọn chỉ định.";
                TaiHangDoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
