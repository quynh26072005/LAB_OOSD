using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu.UI
{
    partial class BangLuongForm
    {
        private IContainer components = null;

        private DataGridView _gridBL, _gridCT;
        private TextBox _txtMaBL;
        private NumericUpDown _numThang, _numNam;
        private Label _lblTong;

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
            this.Text = "Bảng lương";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(1060, 660);

            _gridBL = MakeGrid(new Point(10, 10), new Size(400, 300));
            _gridBL.SelectionChanged += (s, e) => LoadChiTiet();
            Controls.Add(_gridBL);

            GroupBox box = new GroupBox { Text = "Tính lương", Location = new Point(420, 10), Size = new Size(620, 130) };
            int y = 26;
            Label l1 = new Label { Text = "Mã bảng lương", Location = new Point(16, y), AutoSize = true };
            _txtMaBL = new TextBox { Location = new Point(16, y + 22), Width = 180 };
            box.Controls.Add(l1); box.Controls.Add(_txtMaBL);

            Label l2 = new Label { Text = "Tháng", Location = new Point(210, y), AutoSize = true };
            _numThang = new NumericUpDown { Location = new Point(210, y + 22), Width = 80, Minimum = 1, Maximum = 12, Value = DateTime.Today.Month };
            box.Controls.Add(l2); box.Controls.Add(_numThang);

            Label l3 = new Label { Text = "Năm", Location = new Point(310, y), AutoSize = true };
            _numNam = new NumericUpDown { Location = new Point(310, y + 22), Width = 100, Minimum = 2000, Maximum = 2100, Value = DateTime.Today.Year };
            box.Controls.Add(l3); box.Controls.Add(_numNam);

            Button tinh = new Button { Text = "Tính lương", Location = new Point(430, y + 18), Width = 160, Height = 36 };
            tinh.Click += (s, e) => TinhLuong();
            box.Controls.Add(tinh);
            Controls.Add(box);

            _lblTong = new Label
            {
                Location = new Point(420, 150),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            Controls.Add(_lblTong);

            _gridCT = MakeGrid(new Point(10, 320), new Size(1030, 300));
            Controls.Add(_gridCT);
        }

        private static DataGridView MakeGrid(Point loc, Size size) => new DataGridView
        {
            Location = loc,
            Size = size,
            ReadOnly = true,
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };
    }
}