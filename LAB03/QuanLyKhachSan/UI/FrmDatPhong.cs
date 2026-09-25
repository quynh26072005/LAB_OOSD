using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.UI
{
    public partial class FrmDatPhong : Form
    {
        // 1. Thanh Menu Tab Phía Trên
        private Panel panelTabs;
        private Label lblTabKhachHang;
        private Label lblTabDatPhong;
        private Label lblTabNhanPhong;

        // 2. Control thông tin Phiếu Đặt (Hàng Trên)
        private Label lblSoPhieuDat;
        private TextBox txtSoPhieuDat;
        private Label lblKhach;
        private TextBox txtKhach;
        private Label lblKenhDat;
        private TextBox txtKenhDat;
        private Label lblTienCoc;
        private TextBox txtTienCoc;

        // 3. Hai Bảng Chọn Phòng (Ở Giữa)
        private DataGridView dgvDanhSachPhong;
        private DataGridView dgvPhongChon;
        private Button btnLapPhieuDat;

        // 4. Bảng Phiếu Đặt Phòng (Phía Dưới)
        private Label lblPhieuDatPhongHeader;
        private DataGridView dgvPhieuDatPhong;

        public FrmDatPhong()
        {
            ThietKeGiaoDien();
        }

        private void ThietKeGiaoDien()
        {
            // Cấu hình Form
            this.Text = "Khách hàng - Đặt phòng - Nhận phòng";
            this.Size = new Size(820, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(238, 243, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Font fontStandard = new Font("Segoe UI", 9F, FontStyle.Regular);

            // -------------------------------------------------------------
            // 1. Thanh Tab Phía Trên
            // -------------------------------------------------------------
            panelTabs = new Panel();
            panelTabs.Location = new Point(20, 12);
            panelTabs.Size = new Size(765, 25);
            panelTabs.BackColor = Color.Transparent;

            lblTabKhachHang = new Label { Text = "[Khách hàng]", Location = new Point(0, 2), AutoSize = true, Font = fontStandard, ForeColor = Color.DimGray, Cursor = Cursors.Hand };
            lblTabDatPhong = new Label { Text = "[Đặt phòng]", Location = new Point(90, 2), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.Navy, Cursor = Cursors.Hand };
            lblTabNhanPhong = new Label { Text = "[Nhận phòng / Người lưu trú]", Location = new Point(180, 2), AutoSize = true, Font = fontStandard, ForeColor = Color.DimGray, Cursor = Cursors.Hand };

            panelTabs.Controls.Add(lblTabKhachHang);
            panelTabs.Controls.Add(lblTabDatPhong);
            panelTabs.Controls.Add(lblTabNhanPhong);

            // -------------------------------------------------------------
            // 2. Thông tin Phiếu đặt (Hàng Trên)
            // -------------------------------------------------------------
            int yRow1 = 45;

            lblSoPhieuDat = new Label { Text = "Số phiếu đặt:", Location = new Point(20, yRow1 + 3), AutoSize = true, Font = fontStandard };
            txtSoPhieuDat = new TextBox { Location = new Point(98, yRow1), Size = new Size(110, 23), Text = "DP001", Font = fontStandard };

            lblKhach = new Label { Text = "Khách:", Location = new Point(220, yRow1 + 3), AutoSize = true, Font = fontStandard };
            txtKhach = new TextBox { Location = new Point(265, yRow1), Size = new Size(135, 23), Text = "Nguyễn Văn A", Font = fontStandard };

            lblKenhDat = new Label { Text = "Kênh đặt:", Location = new Point(410, yRow1 + 3), AutoSize = true, Font = fontStandard };
            txtKenhDat = new TextBox { Location = new Point(470, yRow1), Size = new Size(100, 23), Text = "Website", Font = fontStandard };

            lblTienCoc = new Label { Text = "Tiền cọc:", Location = new Point(580, yRow1 + 3), AutoSize = true, Font = fontStandard };
            txtTienCoc = new TextBox { Location = new Point(640, yRow1), Size = new Size(145, 23), Text = "500000", Font = fontStandard };

            // -------------------------------------------------------------
            // 3. Hai Bảng DataGridView Song Song (Ở Giữa)
            // -------------------------------------------------------------
            int yDgvMiddle = 80;
            int hDgvMiddle = 170;

            // Bảng 1: Danh sách phòng bên trái (4 cột)
            dgvDanhSachPhong = TaoDataGridView(20, yDgvMiddle, 375, hDgvMiddle);
            dgvDanhSachPhong.Columns.Add("Phong", "Phòng");
            dgvDanhSachPhong.Columns.Add("Khu", "Khu");
            dgvDanhSachPhong.Columns.Add("SucChua", "Sức chứa");
            dgvDanhSachPhong.Columns.Add("DonGia", "Đơn giá");

            // Bảng 2: Phòng chọn bên phải (3 cột)
            dgvPhongChon = TaoDataGridView(410, yDgvMiddle, 375, hDgvMiddle);
            dgvPhongChon.Columns.Add("PhongChon", "Phòng chọn");
            dgvPhongChon.Columns.Add("SoNguoi", "Số người");
            dgvPhongChon.Columns.Add("DonGiaNgay", "Đơn giá/ngày");

            // Nút "Lập phiếu đặt" bên dưới bảng bên phải
            btnLapPhieuDat = new Button
            {
                Text = "Lập phiếu đặt",
                Location = new Point(665, yDgvMiddle + hDgvMiddle + 10),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(240, 240, 240),
                FlatStyle = FlatStyle.Flat,
                Font = fontStandard
            };
            btnLapPhieuDat.FlatAppearance.BorderColor = Color.Gray;
            btnLapPhieuDat.Click += BtnLapPhieuDat_Click;

            // -------------------------------------------------------------
            // 4. Bảng Phiếu đặt phòng (Phía Dưới)
            // -------------------------------------------------------------
            int yDgvBottom = 325;

            lblPhieuDatPhongHeader = new Label { Text = "Phiếu đặt phòng:", Location = new Point(20, yDgvBottom - 20), AutoSize = true, Font = fontStandard };

            dgvPhieuDatPhong = TaoDataGridView(20, yDgvBottom, 765, 170);
            dgvPhieuDatPhong.Columns.Add("SoPhieu", "Số phiếu");
            dgvPhieuDatPhong.Columns.Add("Khach", "Khách");
            dgvPhieuDatPhong.Columns.Add("NgayNhan", "Ngày nhận");
            dgvPhieuDatPhong.Columns.Add("NgayTraDuKien", "Ngày trả dự kiến");
            dgvPhieuDatPhong.Columns.Add("Coc", "Cọc");
            dgvPhieuDatPhong.Columns.Add("Kenh", "Kênh");
            dgvPhieuDatPhong.Columns.Add("TrangThai", "Trạng thái");

            // Nạp dữ liệu mẫu vào các bảng
            NapDuLieuMau();

            // -------------------------------------------------------------
            // Thêm các Control vào Form
            // -------------------------------------------------------------
            this.Controls.Add(panelTabs);

            this.Controls.Add(lblSoPhieuDat);
            this.Controls.Add(txtSoPhieuDat);
            this.Controls.Add(lblKhach);
            this.Controls.Add(txtKhach);
            this.Controls.Add(lblKenhDat);
            this.Controls.Add(txtKenhDat);
            this.Controls.Add(lblTienCoc);
            this.Controls.Add(txtTienCoc);

            this.Controls.Add(dgvDanhSachPhong);
            this.Controls.Add(dgvPhongChon);
            this.Controls.Add(btnLapPhieuDat);

            this.Controls.Add(lblPhieuDatPhongHeader);
            this.Controls.Add(dgvPhieuDatPhong);
        }

        // Hàm phụ trợ tạo nhanh DataGridView chuẩn định dạng
        private DataGridView TaoDataGridView(int x, int y, int width, int height)
        {
            DataGridView dgv = new DataGridView();
            dgv.Location = new Point(x, y);
            dgv.Size = new Size(width, height);
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.Fixed3D;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersWidth = 25; // Hiển thị cột mũi tên chọn hàng giống hình mẫu

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgv.ColumnHeadersHeight = 26;

            return dgv;
        }

        private void NapDuLieuMau()
        {
            // Bảng danh sách phòng khả dụng
            dgvDanhSachPhong.Rows.Add("A101", "Khu A", "2", "600,000");
            dgvDanhSachPhong.Rows.Add("A102", "Khu A", "2", "600,000");
            dgvDanhSachPhong.Rows.Add("B201", "Khu B", "4", "900,000");

            // Bảng phòng đã chọn
            dgvPhongChon.Rows.Add("A101", "2", "600,000");

            // Bảng danh sách phiếu đặt phòng bên dưới
            dgvPhieuDatPhong.Rows.Add("DP001", "Nguyễn Văn A", "25/09/2026", "28/09/2026", "500,000", "Website", "Đã cọc");
            dgvPhieuDatPhong.Rows.Add("DP002", "Trần Thị B", "26/09/2026", "27/09/2026", "300,000", "Trực tiếp", "Đã nhận");
        }

        private void BtnLapPhieuDat_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Lập phiếu đặt phòng {txtSoPhieuDat.Text} cho khách hàng {txtKhach.Text} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}