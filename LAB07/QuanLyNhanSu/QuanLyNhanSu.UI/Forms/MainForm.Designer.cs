using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu.UI
{
    partial class MainForm
    {
        private IContainer components = null;

        private Button btnDanhMuc;
        private Button btnNhanVien;
        private Button btnQuyetDinh;
        private Button btnHopDong;
        private Button btnChamCong;
        private Button btnBangLuong;
        private Button btnKhenThuong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Text = "Quản lý nhân sự - Công ty Sơn Đông Á";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(320, 560);
            this.MinimumSize = new Size(300, 520);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Label title = new Label
            {
                Text = "QUẢN LÝ NHÂN SỰ",
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                BackColor = Color.FromArgb(30, 60, 120),
                ForeColor = Color.White
            };

            Panel menu = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };

            this.btnDanhMuc = MenuButton("Danh mục (Phòng ban, Chức vụ, Ca)", 0);
            this.btnNhanVien = MenuButton("Quản lý Nhân viên", 1);
            this.btnQuyetDinh = MenuButton("Quyết định (Tuyển dụng, Chức vụ, Lương)", 2);
            this.btnHopDong = MenuButton("Hợp đồng & Bảo hiểm", 3);
            this.btnChamCong = MenuButton("Phân công & Chấm công & Phụ trội", 4);
            this.btnBangLuong = MenuButton("Bảng lương", 5);
            this.btnKhenThuong = MenuButton("Khen thưởng & Kỷ luật", 6);

            menu.Controls.Add(this.btnDanhMuc);
            menu.Controls.Add(this.btnNhanVien);
            menu.Controls.Add(this.btnQuyetDinh);
            menu.Controls.Add(this.btnHopDong);
            menu.Controls.Add(this.btnChamCong);
            menu.Controls.Add(this.btnBangLuong);
            menu.Controls.Add(this.btnKhenThuong);

            this.Controls.Add(menu);
            this.Controls.Add(title);
            this.ResumeLayout(false);
        }

        private Button MenuButton(string text, int index)
        {
            int y = 4 + index * 52;
            Button btn = new Button
            {
                Text = text,
                Location = new Point(4, y),
                Width = 280,
                Height = 44,
                Font = new Font("Segoe UI", 10f),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 245, 250),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(150, 170, 200);

            switch (index)
            {
                case 0: btn.Click += new System.EventHandler(this.BtnDanhMuc_Click); break;
                case 1: btn.Click += new System.EventHandler(this.BtnNhanVien_Click); break;
                case 2: btn.Click += new System.EventHandler(this.BtnQuyetDinh_Click); break;
                case 3: btn.Click += new System.EventHandler(this.BtnHopDong_Click); break;
                case 4: btn.Click += new System.EventHandler(this.BtnChamCong_Click); break;
                case 5: btn.Click += new System.EventHandler(this.BtnBangLuong_Click); break;
                case 6: btn.Click += new System.EventHandler(this.BtnKhenThuong_Click); break;
            }
            return btn;
        }
    }
}