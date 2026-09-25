using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.UI
{
    public partial class FrmDanhMuc : Form
    {
     
        private string connectionString = @"Data Source=.;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
        private string currentCategory = "DichVu";

        public FrmDanhMuc()
        {
            InitializeComponent();
            this.Load += FrmDanhMuc_Load;
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            AutoBindCategoryEvents(this);
            SelectCategory("DichVu", lblDichVu);
        }


        private void AutoBindCategoryEvents(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.HasChildren) AutoBindCategoryEvents(c);

                string t = c.Text.ToLower().Trim();
                if (t.Contains("khu vực") || t.Contains("nhân viên") ||
                    t.Contains("tiện nghi") || t.Contains("dịch vụ") ||
                    t.Contains("đền bù"))
                {
                    c.Cursor = Cursors.Hand;
                    c.Click -= CategoryControl_Click;
                    c.Click += CategoryControl_Click;
                }
            }
        }

        private void CategoryControl_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c == null) return;

            string t = c.Text.ToLower();
            if (t.Contains("khu vực")) SelectCategory("KhuVuc", c);
            else if (t.Contains("nhân viên")) SelectCategory("NhanVien", c);
            else if (t.Contains("tiện nghi")) SelectCategory("LoaiTienNghi", c);
            else if (t.Contains("dịch vụ")) SelectCategory("DichVu", c);
            else if (t.Contains("đền bù")) SelectCategory("QuyDinhDenBu", c);
        }

        private void SelectCategory(string category, Control activeControl)
        {
            currentCategory = category;

  
            ResetTabColors();
            if (activeControl != null)
            {
                activeControl.ForeColor = Color.Blue;
                activeControl.Font = new Font(activeControl.Font, FontStyle.Bold);
            }


            txtMa.Clear();
            txtTen.Clear();
            txtExtra.Clear();

            switch (currentCategory)
            {
                case "KhuVuc":
                case "LoaiTienNghi":
                    lblExtra.Text = "Không dùng:";
                    txtExtra.Enabled = false;
                    break;
                case "NhanVien":
                    lblExtra.Text = "Vai trò:";
                    txtExtra.Enabled = true;
                    break;
                case "DichVu":
                    lblExtra.Text = "Đơn vị:";
                    txtExtra.Enabled = true;
                    break;
                case "QuyDinhDenBu":
                    lblExtra.Text = "Mức đền bù:";
                    txtExtra.Enabled = true;
                    break;
            }

            LoadData();
        }

        private void ResetTabColors()
        {
            lblKhuVuc.ForeColor = Color.Gray; lblKhuVuc.Font = new Font(lblKhuVuc.Font, FontStyle.Regular);
            lblNhanVien.ForeColor = Color.Gray; lblNhanVien.Font = new Font(lblNhanVien.Font, FontStyle.Regular);
            lblTienNghi.ForeColor = Color.Gray; lblTienNghi.Font = new Font(lblTienNghi.Font, FontStyle.Regular);
            lblDichVu.ForeColor = Color.Gray; lblDichVu.Font = new Font(lblDichVu.Font, FontStyle.Regular);
            lblDenBu.ForeColor = Color.Gray; lblDenBu.Font = new Font(lblDenBu.Font, FontStyle.Regular);
        }

        public void LoadData()
        {
            string query = "";
            switch (currentCategory)
            {
                case "KhuVuc":
                    query = "SELECT MaKhuVuc AS [Mã], TenKhuVuc AS [Tên], '' AS [Loại / Vai trò], '' AS [Đơn vị], '' AS [Đơn giá / Mức] FROM KhuVuc";
                    break;
                case "NhanVien":
                    query = "SELECT MaNV AS [Mã], TenNV AS [Tên], VaiTro AS [Loại / Vai trò], '' AS [Đơn vị], '' AS [Đơn giá / Mức] FROM NhanVien";
                    break;
                case "LoaiTienNghi":
                    query = "SELECT MaLoaiTN AS [Mã], TenLoaiTN AS [Tên], '' AS [Loại / Vai trò], '' AS [Đơn vị], '' AS [Đơn giá / Mức] FROM LoaiTienNghi";
                    break;
                case "DichVu":
                    query = "SELECT MaDV AS [Mã], TenDV AS [Tên], 'Dịch vụ' AS [Loại / Vai trò], DonVi AS [Đơn vị], DonGia AS [Đơn giá / Mức] FROM DichVu";
                    break;
                case "QuyDinhDenBu":
                    query = "SELECT MaDenBu AS [Mã], TenDenBu AS [Tên], '' AS [Loại / Vai trò], '' AS [Đơn vị], MucDenBu AS [Đơn giá / Mức] FROM QuyDinhDenBu";
                    break;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvData.DataSource = dt;
                }
            }
            catch (Exception) { }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMa.Text.Trim();
            string ten = txtTen.Text.Trim();
            string extra = txtExtra.Text.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "";
            switch (currentCategory)
            {
                case "KhuVuc":
                    query = $"INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc) VALUES ('{ma}', N'{ten}')";
                    break;
                case "NhanVien":
                    query = $"INSERT INTO NhanVien (MaNV, TenNV, VaiTro) VALUES ('{ma}', N'{ten}', N'{extra}')";
                    break;
                case "LoaiTienNghi":
                    query = $"INSERT INTO LoaiTienNghi (MaLoaiTN, TenLoaiTN) VALUES ('{ma}', N'{ten}')";
                    break;
                case "DichVu":
                    query = $"INSERT INTO DichVu (MaDV, TenDV, DonVi, DonGia) VALUES ('{ma}', N'{ten}', N'{extra}', 0)";
                    break;
                case "QuyDinhDenBu":
                    double muc = 0; double.TryParse(extra, out muc);
                    query = $"INSERT INTO QuyDinhDenBu (MaDenBu, TenDenBu, MucDenBu) VALUES ('{ma}', N'{ten}', {muc})";
                    break;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    new SqlCommand(query, conn).ExecuteNonQuery();
                    MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}