using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.UI
{
    partial class NhanVienForm
    {
        private IContainer components = null;

        private DataGridView _grid;
        private TextBox _txtMaNV, _txtHoTen, _txtDiaChi, _txtSDT, _txtCCCD, _txtTim;
        private DateTimePicker _dtNgaySinh;
        private ComboBox _cboGioiTinh, _cboPB, _cboCV;

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
            this.Text = "Quản lý nhân viên";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(1000, 620);

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
                Size = new Size(680, 560),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            _grid.SelectionChanged += (s, e) =>
            {
                if (_grid.CurrentRow?.DataBoundItem is NhanVien nv)
                {
                    _txtMaNV.Text = nv.MaNV;
                    _txtHoTen.Text = nv.HoTen;
                    _dtNgaySinh.Value = nv.NgaySinh ?? DateTime.Today;
                    _cboGioiTinh.SelectedItem = nv.GioiTinh ?? "Nam";
                    _txtDiaChi.Text = nv.DiaChi;
                    _txtSDT.Text = nv.SDT;
                    _txtCCCD.Text = nv.CCCD;
                    _cboPB.SelectedValue = nv.MaPB;
                    _cboCV.SelectedValue = nv.MaCV;
                }
            };

            GroupBox edit = new GroupBox
            {
                Text = "Thông tin nhân viên",
                Location = new Point(700, 10),
                Size = new Size(280, 560),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right
            };

            int y = 20;
            _txtMaNV = AddField(edit, "Mã NV", ref y);
            _txtHoTen = AddField(edit, "Họ và tên", ref y);
            _dtNgaySinh = AddDate(edit, "Ngày sinh", ref y);

            Label lblGT = new Label { Text = "Giới tính", Location = new Point(16, y), AutoSize = true };
            _cboGioiTinh = new ComboBox { Location = new Point(16, y + 22), Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
            _cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            edit.Controls.Add(lblGT);
            edit.Controls.Add(_cboGioiTinh);
            y += 68;

            _txtDiaChi = AddField(edit, "Địa chỉ", ref y);
            _txtSDT = AddField(edit, "SĐT", ref y);
            _txtCCCD = AddField(edit, "CCCD", ref y);

            Label lblPB = new Label { Text = "Phòng ban", Location = new Point(16, y), AutoSize = true };
            _cboPB = new ComboBox { Location = new Point(16, y + 22), Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
            edit.Controls.Add(lblPB);
            edit.Controls.Add(_cboPB);
            y += 46;

            Label lblCV = new Label { Text = "Chức vụ", Location = new Point(16, y), AutoSize = true };
            _cboCV = new ComboBox { Location = new Point(16, y + 22), Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
            edit.Controls.Add(lblCV);
            edit.Controls.Add(_cboCV);
            y += 50;

            Button them = new Button { Text = "Thêm", Location = new Point(16, y), Width = 120 };
            Button sua = new Button { Text = "Cập nhật", Location = new Point(144, y), Width = 120 };
            Button xoa = new Button { Text = "Xóa", Location = new Point(16, y + 38), Width = 120 };
            Button moi = new Button { Text = "Làm mới", Location = new Point(144, y + 38), Width = 120 };
            them.Click += (s, e) => Them();
            sua.Click += (s, e) => Sua();
            xoa.Click += (s, e) => Xoa();
            moi.Click += (s, e) => LamMoi();
            edit.Controls.Add(them);
            edit.Controls.Add(sua);
            edit.Controls.Add(xoa);
            edit.Controls.Add(moi);

            Label lblTim = new Label { Text = "Tìm kiếm", Location = new Point(10, 578), AutoSize = true };
            _txtTim = new TextBox { Location = new Point(80, 575), Width = 240 };
            Button tim = new Button { Text = "Tìm", Location = new Point(330, 573), Width = 80 };
            tim.Click += (s, e) => LoadData();
            _txtTim.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoadData(); };

            Controls.Add(_grid);
            Controls.Add(edit);
            Controls.Add(lblTim);
            Controls.Add(_txtTim);
            Controls.Add(tim);
        }

        private static TextBox AddField(GroupBox box, string label, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(16, y), AutoSize = true };
            TextBox txt = new TextBox { Location = new Point(16, y + 22), Width = 240 };
            box.Controls.Add(lbl);
            box.Controls.Add(txt);
            y += 68;
            return txt;
        }

        private static DateTimePicker AddDate(GroupBox box, string label, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(16, y), AutoSize = true };
            DateTimePicker dt = new DateTimePicker
            {
                Location = new Point(16, y + 22),
                Width = 240,
                Format = DateTimePickerFormat.Short
            };
            box.Controls.Add(lbl);
            box.Controls.Add(dt);
            y += 68;
            return dt;
        }
    }
}