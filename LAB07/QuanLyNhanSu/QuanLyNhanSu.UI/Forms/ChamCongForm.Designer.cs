using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu.UI
{
    partial class ChamCongForm
    {
        private IContainer components = null;

        private DataGridView _gridKH, _gridCTPC, _gridCC, _gridCTCC, _gridPT, _gridCTPT;
        private TextBox _txtMaKH, _txtTuan, _txtNguoiLap, _txtMaCC, _txtSoPhieu, _txtHeSo, _txtSoGioPT;
        private ComboBox _cboKH_PB, _cboNV, _cboCa, _cboCC_PB, _cboTrangThai, _cboHinhThuc;
        private ComboBox _cboNV_CC, _cboCa_CC, _cboNV_PT;
        private DateTimePicker _dtNgayPC, _dtNgayCC, _dtNgayPT;
        private CheckBox _chkPhuTroi;
        private NumericUpDown _numThang, _numNam;

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
            this.Text = "Phân công & Chấm công & Phụ trội";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(1080, 700);

            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(BuildTabPhanCong());
            tabs.TabPages.Add(BuildTabChamCong());
            tabs.TabPages.Add(BuildTabPhuTroi());
            Controls.Add(tabs);
        }

        /* =========================== Phân công =========================== */
        private TabPage BuildTabPhanCong()
        {
            TabPage page = new TabPage("Kế hoạch phân công");

            _gridKH = MakeGrid(new Point(10, 10), new Size(400, 300));
            _gridKH.SelectionChanged += (s, e) => LoadCTPC();
            page.Controls.Add(_gridKH);

            GroupBox boxKH = new GroupBox { Text = "Kế hoạch", Location = new Point(420, 10), Size = new Size(640, 140) };
            int y = 24;
            _txtMaKH = AddTxt(boxKH, "Mã kế hoạch", 16, ref y, 150);
            _txtTuan = AddTxt(boxKH, "Tuần", 180, ref y, 120);
            _txtNguoiLap = AddTxt(boxKH, "Người lập", 316, ref y, 150);
            Label lpb = new Label { Text = "Phòng ban", Location = new Point(16, y), AutoSize = true };
            _cboKH_PB = new ComboBox { Location = new Point(16, y + 22), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            boxKH.Controls.Add(lpb); boxKH.Controls.Add(_cboKH_PB);
            AddMasterButtons(boxKH, 480, y + 22, ThemKH, XoaKH);
            page.Controls.Add(boxKH);

            _gridCTPC = MakeGrid(new Point(420, 160), new Size(640, 150));
            page.Controls.Add(_gridCTPC);

            GroupBox boxCT = new GroupBox { Text = "Thêm chi tiết phân công", Location = new Point(10, 320), Size = new Size(1050, 330) };
            int y2 = 24;
            Label lnv = new Label { Text = "Nhân viên", Location = new Point(16, y2), AutoSize = true };
            _cboNV = new ComboBox { Location = new Point(16, y2 + 22), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            boxCT.Controls.Add(lnv); boxCT.Controls.Add(_cboNV);
            Label lca = new Label { Text = "Ca làm việc", Location = new Point(210, y2), AutoSize = true };
            _cboCa = new ComboBox { Location = new Point(210, y2 + 22), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            boxCT.Controls.Add(lca); boxCT.Controls.Add(_cboCa);
            Label lng = new Label { Text = "Ngày", Location = new Point(365, y2), AutoSize = true };
            _dtNgayPC = new DateTimePicker { Location = new Point(365, y2 + 22), Width = 150, Format = DateTimePickerFormat.Short };
            boxCT.Controls.Add(lng); boxCT.Controls.Add(_dtNgayPC);
            _chkPhuTroi = new CheckBox { Text = "Có phụ trội", Location = new Point(530, y2 + 22), AutoSize = true };
            boxCT.Controls.Add(_chkPhuTroi);
            AddDetailButtons(boxCT, 700, y2, ThemCTPC, XoaCTPC);
            page.Controls.Add(boxCT);

            return page;
        }

        /* =========================== Chấm công =========================== */
        private TabPage BuildTabChamCong()
        {
            TabPage page = new TabPage("Bảng chấm công");

            _gridCC = MakeGrid(new Point(10, 10), new Size(400, 300));
            _gridCC.SelectionChanged += (s, e) => LoadCTCC();
            page.Controls.Add(_gridCC);

            GroupBox boxCC = new GroupBox { Text = "Bảng chấm công", Location = new Point(420, 10), Size = new Size(640, 140) };
            int y = 24;
            _txtMaCC = AddTxt(boxCC, "Mã bảng", 16, ref y, 140);
            Label lt = new Label { Text = "Tháng", Location = new Point(170, y - 44), AutoSize = true };
            _numThang = new NumericUpDown { Location = new Point(170, y - 22), Width = 80, Minimum = 1, Maximum = 12, Value = DateTime.Today.Month };
            Label ln = new Label { Text = "Năm", Location = new Point(260, y - 44), AutoSize = true };
            _numNam = new NumericUpDown { Location = new Point(260, y - 22), Width = 90, Minimum = 2000, Maximum = 2100, Value = DateTime.Today.Year };
            boxCC.Controls.Add(lt); boxCC.Controls.Add(_numThang);
            boxCC.Controls.Add(ln); boxCC.Controls.Add(_numNam);
            Label lpb = new Label { Text = "Phòng ban", Location = new Point(370, y), AutoSize = true };
            _cboCC_PB = new ComboBox { Location = new Point(370, y + 22), Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };
            boxCC.Controls.Add(lpb); boxCC.Controls.Add(_cboCC_PB);
            AddMasterButtons(boxCC, 560, y + 22, ThemCC, XoaCC);
            page.Controls.Add(boxCC);

            _gridCTCC = MakeGrid(new Point(420, 160), new Size(640, 150));
            page.Controls.Add(_gridCTCC);

            GroupBox boxCT = new GroupBox { Text = "Thêm chi tiết chấm công", Location = new Point(10, 320), Size = new Size(1050, 330) };
            int y2 = 24;
            Label lnv2 = new Label { Text = "Nhân viên", Location = new Point(16, y2), AutoSize = true };
            _cboNV_CC = new ComboBox { Location = new Point(16, y2 + 22), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            boxCT.Controls.Add(lnv2); boxCT.Controls.Add(_cboNV_CC);
            Label lca2 = new Label { Text = "Ca làm việc", Location = new Point(210, y2), AutoSize = true };
            _cboCa_CC = new ComboBox { Location = new Point(210, y2 + 22), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            boxCT.Controls.Add(lca2); boxCT.Controls.Add(_cboCa_CC);
            Label lng2 = new Label { Text = "Ngày", Location = new Point(365, y2), AutoSize = true };
            _dtNgayCC = new DateTimePicker { Location = new Point(365, y2 + 22), Width = 150, Format = DateTimePickerFormat.Short };
            boxCT.Controls.Add(lng2); boxCT.Controls.Add(_dtNgayCC);
            Label ltt = new Label { Text = "Trạng thái", Location = new Point(530, y2), AutoSize = true };
            _cboTrangThai = new ComboBox
            {
                Location = new Point(530, y2 + 22),
                Width = 140,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cboTrangThai.Items.AddRange(new object[] { "Đi làm", "Vắng", "Nghỉ phép", "Nghỉ ốm" });
            _cboTrangThai.SelectedIndex = 0;
            boxCT.Controls.Add(ltt); boxCT.Controls.Add(_cboTrangThai);
            AddDetailButtons(boxCT, 700, y2, ThemCTCC, XoaCTCC);
            page.Controls.Add(boxCT);

            return page;
        }

        /* =========================== Phụ trội =========================== */
        private TabPage BuildTabPhuTroi()
        {
            TabPage page = new TabPage("Phiếu phụ trội");

            _gridPT = MakeGrid(new Point(10, 10), new Size(400, 300));
            _gridPT.SelectionChanged += (s, e) => LoadCTPT();
            page.Controls.Add(_gridPT);

            GroupBox boxPT = new GroupBox { Text = "Phiếu ghi nhận giờ phụ trội", Location = new Point(420, 10), Size = new Size(640, 140) };
            int y = 24;
            _txtSoPhieu = AddTxt(boxPT, "Số phiếu", 16, ref y, 140);
            Label l1 = new Label { Text = "Ngày phụ trội", Location = new Point(170, y - 44), AutoSize = true };
            _dtNgayPT = new DateTimePicker { Location = new Point(170, y - 22), Width = 140, Format = DateTimePickerFormat.Short };
            Label l2 = new Label { Text = "Hình thức", Location = new Point(330, y - 44), AutoSize = true };
            _cboHinhThuc = new ComboBox { Location = new Point(330, y - 22), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            _cboHinhThuc.Items.AddRange(new object[] { "Tăng ca", "Ngày lễ", "Ngày nghỉ" });
            _cboHinhThuc.SelectedIndex = 0;
            Label l3 = new Label { Text = "Hệ số", Location = new Point(490, y - 44), AutoSize = true };
            _txtHeSo = new TextBox { Location = new Point(490, y - 22), Width = 80, Text = "1.5" };
            boxPT.Controls.Add(l1); boxPT.Controls.Add(_dtNgayPT);
            boxPT.Controls.Add(l2); boxPT.Controls.Add(_cboHinhThuc);
            boxPT.Controls.Add(l3); boxPT.Controls.Add(_txtHeSo);
            Button bThem = new Button { Text = "Thêm", Location = new Point(16, y + 22), Width = 90 };
            Button bXoa = new Button { Text = "Xóa", Location = new Point(112, y + 22), Width = 90 };
            bThem.Click += (s, e) => ThemPT();
            bXoa.Click += (s, e) => XoaPT();
            boxPT.Controls.Add(bThem); boxPT.Controls.Add(bXoa);
            page.Controls.Add(boxPT);

            _gridCTPT = MakeGrid(new Point(420, 160), new Size(640, 150));
            page.Controls.Add(_gridCTPT);

            GroupBox boxCT = new GroupBox { Text = "Thêm chi tiết phụ trội", Location = new Point(10, 320), Size = new Size(1050, 330) };
            int y2 = 24;
            Label lnv = new Label { Text = "Nhân viên", Location = new Point(16, y2), AutoSize = true };
            _cboNV_PT = new ComboBox { Location = new Point(16, y2 + 22), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            boxCT.Controls.Add(lnv); boxCT.Controls.Add(_cboNV_PT);
            Label lsg = new Label { Text = "Số giờ", Location = new Point(210, y2), AutoSize = true };
            _txtSoGioPT = new TextBox { Location = new Point(210, y2 + 22), Width = 100 };
            boxCT.Controls.Add(lsg); boxCT.Controls.Add(_txtSoGioPT);
            AddDetailButtons(boxCT, 400, y2, ThemCTPT, XoaCTPT);
            page.Controls.Add(boxCT);

            return page;
        }

        /* =========================== Tiện ích bố cục =========================== */
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

        private static void AddMasterButtons(GroupBox box, int x, int y, Action them, Action xoa)
        {
            Button b1 = new Button { Text = "Thêm", Location = new Point(x, y), Width = 70 };
            Button b2 = new Button { Text = "Xóa", Location = new Point(x + 76, y), Width = 70 };
            b1.Click += (s, e) => them();
            b2.Click += (s, e) => xoa();
            box.Controls.Add(b1);
            box.Controls.Add(b2);
        }

        private static void AddDetailButtons(GroupBox box, int x, int y, Action them, Action xoa)
        {
            Button b1 = new Button { Text = "Thêm", Location = new Point(x, y), Width = 80 };
            Button b2 = new Button { Text = "Xóa", Location = new Point(x + 86, y), Width = 80 };
            b1.Click += (s, e) => them();
            b2.Click += (s, e) => xoa();
            box.Controls.Add(b1);
            box.Controls.Add(b2);
        }
    }
}