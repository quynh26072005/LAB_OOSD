using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.UI
{
    partial class QuyetDinhForm
    {
        private IContainer components = null;

        private DataGridView _grid;
        private ComboBox _cboLoai;
        private TextBox _txtSoQD, _txtNoiDung, _txtNguoiKy;
        private DateTimePicker _dtLap, _dtHieuLuc;
        private Panel _pnlTD, _pnlCV, _pnlL;
        private ComboBox _tdNV, _tdPB, _cvNV, _cvCV, _lNV;
        private TextBox _tdTg, _tdLuong, _tdDK, _cvPc, _lLcb, _lPcl, _lPcdh;

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
            this.Text = "Quản lý quyết định";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(1020, 720);

            BuildLayout();
        }

        private void BuildLayout()
        {
            _grid = new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Location = new Point(10, 10),
                Size = new Size(990, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            GroupBox box = new GroupBox
            {
                Text = "Lập quyết định mới",
                Location = new Point(10, 320),
                Size = new Size(990, 350),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            int y = 22;
            _txtSoQD = AddField(box, "Số QĐ", 16, ref y, 200);
            _dtLap = AddDate(box, "Ngày lập", 230, ref y);
            _dtHieuLuc = AddDate(box, "Ngày hiệu lực", 430, ref y);
            _txtNguoiKy = AddField(box, "Người ký", 630, ref y, 200);

            Label lblNoiDung = new Label { Text = "Nội dung", Location = new Point(16, y), AutoSize = true };
            _txtNoiDung = new TextBox { Location = new Point(16, y + 22), Width = 400, Multiline = true, Height = 46 };
            box.Controls.Add(lblNoiDung);
            box.Controls.Add(_txtNoiDung);
            y += 74;

            Label lblLoai = new Label { Text = "Loại quyết định", Location = new Point(430, y), AutoSize = true };
            _cboLoai = new ComboBox
            {
                Location = new Point(430, y + 22),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cboLoai.Items.AddRange(new object[]
            {
                new ComboItem(LoaiQuyetDinh.TuyenDung, "Tuyển dụng (thử việc)"),
                new ComboItem(LoaiQuyetDinh.ChucVu, "Chức vụ & phụ cấp"),
                new ComboItem(LoaiQuyetDinh.Luong, "Lương")
            });
            _cboLoai.SelectedIndex = 0;
            _cboLoai.SelectedIndexChanged += (s, e) => SwitchType();
            box.Controls.Add(lblLoai);
            box.Controls.Add(_cboLoai);

            _pnlTD = BuildPanelTuyenDung(box);
            _pnlCV = BuildPanelChucVu(box);
            _pnlL = BuildPanelLuong(box);

            Button them = new Button { Text = "Lập quyết định", Location = new Point(780, 60), Width = 140, Height = 40 };
            them.Click += (s, e) => Them();
            Button xoa = new Button { Text = "Xóa", Location = new Point(780, 110), Width = 140 };
            xoa.Click += (s, e) => Xoa();
            Button moi = new Button { Text = "Làm mới", Location = new Point(780, 146), Width = 140 };
            moi.Click += (s, e) => LamMoi();
            box.Controls.Add(them);
            box.Controls.Add(xoa);
            box.Controls.Add(moi);

            Controls.Add(_grid);
            Controls.Add(box);
        }

        private Panel BuildPanelTuyenDung(GroupBox box)
        {
            Panel p = new Panel { Location = new Point(16, 200), Width = 730, Height = 100 };
            int y = 0;
            var lbl = new Label { Text = "NV:", Location = new Point(0, y + 3), AutoSize = true };
            _tdNV = MakeCombo(new Point(0, y + 24), 140);
            var lblPB = new Label { Text = "Phòng ban:", Location = new Point(150, y + 3), AutoSize = true };
            _tdPB = MakeCombo(new Point(150, y + 24), 140);
            var lblTg = new Label { Text = "Thời gian thử việc (tháng):", Location = new Point(300, y + 3), AutoSize = true };
            _tdTg = new TextBox { Location = new Point(300, y + 24), Width = 90 };
            var lblLuong = new Label { Text = "Lương thử việc:", Location = new Point(400, y + 3), AutoSize = true };
            _tdLuong = new TextBox { Location = new Point(400, y + 24), Width = 110 };
            var lblDK = new Label { Text = "Điều khoản:", Location = new Point(0, y + 56), AutoSize = true };
            _tdDK = new TextBox { Location = new Point(0, y + 76), Width = 720 };
            p.Controls.Add(lbl); p.Controls.Add(_tdNV);
            p.Controls.Add(lblPB); p.Controls.Add(_tdPB);
            p.Controls.Add(lblTg); p.Controls.Add(_tdTg);
            p.Controls.Add(lblLuong); p.Controls.Add(_tdLuong);
            p.Controls.Add(lblDK); p.Controls.Add(_tdDK);
            box.Controls.Add(p);
            return p;
        }

        private Panel BuildPanelChucVu(GroupBox box)
        {
            Panel p = new Panel { Location = new Point(16, 200), Width = 730, Height = 60 };
            int y = 0;
            var lblNV = new Label { Text = "NV:", Location = new Point(0, y + 3), AutoSize = true };
            _cvNV = MakeCombo(new Point(0, y + 24), 180);
            var lblCV = new Label { Text = "Chức vụ:", Location = new Point(190, y + 3), AutoSize = true };
            _cvCV = MakeCombo(new Point(190, y + 24), 180);
            var lblPc = new Label { Text = "Phụ cấp chức vụ:", Location = new Point(380, y + 3), AutoSize = true };
            _cvPc = new TextBox { Location = new Point(380, y + 24), Width = 140 };
            p.Controls.Add(lblNV); p.Controls.Add(_cvNV);
            p.Controls.Add(lblCV); p.Controls.Add(_cvCV);
            p.Controls.Add(lblPc); p.Controls.Add(_cvPc);
            box.Controls.Add(p);
            return p;
        }

        private Panel BuildPanelLuong(GroupBox box)
        {
            Panel p = new Panel { Location = new Point(16, 200), Width = 730, Height = 60 };
            int y = 0;
            var lblNV = new Label { Text = "NV:", Location = new Point(0, y + 3), AutoSize = true };
            _lNV = MakeCombo(new Point(0, y + 24), 180);
            var lblLcb = new Label { Text = "Lương cơ bản:", Location = new Point(190, y + 3), AutoSize = true };
            _lLcb = new TextBox { Location = new Point(190, y + 24), Width = 120 };
            var lblPcl = new Label { Text = "Phụ cấp lương:", Location = new Point(320, y + 3), AutoSize = true };
            _lPcl = new TextBox { Location = new Point(320, y + 24), Width = 120 };
            var lblPcdh = new Label { Text = "Phụ cấp độc hại:", Location = new Point(450, y + 3), AutoSize = true };
            _lPcdh = new TextBox { Location = new Point(450, y + 24), Width = 120 };
            p.Controls.Add(lblNV); p.Controls.Add(_lNV);
            p.Controls.Add(lblLcb); p.Controls.Add(_lLcb);
            p.Controls.Add(lblPcl); p.Controls.Add(_lPcl);
            p.Controls.Add(lblPcdh); p.Controls.Add(_lPcdh);
            box.Controls.Add(p);
            return p;
        }

        private static ComboBox MakeCombo(Point p, int w) =>
            new ComboBox { Location = p, Width = w, DropDownStyle = ComboBoxStyle.DropDownList };

        private static TextBox AddField(GroupBox box, string label, int x, ref int y, int width)
        {
            Label lbl = new Label { Text = label, Location = new Point(x, y), AutoSize = true };
            TextBox txt = new TextBox { Location = new Point(x, y + 22), Width = width };
            box.Controls.Add(lbl);
            box.Controls.Add(txt);
            return txt;
        }

        private static DateTimePicker AddDate(GroupBox box, string label, int x, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(x, y), AutoSize = true };
            DateTimePicker dt = new DateTimePicker
            {
                Location = new Point(x, y + 22),
                Width = 160,
                Format = DateTimePickerFormat.Short
            };
            box.Controls.Add(lbl);
            box.Controls.Add(dt);
            return dt;
        }
    }
}