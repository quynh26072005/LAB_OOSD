using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu.UI
{
    partial class KhenThuongKyLuatForm
    {
        private IContainer components = null;

        private DataGridView _gridKT, _gridPC, _gridKL, _gridPT;
        private TextBox _txtSoQDKT, _txtNoiDungKT, _txtNguoiKyKT, _txtLoaiKT, _txtTienKT, _txtPhieuChi, _txtNgayChi;
        private ComboBox _cboDoiTuongKT;
        private TextBox _txtSoQDKL, _txtNoiDungKL, _txtNguoiKyKL, _txtHinhThucKL, _txtTienKL, _txtPhieuThu;
        private ComboBox _cboDoiTuongKL, _cboHinhThucThu;
        private DateTimePicker _dtLapKT, _dtLapKL;

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
            this.Text = "Khen thưởng & Kỷ luật";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(1080, 680);

            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(BuildTabKhenThuong());
            tabs.TabPages.Add(BuildTabKyLuat());
            Controls.Add(tabs);
        }

        /* ==================== Khen thưởng ==================== */
        private TabPage BuildTabKhenThuong()
        {
            TabPage page = new TabPage("Khen thưởng");

            _gridKT = MakeGrid(new Point(10, 10), new Size(690, 300));
            page.Controls.Add(_gridKT);

            GroupBox box = new GroupBox
            {
                Text = "Lập quyết định khen thưởng",
                Location = new Point(10, 320),
                Size = new Size(690, 310)
            };
            int y = 24;
            _txtSoQDKT = AddTxt(box, "Số QĐ", 16, ref y, 190);
            Label ld = new Label { Text = "Ngày lập", Location = new Point(220, y - 44), AutoSize = true };
            _dtLapKT = new DateTimePicker { Location = new Point(220, y - 22), Width = 150, Format = DateTimePickerFormat.Short };
            box.Controls.Add(ld); box.Controls.Add(_dtLapKT);
            _txtNguoiKyKT = AddTxt(box, "Người ký", 390, ref y, 190);
            _txtLoaiKT = AddTxt(box, "Loại khen thưởng", 16, ref y, 190);
            _txtTienKT = AddTxt(box, "Số tiền", 220, ref y, 190);
            Label ldt = new Label { Text = "Đối tượng (cá nhân/tập thể)", Location = new Point(430, y - 44), AutoSize = true };
            _cboDoiTuongKT = new ComboBox { Location = new Point(430, y - 22), Width = 190, DropDownStyle = ComboBoxStyle.DropDown };
            box.Controls.Add(ldt); box.Controls.Add(_cboDoiTuongKT);
            Label lnd = new Label { Text = "Nội dung", Location = new Point(16, y), AutoSize = true };
            _txtNoiDungKT = new TextBox { Location = new Point(16, y + 22), Width = 570, Multiline = true, Height = 40 };
            box.Controls.Add(lnd); box.Controls.Add(_txtNoiDungKT);
            y += 68;
            _txtPhieuChi = AddTxt(box, "Số phiếu chi", 16, ref y, 190);
            Label lnc = new Label { Text = "Ngày chi", Location = new Point(220, y - 44), AutoSize = true };
            _txtNgayChi = new TextBox { Location = new Point(220, y - 22), Width = 150, Text = DateTime.Today.ToShortDateString() };
            box.Controls.Add(lnc); box.Controls.Add(_txtNgayChi);
            Button lap = new Button { Text = "Lập QĐ + Phiếu chi", Location = new Point(430, y - 22), Width = 190, Height = 40 };
            lap.Click += (s, e) => LapKhenThuong();
            box.Controls.Add(lap);
            page.Controls.Add(box);

            GroupBox gbPC = new GroupBox { Text = "Danh sách phiếu chi khen thưởng", Location = new Point(710, 10), Size = new Size(350, 620) };
            _gridPC = MakeGrid(new Point(10, 20), new Size(330, 590));
            gbPC.Controls.Add(_gridPC);
            page.Controls.Add(gbPC);

            return page;
        }

        /* ==================== Kỷ luật ==================== */
        private TabPage BuildTabKyLuat()
        {
            TabPage page = new TabPage("Kỷ luật");

            _gridKL = MakeGrid(new Point(10, 10), new Size(690, 300));
            page.Controls.Add(_gridKL);

            GroupBox box = new GroupBox
            {
                Text = "Lập quyết định kỷ luật",
                Location = new Point(10, 320),
                Size = new Size(690, 310)
            };
            int y = 24;
            _txtSoQDKL = AddTxt(box, "Số QĐ", 16, ref y, 190);
            Label ld = new Label { Text = "Ngày lập", Location = new Point(220, y - 44), AutoSize = true };
            _dtLapKL = new DateTimePicker { Location = new Point(220, y - 22), Width = 150, Format = DateTimePickerFormat.Short };
            box.Controls.Add(ld); box.Controls.Add(_dtLapKL);
            _txtNguoiKyKL = AddTxt(box, "Người ký", 390, ref y, 190);
            _txtHinhThucKL = AddTxt(box, "Hình thức kỷ luật", 16, ref y, 190);
            _txtTienKL = AddTxt(box, "Số tiền phạt", 220, ref y, 190);
            Label ldt = new Label { Text = "Đối tượng (cá nhân/tập thể)", Location = new Point(430, y - 44), AutoSize = true };
            _cboDoiTuongKL = new ComboBox { Location = new Point(430, y - 22), Width = 190, DropDownStyle = ComboBoxStyle.DropDown };
            box.Controls.Add(ldt); box.Controls.Add(_cboDoiTuongKL);
            Label lnd = new Label { Text = "Nội dung", Location = new Point(16, y), AutoSize = true };
            _txtNoiDungKL = new TextBox { Location = new Point(16, y + 22), Width = 570, Multiline = true, Height = 40 };
            box.Controls.Add(lnd); box.Controls.Add(_txtNoiDungKL);
            y += 68;
            _txtPhieuThu = AddTxt(box, "Số phiếu thu", 16, ref y, 190);
            Label lht = new Label { Text = "Hình thức thu", Location = new Point(220, y - 44), AutoSize = true };
            _cboHinhThucThu = new ComboBox
            {
                Location = new Point(220, y - 22),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cboHinhThucThu.Items.AddRange(new object[] { "Thu tiền mặt", "Trừ lương" });
            _cboHinhThucThu.SelectedIndex = 0;
            box.Controls.Add(lht); box.Controls.Add(_cboHinhThucThu);
            Button lap = new Button { Text = "Lập QĐ + Phiếu thu", Location = new Point(430, y - 22), Width = 190, Height = 40 };
            lap.Click += (s, e) => LapKyLuat();
            box.Controls.Add(lap);
            page.Controls.Add(box);

            GroupBox gbPT = new GroupBox { Text = "Danh sách phiếu thu nộp phạt", Location = new Point(710, 10), Size = new Size(350, 620) };
            _gridPT = MakeGrid(new Point(10, 20), new Size(330, 590));
            gbPT.Controls.Add(_gridPT);
            page.Controls.Add(gbPT);

            return page;
        }

        /* ==================== Tiện ích bố cục ==================== */
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

        private static TextBox AddTxt(GroupBox box, string label, int x, ref int y, int w)
        {
            Label l = new Label { Text = label, Location = new Point(x, y), AutoSize = true };
            TextBox t = new TextBox { Location = new Point(x, y + 22), Width = w };
            box.Controls.Add(l);
            box.Controls.Add(t);
            return t;
        }
    }
}