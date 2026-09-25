using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.UI
{
    public partial class FrmKhachHang : Form
    {
        private Label lblMaKhach;
        private TextBox txtMaKhach;
        private Label lblTenKhach;
        private TextBox txtTenKhach;
        private Label lblDienThoai;
        private TextBox txtDienThoai;
        private Button btnThem;
        private DataGridView dgvKhachHang;
        private KhachHangService khachHangService;

        public FrmKhachHang()
        {
            ThietKeGiaoDien();
            khachHangService = new KhachHangService();
        }

        private void ThietKeGiaoDien()
        {
            this.Text = "Quản lý Khách Hàng";
            this.Size = new Size(850, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(this.FrmKhachHang_Load);

            this.lblMaKhach = new Label();
            this.lblMaKhach.Text = "Mã khách:";
            this.lblMaKhach.Location = new Point(20, 20);
            this.lblMaKhach.AutoSize = true;

            this.txtMaKhach = new TextBox();
            this.txtMaKhach.Location = new Point(90, 18);
            this.txtMaKhach.Size = new Size(100, 20);

            this.lblTenKhach = new Label();
            this.lblTenKhach.Text = "Tên khách:";
            this.lblTenKhach.Location = new Point(210, 20);
            this.lblTenKhach.AutoSize = true;

            this.txtTenKhach = new TextBox();
            this.txtTenKhach.Location = new Point(280, 18);
            this.txtTenKhach.Size = new Size(180, 20);

            this.lblDienThoai = new Label();
            this.lblDienThoai.Text = "Điện thoại:";
            this.lblDienThoai.Location = new Point(480, 20);
            this.lblDienThoai.AutoSize = true;

            this.txtDienThoai = new TextBox();
            this.txtDienThoai.Location = new Point(550, 18);
            this.txtDienThoai.Size = new Size(120, 20);

            this.btnThem = new Button();
            this.btnThem.Text = "Thêm";
            this.btnThem.Location = new Point(690, 16);
            this.btnThem.Size = new Size(70, 25);
            this.btnThem.Click += new EventHandler(this.btnThem_Click);

            this.dgvKhachHang = new DataGridView();
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.Location = new Point(20, 60);
            this.dgvKhachHang.Size = new Size(790, 320);
            this.dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvKhachHang.AllowUserToAddRows = false;
            this.dgvKhachHang.CellClick += new DataGridViewCellEventHandler(this.dgvKhachHang_CellClick);

            this.Controls.Add(this.lblMaKhach);
            this.Controls.Add(this.txtMaKhach);
            this.Controls.Add(this.lblTenKhach);
            this.Controls.Add(this.txtTenKhach);
            this.Controls.Add(this.lblDienThoai);
            this.Controls.Add(this.txtDienThoai);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvKhachHang);
        }

        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvKhachHang.DataSource = khachHangService.LayDanhSachKhachHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maKhach = txtMaKhach.Text.Trim();
                string tenKhach = txtTenKhach.Text.Trim();
                string dienThoai = txtDienThoai.Text.Trim();

                if (string.IsNullOrEmpty(maKhach) || string.IsNullOrEmpty(tenKhach))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                khachHangService.ThemKhachHang(maKhach, tenKhach, dienThoai);
                MessageBox.Show("Thêm khách hàng thành công!");

                txtMaKhach.Clear();
                txtTenKhach.Clear();
                txtDienThoai.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKhach.Text = row.Cells[0].Value.ToString();
                txtTenKhach.Text = row.Cells[1].Value.ToString();
                txtDienThoai.Text = row.Cells[2].Value.ToString();
            }
        }
    }
}