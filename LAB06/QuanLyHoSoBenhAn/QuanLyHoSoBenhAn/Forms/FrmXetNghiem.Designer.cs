using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmXetNghiem
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView gridHangDoi;
        private Label lblChiDinhDangChon;
        private TextBox txtKetQua, txtDonVi, txtKhoangThamChieu, txtNhanXet;

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
            var split = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, SplitterDistance = 500 };

            gridHangDoi = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            gridHangDoi.CellClick += GridHangDoi_CellClick;

            var panelTrai = new Panel { Dock = DockStyle.Fill };
            var panelTop = new Panel { Dock = DockStyle.Top, Height = 32 };
            var btnLamMoi = new Button { Text = "Làm mới hàng đợi", Location = new Point(10, 3), Size = new Size(150, 25) };
            btnLamMoi.Click += (s, e) => TaiHangDoi();
            panelTop.Controls.Add(btnLamMoi);
            panelTrai.Controls.Add(gridHangDoi);
            panelTrai.Controls.Add(panelTop);
            split.Panel1.Controls.Add(panelTrai);

            var group = new GroupBox { Text = "Nhập kết quả xét nghiệm", Dock = DockStyle.Fill, Padding = new Padding(10) };
            lblChiDinhDangChon = new Label { Text = "Chưa chọn chỉ định.", Location = new Point(10, 25), Size = new Size(280, 50), Font = new Font("Segoe UI", 9, FontStyle.Bold) };

            var lbl1 = new Label { Text = "Kết quả (*):", Location = new Point(10, 85), Size = new Size(90, 23) };
            txtKetQua = new TextBox { Location = new Point(10, 110), Size = new Size(280, 23) };

            var lbl2 = new Label { Text = "Đơn vị:", Location = new Point(10, 143), Size = new Size(90, 23) };
            txtDonVi = new TextBox { Location = new Point(10, 168), Size = new Size(280, 23) };

            var lbl3 = new Label { Text = "Khoảng tham chiếu:", Location = new Point(10, 201), Size = new Size(150, 23) };
            txtKhoangThamChieu = new TextBox { Location = new Point(10, 226), Size = new Size(280, 23) };

            var lbl4 = new Label { Text = "Nhận xét:", Location = new Point(10, 259), Size = new Size(90, 23) };
            txtNhanXet = new TextBox { Location = new Point(10, 284), Size = new Size(280, 60), Multiline = true };

            var btnLuu = new Button { Text = "Xác nhận kết quả", Location = new Point(10, 355), Size = new Size(180, 34) };
            btnLuu.Click += BtnLuu_Click;

            group.Controls.AddRange(new Control[] { lblChiDinhDangChon, lbl1, txtKetQua, lbl2, txtDonVi, lbl3, txtKhoangThamChieu, lbl4, txtNhanXet, btnLuu });

            split.Panel2.Controls.Add(group);
            Controls.Add(split);
        }
    }
}
