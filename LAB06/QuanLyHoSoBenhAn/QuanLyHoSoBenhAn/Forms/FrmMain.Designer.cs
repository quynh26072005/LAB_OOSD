using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private Panel _menuPanel;
        private Label _lblUser;

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
            _menuPanel = new Panel { Dock = DockStyle.Left, Width = 230, BackColor = Color.FromArgb(32, 54, 82) };
            Controls.Add(_menuPanel);

            _lblUser = new Label
            {
                Text = $"{_nhanVien.HoTen}\n{TenVaiTro(_nhanVien.VaiTro)}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 15),
                Size = new Size(210, 50)
            };
            _menuPanel.Controls.Add(_lblUser);

            int y = 80;
            foreach (var muc in LayMenuTheoVaiTro())
            {
                var btn = TaoNutMenu(muc.Item1, y);
                btn.Click += (s, e) => muc.Item2();
                _menuPanel.Controls.Add(btn);
                y += 46;
            }

            var btnThoat = TaoNutMenu("Đăng xuất", y + 20);
            btnThoat.BackColor = Color.FromArgb(150, 40, 40);
            btnThoat.Click += (s, e) => { Application.Restart(); };
            _menuPanel.Controls.Add(btnThoat);
        }

        private Button TaoNutMenu(string text, int y)
        {
            return new Button
            {
                Text = text,
                Location = new Point(10, y),
                Size = new Size(210, 38),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(45, 74, 110),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
        }
    }
}
