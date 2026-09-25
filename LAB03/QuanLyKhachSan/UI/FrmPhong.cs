using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.UI
{
    public partial class FrmPhong : Form
    {
        private string connectionString = @"Data Source=.;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
        private string currentTab = "Phong";

        public FrmPhong()
        {
            InitializeComponent();
        }

        private void FrmPhong_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            SelectTab(lblTabPhong, "Phong");
        }

        // =========================================================
        // XỬ LÝ CHUYỂN TAB VÀ BẬT / TẮT TÍNH NĂNG THEO FILE PDF
        // =========================================================
        private void Tab_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl == null) return;

            if (lbl.Text.Contains("Phòng")) SelectTab(lbl, "Phong");
            else if (lbl.Text.Contains("Tiện nghi")) SelectTab(lbl, "TienNghi");
            else if (lbl.Text.Contains("Lắp đặt")) SelectTab(lbl, "LapDat");
        }

        private void SelectTab(Label activeLabel, string tabName)
        {
            currentTab = tabName;

            // Reset màu chữ các Tab
            lblTabPhong.ForeColor = Color.Gray; lblTabPhong.Font = new Font(lblTabPhong.Font, FontStyle.Regular);
            lblTabTienNghi.ForeColor = Color.Gray; lblTabTienNghi.Font = new Font(lblTabTienNghi.Font, FontStyle.Regular);
            lblTabLapDat.ForeColor = Color.Gray; lblTabLapDat.Font = new Font(lblTabLapDat.Font, FontStyle.Regular);

            // Active Tab (Xanh và In đậm)
            activeLabel.ForeColor = Color.Blue;
            activeLabel.Font = new Font(activeLabel.Font, FontStyle.Bold);

            // BẬT / TẮT CÁC CONTROL DỰA THEO ĐÚNG TỪNG TAB
            if (tabName == "Phong")
            {
                // Bật ô nhập thông tin phòng
                SetControlState(true);
                pnlLapDat.Enabled = false;

                LoadDataPhong();
            }
            else if (tabName == "TienNghi")
            {
                // Tắt hoàn toàn ô nhập thông tin phòng & nút thêm (Giống ảnh chụp)
                SetControlState(false);
                pnlLapDat.Enabled = false;

                LoadDataTienNghi();
            }
            else // LapDat
            {
                // Tắt ô nhập phòng, Bật phần Lập phiếu bên dưới
                SetControlState(false);
                pnlLapDat.Enabled = true;

                LoadDataLapDat();
            }
        }

        private void SetControlState(bool enable)
        {
            txtSoPhong.Enabled = enable;
            cmbKhuVuc.Enabled = enable;
            txtSucChua.Enabled = enable;
            txtDonGia.Enabled = enable;
            btnLuu.Enabled = enable;

            if (!enable)
            {
                txtSoPhong.Clear();
                txtSucChua.Clear();
                txtDonGia.Clear();
            }
        }

        // =========================================================
        // LOAD DỮ LIỆU TỪ DATABASE
        // =========================================================
        private void LoadComboBoxes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Load Khu vực
                    SqlDataAdapter daKhu = new SqlDataAdapter("SELECT MaKhuVuc, TenKhuVuc FROM KhuVuc", conn);
                    DataTable dtKhu = new DataTable();
                    daKhu.Fill(dtKhu);
                    cmbKhuVuc.DataSource = dtKhu;
                    cmbKhuVuc.DisplayMember = "TenKhuVuc";
                    cmbKhuVuc.ValueMember = "MaKhuVuc";

                    // Load Phòng cho Lắp đặt
                    SqlDataAdapter daPhong = new SqlDataAdapter("SELECT MaPhong FROM Phong", conn);
                    DataTable dtPhong = new DataTable();
                    daPhong.Fill(dtPhong);
                    cmbPhongLapDat.DataSource = dtPhong;
                    cmbPhongLapDat.DisplayMember = "MaPhong";
                    cmbPhongLapDat.ValueMember = "MaPhong";

                    // Load Tiện nghi cho Lắp đặt
                    SqlDataAdapter daTN = new SqlDataAdapter("SELECT MaLoaiTN, TenLoaiTN FROM LoaiTienNghi", conn);
                    DataTable dtTN = new DataTable();
                    daTN.Fill(dtTN);
                    cmbTienNghi.DataSource = dtTN;
                    cmbTienNghi.DisplayMember = "TenLoaiTN";
                    cmbTienNghi.ValueMember = "MaLoaiTN";
                }
            }
            catch (Exception) { }
        }

        private void LoadDataPhong()
        {
            string query = @"SELECT p.MaPhong AS [Phòng], k.TenKhuVuc AS [Khu], 
                                    p.SucChua AS [Sức chứa], p.DonGia AS [Đơn giá], p.TrangThai AS [Trạng thái]
                             FROM Phong p JOIN KhuVuc k ON p.MaKhuVuc = k.MaKhuVuc";
            LoadToGrid(query);
        }

        private void LoadDataTienNghi()
        {
            string query = "SELECT MaLoaiTN AS [Mã tiện nghi], TenLoaiTN AS [Tên tiện nghi] FROM LoaiTienNghi";
            LoadToGrid(query);
        }

        private void LoadDataLapDat()
        {
            string query = @"SELECT MaPhieu AS [Phiếu lắp đặt], MaPhong AS [Phòng], 
                                    MaLoaiTN AS [Mã Tiện Nghi], TinhTrang AS [Tình trạng] 
                             FROM PhieuLapDat";
            LoadToGrid(query);
        }

        private void LoadToGrid(string query)
        {
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

        // =========================================================
        // THAO TÁC NGHIỆP VỤ (THÊM PHÒNG / LẬP PHIẾU LẮP ĐẶT)
        // =========================================================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoPhong.Text) || string.IsNullOrEmpty(txtSucChua.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $@"INSERT INTO Phong (MaPhong, MaKhuVuc, SucChua, DonGia, TrangThai) 
                                      VALUES ('{txtSoPhong.Text.Trim()}', '{cmbKhuVuc.SelectedValue}', {txtSucChua.Text.Trim()}, {txtDonGia.Text.Trim()}, N'Trống')";
                    new SqlCommand(query, conn).ExecuteNonQuery();
                    MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataPhong();
                    LoadComboBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPhieu.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã phiếu lắp đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $@"INSERT INTO PhieuLapDat (MaPhieu, MaPhong, MaLoaiTN, TinhTrang) 
                                      VALUES ('{txtMaPhieu.Text.Trim()}', '{cmbPhongLapDat.SelectedValue}', '{cmbTienNghi.SelectedValue}', N'{txtTinhTrang.Text.Trim()}')";
                    new SqlCommand(query, conn).ExecuteNonQuery();
                    MessageBox.Show("Lập phiếu lắp đặt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (currentTab == "LapDat") LoadDataLapDat();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}