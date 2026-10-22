using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;

        private DateTimePicker dtpTu, dtpDen;
        private Label lblTongQuan;
        private DataGridView gridKhoa, gridBacSi, gridDoanhThu;

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
            var panelTop = new Panel { Dock = DockStyle.Top, Height = 44 };
            var lblTu = new Label { Text = "Từ ngày:", Location = new Point(10, 12), AutoSize = true };
            dtpTu = new DateTimePicker { Location = new Point(70, 9), Size = new Size(120, 23), Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddMonths(-1) };
            var lblDen = new Label { Text = "Đến ngày:", Location = new Point(210, 12), AutoSize = true };
            dtpDen = new DateTimePicker { Location = new Point(275, 9), Size = new Size(120, 23), Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            var btnXem = new Button { Text = "Xem báo cáo", Location = new Point(410, 8), Size = new Size(110, 25) };
            btnXem.Click += (s, e) => TaiBaoCao();
            panelTop.Controls.AddRange(new Control[] { lblTu, dtpTu, lblDen, dtpDen, btnXem });

            lblTongQuan = new Label { Dock = DockStyle.Top, Height = 40, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), Padding = new Padding(10, 8, 0, 0) };

            var tabs = new TabControl { Dock = DockStyle.Fill };

            var tabKhoa = new TabPage("Theo khoa");
            gridKhoa = TaoGrid();
            tabKhoa.Controls.Add(gridKhoa);

            var tabBacSi = new TabPage("Theo bác sĩ");
            gridBacSi = TaoGrid();
            tabBacSi.Controls.Add(gridBacSi);

            var tabDoanhThu = new TabPage("Doanh thu theo ngày");
            gridDoanhThu = TaoGrid();
            tabDoanhThu.Controls.Add(gridDoanhThu);

            tabs.TabPages.Add(tabKhoa);
            tabs.TabPages.Add(tabBacSi);
            tabs.TabPages.Add(tabDoanhThu);

            Controls.Add(tabs);
            Controls.Add(lblTongQuan);
            Controls.Add(panelTop);
        }

        private DataGridView TaoGrid() => new DataGridView
        {
            Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
    }
}
