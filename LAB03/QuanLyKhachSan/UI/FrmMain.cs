using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.UI
{
    public partial class FrmMain : Form
    {
        private Label lblTitle;
        private Button btnDanhMuc;
        private Button btnPhongTienNghi;
        private Button btnDatNhanPhong;
        private Button btnSuDungDichVu;
        private Button btnTraPhong;
        private Button btnThongKe;
        private Button btnThoat;

        public FrmMain()
        {
            InitializeComponent();
            VeGiaoDien();
        }

        private void VeGiaoDien()
        {
            this.Text = "Quản lý khách sạn";
            this.Size = new Size(760, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(235, 240, 245);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.Controls.Clear();

            // Tiêu đề
            lblTitle = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(12, 50, 110),
                AutoSize = true,
                Location = new Point(190, 25)
            };
            this.Controls.Add(lblTitle);

            Size btnSize = new Size(210, 65);
            Font btnFont = new Font("Segoe UI", 9.75F, FontStyle.Regular);

            int col1 = 50;
            int col2 = 270;
            int col3 = 490;

            int row1 = 80;
            int row2 = 160;
            int row3 = 240;

            // Hàng 1
            btnDanhMuc = TaoButton("Danh mục", new Point(col1, row1), btnSize, btnFont, LucIcon("DanhMuc"));
            btnDanhMuc.Click += (s, e) => MoForm(new FrmDanhMuc());

            btnPhongTienNghi = TaoButton("Phòng - Tiện nghi", new Point(col2, row1), btnSize, btnFont, LucIcon("Phong"));
            btnPhongTienNghi.Click += (s, e) => MoForm(new FrmPhong());

            btnDatNhanPhong = TaoButton("Đặt / Nhận phòng", new Point(col3, row1), btnSize, btnFont, LucIcon("DatPhong"));
            btnDatNhanPhong.Click += (s, e) => MoForm(new FrmDatPhong());

            // Hàng 2
            btnSuDungDichVu = TaoButton("Sử dụng dịch vụ", new Point(col1, row2), btnSize, btnFont, LucIcon("DichVu"));
            btnSuDungDichVu.Click += (s, e) => MoForm(new FrmDichVu());

            btnTraPhong = TaoButton("Trả phòng - Thanh toán", new Point(col2, row2), btnSize, btnFont, LucIcon("TraPhong"));
            btnTraPhong.Click += (s, e) => MoForm(new FrmTraPhong());

            // Đã cập nhật kết nối đến FrmThongKe
            btnThongKe = TaoButton("Thống kê", new Point(col3, row2), btnSize, btnFont, LucIcon("ThongKe"));
            btnThongKe.Click += (s, e) => MoForm(new FrmThongKe());

            // Hàng 3 (Ở giữa)
            btnThoat = TaoButton("Thoát", new Point(col2, row3), btnSize, btnFont, LucIcon("Thoat"));
            btnThoat.Click += (s, e) => Application.Exit();

            // Thêm các nút vào Form
            this.Controls.Add(btnDanhMuc);
            this.Controls.Add(btnPhongTienNghi);
            this.Controls.Add(btnDatNhanPhong);
            this.Controls.Add(btnSuDungDichVu);
            this.Controls.Add(btnTraPhong);
            this.Controls.Add(btnThongKe);
            this.Controls.Add(btnThoat);
        }

        private Button TaoButton(string text, Point location, Size size, Font font, Image icon)
        {
            Button btn = new Button
            {
                Text = "  " + text,
                Location = location,
                Size = size,
                Font = font,
                BackColor = Color.FromArgb(245, 245, 245),
                FlatStyle = FlatStyle.Flat,
                Image = icon,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleCenter,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Cursor = Cursors.Hand,
                Padding = new Padding(10, 0, 0, 0)
            };
            btn.FlatAppearance.BorderColor = Color.DarkGray;
            return btn;
        }

        private void MoForm(Form form)
        {
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private Image LucIcon(string loaiIcon)
        {
            Bitmap bmp = new Bitmap(36, 36);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                switch (loaiIcon)
                {
                    case "DanhMuc":
                        g.DrawRectangle(new Pen(Color.SteelBlue, 2), 8, 8, 20, 24);
                        g.FillRectangle(Brushes.DodgerBlue, 13, 5, 10, 5);
                        g.DrawLine(new Pen(Color.SteelBlue, 2), 12, 15, 24, 15);
                        g.DrawLine(new Pen(Color.SteelBlue, 2), 12, 20, 24, 20);
                        g.DrawLine(new Pen(Color.SteelBlue, 2), 12, 25, 20, 25);
                        break;

                    case "Phong":
                        g.FillRectangle(Brushes.Sienna, 4, 16, 28, 8);
                        g.FillRectangle(Brushes.SkyBlue, 6, 12, 8, 4);
                        g.FillRectangle(Brushes.SaddleBrown, 4, 10, 4, 18);
                        g.FillRectangle(Brushes.SaddleBrown, 28, 14, 4, 14);
                        break;

                    case "DatPhong":
                        g.DrawEllipse(new Pen(Color.Goldenrod, 3), 6, 10, 12, 12);
                        g.DrawLine(new Pen(Color.Goldenrod, 3), 18, 16, 30, 16);
                        g.DrawLine(new Pen(Color.Goldenrod, 3), 26, 16, 26, 22);
                        g.DrawLine(new Pen(Color.Goldenrod, 3), 22, 16, 22, 20);
                        break;

                    case "DichVu":
                        g.FillEllipse(Brushes.SlateGray, 8, 8, 20, 20);
                        g.FillEllipse(Brushes.White, 14, 14, 8, 8);
                        g.DrawLine(new Pen(Color.SlateGray, 4), 18, 4, 18, 32);
                        g.DrawLine(new Pen(Color.SlateGray, 4), 4, 18, 32, 18);
                        break;

                    case "TraPhong":
                        g.FillRectangle(Brushes.ForestGreen, 10, 6, 16, 10);
                        g.DrawString("$", new Font("Arial", 8, FontStyle.Bold), Brushes.White, 14, 5);
                        g.FillRectangle(Brushes.Goldenrod, 6, 20, 24, 6);
                        break;

                    case "ThongKe":
                        g.FillRectangle(Brushes.DodgerBlue, 6, 18, 6, 12);
                        g.FillRectangle(Brushes.Orange, 15, 12, 6, 18);
                        g.FillRectangle(Brushes.LimeGreen, 24, 6, 6, 24);
                        break;

                    case "Thoat":
                        g.FillRectangle(Brushes.SaddleBrown, 8, 6, 14, 24);
                        g.FillEllipse(Brushes.Gold, 18, 18, 3, 3);
                        g.FillPolygon(Brushes.LimeGreen, new Point[] { new Point(24, 18), new Point(32, 12), new Point(32, 24) });
                        break;
                }
            }
            return bmp;
        }
    }
}