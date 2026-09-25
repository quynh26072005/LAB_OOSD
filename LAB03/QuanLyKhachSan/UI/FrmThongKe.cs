using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.UI
{
    public partial class FrmThongKe : Form
    {
        private ThongKeService _service;

        // UI Controls
        private Label lblTuNgay, lblDenNgay;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnThongKe;

        private Label lblPhieuDatVal, lblDangOVal, lblHoaDonVal, lblDoanhThuVal, lblTongDenBuVal;
        private Label lblDichVuHeader;
        private DataGridView dgvDichVu;

        public FrmThongKe()
        {
            InitializeComponentCustom();
            _service = new ThongKeService();
            ThucHienThongKe();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Thống kê khách sạn";
            this.Size = new Size(620, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 244, 248);

            Font fontRegular = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            Font fontBold = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            // 1. Thanh lọc ngày
            lblTuNgay = new Label { Text = "Từ ngày:", Location = new Point(25, 20), AutoSize = true, Font = fontRegular };
            dtpTuNgay = new DateTimePicker { Location = new Point(85, 17), Width = 110, Format = DateTimePickerFormat.Short, Font = fontRegular, Value = new DateTime(2026, 9, 1) };

            lblDenNgay = new Label { Text = "Đến ngày:", Location = new Point(210, 20), AutoSize = true, Font = fontRegular };
            dtpDenNgay = new DateTimePicker { Location = new Point(280, 17), Width = 110, Format = DateTimePickerFormat.Short, Font = fontRegular, Value = new DateTime(2026, 9, 30) };

            btnThongKe = new Button
            {
                Text = "Thống kê",
                Location = new Point(410, 15),
                Size = new Size(90, 28),
                Font = fontRegular,
                BackColor = Color.FromArgb(230, 235, 240),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnThongKe.FlatAppearance.BorderColor = Color.Gray;
            btnThongKe.Click += (s, e) => ThucHienThongKe();

            // 2. Khối hiển thị các chỉ số tổng quan
            // Cột trái
            lblPhieuDatVal = new Label { Text = "Phiếu đặt: 0", Location = new Point(25, 65), AutoSize = true, Font = fontRegular };
            lblHoaDonVal = new Label { Text = "Hóa đơn: 0", Location = new Point(25, 95), AutoSize = true, Font = fontRegular };
            lblTongDenBuVal = new Label { Text = "Tổng đền bù: 0 đ", Location = new Point(25, 125), AutoSize = true, Font = fontRegular };

            // Cột phải
            lblDangOVal = new Label { Text = "Đang ở: 0", Location = new Point(300, 65), AutoSize = true, Font = fontRegular };
            lblDoanhThuVal = new Label { Text = "Doanh thu HĐ: 0 đ", Location = new Point(300, 95), AutoSize = true, Font = fontBold };

            // 3. Tiêu đề danh sách dịch vụ
            lblDichVuHeader = new Label { Text = "Dịch vụ sử dụng:", Location = new Point(25, 165), AutoSize = true, Font = fontBold };

            // 4. DataGridView Dịch vụ
            dgvDichVu = new DataGridView
            {
                Location = new Point(25, 190),
                Size = new Size(550, 220),
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false
            };

            // Thêm tất cả controls vào Form
            this.Controls.Add(lblTuNgay); this.Controls.Add(dtpTuNgay);
            this.Controls.Add(lblDenNgay); this.Controls.Add(dtpDenNgay);
            this.Controls.Add(btnThongKe);

            this.Controls.Add(lblPhieuDatVal); this.Controls.Add(lblDangOVal);
            this.Controls.Add(lblHoaDonVal); this.Controls.Add(lblDoanhThuVal);
            this.Controls.Add(lblTongDenBuVal);

            this.Controls.Add(lblDichVuHeader);
            this.Controls.Add(dgvDichVu);
        }

        private void ThucHienThongKe()
        {
            DateTime tuNgay = dtpTuNgay.Value;
            DateTime denNgay = dtpDenNgay.Value;

            // Load số liệu chỉ số
            ThongKeModel tk = _service.GetThongKeTongQuan(tuNgay, denNgay);
            lblPhieuDatVal.Text = $"Phiếu đặt: {tk.SoPhieuDat}";
            lblDangOVal.Text = $"Đang ở: {tk.SoDangO}";
            lblHoaDonVal.Text = $"Hóa đơn: {tk.SoHoaDon}";
            lblDoanhThuVal.Text = $"Doanh thu HĐ: {tk.DoanhThuHD:N0} đ";
            lblTongDenBuVal.Text = $"Tổng đền bù: {tk.TongDenBu:N0} đ";

            // Load grid dịch vụ
            DataTable dt = _service.GetDichVuSuDung(tuNgay, denNgay);
            dgvDichVu.DataSource = dt;

            // Đổi tên cột hiển thị tiếng Việt
            if (dgvDichVu.Columns.Count > 0)
            {
                dgvDichVu.Columns["MaDV"].HeaderText = "Mã DV";
                dgvDichVu.Columns["TenDichVu"].HeaderText = "Tên dịch vụ";
                dgvDichVu.Columns["TongSoLuong"].HeaderText = "Tổng số lượng";
                dgvDichVu.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvDichVu.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            }
        }
    }
}