using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Service;

namespace QuanLyNhanSu.UI
{
    partial class HopDongForm
    {
        private IContainer components = null;

        private DataGridView _gridHD, _gridBH;
        private TextBox _txtSoHD, _txtMucBHYT, _txtMucBHXH;
        private ComboBox _cboNV, _cboLoaiHD;
        private DateTimePicker _dtKy, _dtBatDau, _dtKetThuc;
        private CheckBox _chkKetThuc;

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
            this.Text = "Hợp đồng & Bảo hiểm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(1000, 640);

            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(BuildHopDongTab());
            tabs.TabPages.Add(BuildBaoHiemTab());
            Controls.Add(tabs);
        }

        private TabPage BuildHopDongTab()
        {
            TabPage page = new TabPage("Hợp đồng");

            _gridHD = MakeGrid(new Point(10, 10), new Size(620, 520));
            page.Controls.Add(_gridHD);

            GroupBox box = new GroupBox
            {
                Text = "Ký hợp đồng chính thức",
                Location = new Point(640, 10),
                Size = new Size(330, 550)
            };
            int y = 24;
            _txtSoHD = AddTextBox(box, "Số hợp đồng", ref y);
            _cboNV = AddCombo(box, "Nhân viên", ref y);
            _cboLoaiHD = AddCombo(box, "Loại hợp đồng", ref y);
            _cboLoaiHD.Items.AddRange(new object[] { LoaiHopDong.DaiHan, LoaiHopDong.NganHan });
            _cboLoaiHD.SelectedIndex = 1;
            _cboLoaiHD.SelectedIndexChanged += (s, e) =>
            {
                bool daiHan = _cboLoaiHD.SelectedItem?.ToString() == LoaiHopDong.DaiHan;
                _txtMucBHYT.Enabled = daiHan;
                _txtMucBHXH.Enabled = daiHan;
            };

            Label l1 = new Label { Text = "Ngày ký", Location = new Point(16, y), AutoSize = true };
            _dtKy = new DateTimePicker { Location = new Point(16, y + 22), Width = 290, Format = DateTimePickerFormat.Short };
            box.Controls.Add(l1); box.Controls.Add(_dtKy); y += 66;

            Label l2 = new Label { Text = "Ngày bắt đầu", Location = new Point(16, y), AutoSize = true };
            _dtBatDau = new DateTimePicker { Location = new Point(16, y + 22), Width = 290, Format = DateTimePickerFormat.Short };
            box.Controls.Add(l2); box.Controls.Add(_dtBatDau); y += 66;

            _chkKetThuc = new CheckBox { Text = "Có ngày kết thúc", Location = new Point(16, y), AutoSize = true };
            _dtKetThuc = new DateTimePicker { Location = new Point(16, y + 24), Width = 290, Format = DateTimePickerFormat.Short, Enabled = false };
            _chkKetThuc.CheckedChanged += (s, e) => _dtKetThuc.Enabled = _chkKetThuc.Checked;
            box.Controls.Add(_chkKetThuc); box.Controls.Add(_dtKetThuc); y += 72;

            _txtMucBHYT = AddTextBox(box, "Mức đóng BHYT", ref y);
            _txtMucBHXH = AddTextBox(box, "Mức đóng BHXH", ref y);
            _txtMucBHYT.Text = "45000";
            _txtMucBHXH.Text = "1050000";
            _txtMucBHYT.Enabled = false;
            _txtMucBHXH.Enabled = false;

            Button ky = new Button { Text = "Ký hợp đồng", Location = new Point(16, y + 6), Width = 290, Height = 38 };
            ky.Click += (s, e) => KyHopDong();
            box.Controls.Add(ky);

            page.Controls.Add(box);
            return page;
        }

        private TabPage BuildBaoHiemTab()
        {
            TabPage page = new TabPage("Bảo hiểm");
            _gridBH = MakeGrid(new Point(10, 10), new Size(960, 520));
            page.Controls.Add(_gridBH);
            return page;
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

        private static TextBox AddTextBox(GroupBox box, string label, ref int y)
        {
            Label l = new Label { Text = label, Location = new Point(16, y), AutoSize = true };
            TextBox t = new TextBox { Location = new Point(16, y + 22), Width = 290 };
            box.Controls.Add(l);
            box.Controls.Add(t);
            y += 66;
            return t;
        }

        private static ComboBox AddCombo(GroupBox box, string label, ref int y)
        {
            Label l = new Label { Text = label, Location = new Point(16, y), AutoSize = true };
            ComboBox c = new ComboBox { Location = new Point(16, y + 22), Width = 290, DropDownStyle = ComboBoxStyle.DropDownList };
            box.Controls.Add(l);
            box.Controls.Add(c);
            y += 66;
            return c;
        }
    }
}