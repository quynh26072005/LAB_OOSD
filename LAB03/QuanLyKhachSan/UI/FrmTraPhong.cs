using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.UI
{
    public partial class FrmTraPhong : Form
    {
        private TraPhongService _service;

        // UI Controls
        private Label lblPhieuDangO, lblSoPhieuDenBu, lblMucDo, lblSoTienDenBu, lblSoHoaDon, lblSoNgayTinhTien, lblHinhThuc, lblSoTienThanhToan;
        private TextBox txtPhieuDangO, txtSoPhieuDenBu, txtMucDo, txtSoTienDenBu, txtSoHoaDon, txtSoNgayTinhTien, txtHinhThuc, txtSoTienThanhToan;
        private Button btnLapPhieuDenBu, btnLapHoaDon, btnThanhToan, btnHoanTatTraPhong;
        private DataGridView dgvPhong, dgvTienNghi, dgvTienNghiDenBu, dgvHoaDon;

        public FrmTraPhong()
        {
            InitializeComponentCustom();
            _service = new TraPhongService();
            LoadDataMacDinh();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Trả phòng - Đền bù - Hóa đơn - Thanh toán";
            this.Size = new Size(820, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 244, 248);

            Font fontText = new Font("Segoe UI", 9F, FontStyle.Regular);

            // 1. Phiếu đang ở
            lblPhieuDangO = new Label { Text = "Phiếu đang ở:", Location = new Point(25, 20), AutoSize = true, Font = fontText };
            txtPhieuDangO = new TextBox { Location = new Point(125, 17), Size = new Size(130, 23), Font = fontText, Text = "DP001" };
            txtPhieuDangO.TextChanged += (s, e) => LoadDataTheoPhieu();

            // 2. DataGridViews Top
            dgvPhong = TaoGrid(new Point(25, 50), new Size(230, 110));
            dgvTienNghi = TaoGrid(new Point(270, 50), new Size(240, 110));
            dgvTienNghiDenBu = TaoGrid(new Point(525, 50), new Size(245, 110));

            // 3. Khối Phiếu Đền Bù
            lblSoPhieuDenBu = new Label { Text = "Số phiếu đền bù:", Location = new Point(25, 175), AutoSize = true, Font = fontText };
            txtSoPhieuDenBu = new TextBox { Location = new Point(125, 172), Size = new Size(130, 23), Font = fontText, Text = "DB001" };

            lblMucDo = new Label { Text = "Mức độ:", Location = new Point(270, 175), AutoSize = true, Font = fontText };
            txtMucDo = new TextBox { Location = new Point(330, 172), Size = new Size(120, 23), Font = fontText, Text = "Hư hỏng nhẹ" };

            lblSoTienDenBu = new Label { Text = "Số tiền:", Location = new Point(465, 175), AutoSize = true, Font = fontText };
            txtSoTienDenBu = new TextBox { Location = new Point(515, 172), Size = new Size(110, 23), Font = fontText, Text = "500000" };

            btnLapPhieuDenBu = TaoButton("Lập phiếu đền bù", new Point(640, 168), new Size(130, 30));
            btnLapPhieuDenBu.Click += BtnLapPhieuDenBu_Click;

            // 4. Khối Hóa Đơn
            lblSoHoaDon = new Label { Text = "Số hóa đơn:", Location = new Point(25, 215), AutoSize = true, Font = fontText };
            txtSoHoaDon = new TextBox { Location = new Point(125, 212), Size = new Size(130, 23), Font = fontText, Text = "HD001" };

            lblSoNgayTinhTien = new Label { Text = "Số ngày tính tiền:", Location = new Point(270, 215), AutoSize = true, Font = fontText };
            txtSoNgayTinhTien = new TextBox { Location = new Point(380, 212), Size = new Size(70, 23), Font = fontText, Text = "2" };

            btnLapHoaDon = TaoButton("Lập hóa đơn", new Point(465, 208), new Size(120, 30));
            btnLapHoaDon.Click += BtnLapHoaDon_Click;

            // 5. DataGridView Bottom (Hóa đơn)
            dgvHoaDon = TaoGrid(new Point(25, 255), new Size(745, 150));

            // 6. Khối Thanh Toán
            lblHinhThuc = new Label { Text = "Hình thức:", Location = new Point(25, 425), AutoSize = true, Font = fontText };
            txtHinhThuc = new TextBox { Location = new Point(100, 422), Size = new Size(120, 23), Font = fontText, Text = "Thẻ" };

            lblSoTienThanhToan = new Label { Text = "Số tiền:", Location = new Point(240, 425), AutoSize = true, Font = fontText };
            txtSoTienThanhToan = new TextBox { Location = new Point(290, 422), Size = new Size(120, 23), Font = fontText, Text = "1200000" };

            btnThanhToan = TaoButton("Thanh toán", new Point(430, 418), new Size(110, 30));
            btnThanhToan.Click += BtnThanhToan_Click;

            btnHoanTatTraPhong = TaoButton("Hoàn tất trả phòng", new Point(555, 418), new Size(140, 30));
            btnHoanTatTraPhong.Click += BtnHoanTatTraPhong_Click;

            // Control Add
            this.Controls.Add(lblPhieuDangO); this.Controls.Add(txtPhieuDangO);
            this.Controls.Add(dgvPhong); this.Controls.Add(dgvTienNghi); this.Controls.Add(dgvTienNghiDenBu);
            this.Controls.Add(lblSoPhieuDenBu); this.Controls.Add(txtSoPhieuDenBu);
            this.Controls.Add(lblMucDo); this.Controls.Add(txtMucDo);
            this.Controls.Add(lblSoTienDenBu); this.Controls.Add(txtSoTienDenBu);
            this.Controls.Add(btnLapPhieuDenBu);
            this.Controls.Add(lblSoHoaDon); this.Controls.Add(txtSoHoaDon);
            this.Controls.Add(lblSoNgayTinhTien); this.Controls.Add(txtSoNgayTinhTien);
            this.Controls.Add(btnLapHoaDon);
            this.Controls.Add(dgvHoaDon);
            this.Controls.Add(lblHinhThuc); this.Controls.Add(txtHinhThuc);
            this.Controls.Add(lblSoTienThanhToan); this.Controls.Add(txtSoTienThanhToan);
            this.Controls.Add(btnThanhToan); this.Controls.Add(btnHoanTatTraPhong);
        }

        private void LoadDataMacDinh()
        {
            LoadDataTheoPhieu();
        }

        private void LoadDataTheoPhieu()
        {
            string maPhieu = txtPhieuDangO.Text.Trim();
            dgvPhong.DataSource = _service.GetThongTinPhong(maPhieu);
            dgvTienNghi.DataSource = _service.GetTienNghiPhong("P101");
            dgvTienNghiDenBu.DataSource = _service.GetTienNghiDenBu(maPhieu);
        }

        private void BtnLapPhieuDenBu_Click(object sender, EventArgs e)
        {
            decimal.TryParse(txtSoTienDenBu.Text, out decimal soTien);
            bool result = _service.LapPhieuDenBu(txtSoPhieuDenBu.Text, txtPhieuDangO.Text, txtMucDo.Text, soTien);

            if (result)
            {
                // Cập nhật lại Grid tiện nghi đền bù
                DataTable dt = (DataTable)dgvTienNghiDenBu.DataSource ?? new DataTable();
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("TienNghiDB"); dt.Columns.Add("MucDo"); dt.Columns.Add("SoTien");
                }
                dt.Rows.Add("Hư hỏng tiện nghi", txtMucDo.Text, soTien);
                dgvTienNghiDenBu.DataSource = dt;

                MessageBox.Show("Đã lập phiếu đền bù thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnLapHoaDon_Click(object sender, EventArgs e)
        {
            int.TryParse(txtSoNgayTinhTien.Text, out int soNgay);
            decimal.TryParse(txtSoTienDenBu.Text, out decimal tienDenBu);

            DataTable dtHoaDon = _service.LapHoaDon(txtSoHoaDon.Text, txtPhieuDangO.Text, soNgay, tienDenBu);
            dgvHoaDon.DataSource = dtHoaDon;

            if (dtHoaDon.Rows.Count > 0)
            {
                txtSoTienThanhToan.Text = dtHoaDon.Rows[0]["TongTien"].ToString();
            }

            MessageBox.Show("Đã lập hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Thanh toán thành công qua hình thức: {txtHinhThuc.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnHoanTatTraPhong_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đã hoàn tất thủ tục trả phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private DataGridView TaoGrid(Point pt, Size sz)
        {
            return new DataGridView
            {
                Location = pt,
                Size = sz,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false
            };
        }

        private Button TaoButton(string text, Point pt, Size sz)
        {
            Button btn = new Button
            {
                Text = text,
                Location = pt,
                Size = sz,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                BackColor = Color.FromArgb(230, 235, 240),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.Gray;
            return btn;
        }
    }
}