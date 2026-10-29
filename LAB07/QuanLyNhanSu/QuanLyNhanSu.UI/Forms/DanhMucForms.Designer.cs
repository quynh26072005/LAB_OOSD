using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.UI
{
    partial class DanhMucForm
    {
        private IContainer components = null;

        private DataGridView _gridPB;
        private TextBox _txtMaPB, _txtTenPB, _txtMoTa;

        private DataGridView _gridCV;
        private TextBox _txtMaCV, _txtTenCV, _txtPCCV;

        private DataGridView _gridCA;
        private TextBox _txtMaCa, _txtTenCa, _txtBD, _txtKT;

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
            this.Text = "Quản lý danh mục";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(820, 560);

            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            tabs.TabPages.Add(BuildPhongBanTab());
            tabs.TabPages.Add(BuildChucVuTab());
            tabs.TabPages.Add(BuildCaTab());
            this.Controls.Add(tabs);
        }

        /* --------------------------- Phòng ban --------------------------- */
        private TabPage BuildPhongBanTab()
        {
            TabPage page = new TabPage("Phòng ban");

            _gridPB = CreateGrid();
            _gridPB.Location = new Point(10, 10);
            _gridPB.Width = 470;
            _gridPB.Height = 300;

            GroupBox box = new GroupBox
            {
                Text = "Thông tin phòng ban",
                Location = new Point(490, 10),
                Size = new Size(310, 430)
            };
            _txtMaPB = AddField(box, "Mã phòng ban", 30);
            _txtTenPB = AddField(box, "Tên phòng ban", 80);
            _txtMoTa = AddField(box, "Mô tả", 130);

            AddCrudButtons(box, 185, ThemPhongBan, SuaPhongBan, XoaPhongBan, LamMoiPhongBan);

            page.Controls.Add(_gridPB);
            page.Controls.Add(box);
            return page;
        }

        /* --------------------------- Chức vụ --------------------------- */
        private TabPage BuildChucVuTab()
        {
            TabPage page = new TabPage("Chức vụ");

            _gridCV = CreateGrid();
            _gridCV.Location = new Point(10, 10);
            _gridCV.Width = 470;
            _gridCV.Height = 300;
            _gridCV.SelectionChanged += (s, e) =>
            {
                if (_gridCV.CurrentRow?.DataBoundItem is ChucVu cv)
                {
                    _txtMaCV.Text = cv.MaCV;
                    _txtTenCV.Text = cv.TenCV;
                    _txtPCCV.Text = cv.PhuCapChucVu.ToString("0.##");
                }
            };

            GroupBox box = new GroupBox
            {
                Text = "Thông tin chức vụ",
                Location = new Point(490, 10),
                Size = new Size(310, 430)
            };
            _txtMaCV = AddField(box, "Mã chức vụ", 30);
            _txtTenCV = AddField(box, "Tên chức vụ", 80);
            _txtPCCV = AddField(box, "Phụ cấp chức vụ", 130);

            AddCrudButtons(box, 185, ThemChucVu, SuaChucVu, XoaChucVu, LamMoiChucVu);

            page.Controls.Add(_gridCV);
            page.Controls.Add(box);
            return page;
        }

        /* --------------------------- Ca làm việc --------------------------- */
        private TabPage BuildCaTab()
        {
            TabPage page = new TabPage("Ca làm việc");

            _gridCA = CreateGrid();
            _gridCA.Location = new Point(10, 10);
            _gridCA.Width = 470;
            _gridCA.Height = 300;
            _gridCA.SelectionChanged += (s, e) =>
            {
                if (_gridCA.CurrentRow?.DataBoundItem is CaLamViec ca)
                {
                    _txtMaCa.Text = ca.MaCa;
                    _txtTenCa.Text = ca.TenCa;
                    _txtBD.Text = ca.GioBatDau.ToString(@"hh\:mm");
                    _txtKT.Text = ca.GioKetThuc.ToString(@"hh\:mm");
                }
            };

            GroupBox box = new GroupBox
            {
                Text = "Thông tin ca làm việc",
                Location = new Point(490, 10),
                Size = new Size(310, 430)
            };
            _txtMaCa = AddField(box, "Mã ca", 30);
            _txtTenCa = AddField(box, "Tên ca", 80);
            _txtBD = AddField(box, "Giờ bắt đầu (HH:mm)", 130);
            _txtKT = AddField(box, "Giờ kết thúc (HH:mm)", 180);

            AddCrudButtons(box, 235, ThemCa, SuaCa, XoaCa, LamMoiCa);

            page.Controls.Add(_gridCA);
            page.Controls.Add(box);
            return page;
        }

        /* --------------------------- Tiện ích bố cục --------------------------- */
        private static DataGridView CreateGrid()
        {
            return new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
        }

        private static TextBox AddField(GroupBox box, string label, int y)
        {
            Label lbl = new Label
            {
                Text = label,
                Location = new Point(16, y + 3),
                AutoSize = true
            };
            TextBox txt = new TextBox
            {
                Location = new Point(16, y + 24),
                Width = box.Width - 48,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            box.Controls.Add(lbl);
            box.Controls.Add(txt);
            return txt;
        }

        private static void AddCrudButtons(GroupBox box, int y,
            Action them, Action sua, Action xoa, Action lamMoi)
        {
            Button b1 = new Button { Text = "Thêm", Location = new Point(16, y), Width = 130 };
            Button b2 = new Button { Text = "Cập nhật", Location = new Point(154, y), Width = 130 };
            Button b3 = new Button { Text = "Xóa", Location = new Point(16, y + 40), Width = 130 };
            Button b4 = new Button { Text = "Làm mới", Location = new Point(154, y + 40), Width = 130 };
            b1.Click += (s, e) => them();
            b2.Click += (s, e) => sua();
            b3.Click += (s, e) => xoa();
            b4.Click += (s, e) => lamMoi();
            box.Controls.Add(b1);
            box.Controls.Add(b2);
            box.Controls.Add(b3);
            box.Controls.Add(b4);
        }
    }
}