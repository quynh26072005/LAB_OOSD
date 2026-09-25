using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.BUS; // Đảm bảo namespace chứa DichVuService

namespace QuanLyKhachSan.UI
{
    public partial class FrmDichVu : Form
    {
        private DichVuService dichVuService;

        // Các Control giao diện
        private Label lblPhieuLuuTru;
        private TextBox txtPhieuLuuTru;
        private Label lblPhong;
        private TextBox txtPhong;
        private Label lblDichVu;
        private TextBox txtDichVu;

        private Label lblNgaySuDung;
        private TextBox txtNgaySuDung;
        private Label lblSoLuong;
        private TextBox txtSoLuong;
        private Button btnGhiNhan;

        private DataGridView dgvSuDungDichVu;

        public FrmDichVu()
        {
            dichVuService = new DichVuService();
            ThietKeGiaoDien();
            HienThiDanhSachDichVu();
        }

        private void ThietKeGiaoDien()
        {
            // Cấu hình Form
            this.Text = "Sử dụng dịch vụ";
            this.Size = new Size(820, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(238, 243, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Font fontStandard = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Hàng nhập liệu 1
            int yRow1 = 25;
            lblPhieuLuuTru = new Label { Text = "Phiếu lưu trú:", Location = new Point(20, yRow1 + 3), AutoSize = true, Font = fontStandard };
            txtPhieuLuuTru = new TextBox { Location = new Point(105, yRow1), Size = new Size(130, 23), Text = "DP001", Font = fontStandard };

            lblPhong = new Label { Text = "Phòng:", Location = new Point(255, yRow1 + 3), AutoSize = true, Font = fontStandard };
            txtPhong = new TextBox { Location = new Point(305, yRow1), Size = new Size(90, 23), Text = "A101", Font = fontStandard };

            lblDichVu = new Label { Text = "Dịch vụ:", Location = new Point(415, yRow1 + 3), AutoSize = true, Font = fontStandard };
            txtDichVu = new TextBox { Location = new Point(470, yRow1), Size = new Size(150, 23), Text = "Ăn sáng", Font = fontStandard };

            // Hàng nhập liệu 2
            int yRow2 = 60;
            lblNgaySuDung = new Label { Text = "Ngày sử dụng:", Location = new Point(20, yRow2 + 3), AutoSize = true, Font = fontStandard };
            txtNgaySuDung = new TextBox { Location = new Point(105, yRow2), Size = new Size(130, 23), Text = "12/09/2026", Font = fontStandard };

            lblSoLuong = new Label { Text = "Số lượng:", Location = new Point(255, yRow2 + 3), AutoSize = true, Font = fontStandard };
            txtSoLuong = new TextBox { Location = new Point(305, yRow2), Size = new Size(90, 23), Text = "2", Font = fontStandard };

            btnGhiNhan = new Button
            {
                Text = "Ghi nhận",
                Location = new Point(470, yRow2 - 2),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(240, 240, 240),
                FlatStyle = FlatStyle.Flat,
                Font = fontStandard
            };
            btnGhiNhan.FlatAppearance.BorderColor = Color.Gray;
            btnGhiNhan.Click += BtnGhiNhan_Click;

            // Bảng DataGridView
            dgvSuDungDichVu = new DataGridView();
            dgvSuDungDichVu.Location = new Point(20, 105);
            dgvSuDungDichVu.Size = new Size(765, 350);
            dgvSuDungDichVu.BackgroundColor = Color.White;
            dgvSuDungDichVu.BorderStyle = BorderStyle.Fixed3D;
            dgvSuDungDichVu.AllowUserToAddRows = false;
            dgvSuDungDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSuDungDichVu.RowHeadersVisible = false;

            dgvSuDungDichVu.EnableHeadersVisualStyles = false;
            dgvSuDungDichVu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            dgvSuDungDichVu.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvSuDungDichVu.ColumnHeadersDefaultCellStyle.Font = fontStandard;
            dgvSuDungDichVu.ColumnHeadersHeight = 28;

            // Thêm các control vào Form
            this.Controls.Add(lblPhieuLuuTru);
            this.Controls.Add(txtPhieuLuuTru);
            this.Controls.Add(lblPhong);
            this.Controls.Add(txtPhong);
            this.Controls.Add(lblDichVu);
            this.Controls.Add(txtDichVu);

            this.Controls.Add(lblNgaySuDung);
            this.Controls.Add(txtNgaySuDung);
            this.Controls.Add(lblSoLuong);
            this.Controls.Add(txtSoLuong);
            this.Controls.Add(btnGhiNhan);

            this.Controls.Add(dgvSuDungDichVu);
        }

        private void HienThiDanhSachDichVu()
        {
            if (dichVuService != null)
            {
                dgvSuDungDichVu.DataSource = dichVuService.LayDanhSachSuDungDichVu();
            }
        }

        private void BtnGhiNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhieuLuuTru.Text) || string.IsNullOrWhiteSpace(txtDichVu.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong = int.TryParse(txtSoLuong.Text, out int sl) ? sl : 1;
            decimal donGia = dichVuService.LayDonGiaDichVu(txtDichVu.Text);

            bool ketQua = dichVuService.GhiNhanSuDungDichVu(
                txtPhieuLuuTru.Text,
                txtPhong.Text,
                txtNgaySuDung.Text,
                txtDichVu.Text,
                soLuong,
                donGia
            );

            if (ketQua)
            {
                MessageBox.Show("Ghi nhận dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi ghi nhận!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}