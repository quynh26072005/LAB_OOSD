using System;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Forms
{
    public partial class FrmThongKe : Form
    {
        private readonly ThongKeService service = new ThongKeService();
        private DateTimePicker dtTu, dtDen;
        private Label lblMuon, lblQuaHan, lblMat, lblHuHong, lblPhiPhat;
        private DataGridView dgvPhat;

        public FrmThongKe()
        {
            InitializeComponent();
            this.Text = "Thống kê thư viện";
            this.Size = new System.Drawing.Size(950, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            KhoiTaoUI();
            TaiData();
        }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
        }

        private void KhoiTaoUI()
        {
            dtTu = new DateTimePicker { Location = new System.Drawing.Point(100, 20), Width = 130, Format = DateTimePickerFormat.Short, Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1) };
            dtDen = new DateTimePicker { Location = new System.Drawing.Point(340, 20), Width = 130, Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            Button btnThongKe = new Button { Text = "Thống kê", Location = new System.Drawing.Point(490, 18), Width = 110 };

            lblMuon = new Label { Location = new System.Drawing.Point(20, 60), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            lblQuaHan = new Label { Location = new System.Drawing.Point(180, 60), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            lblMat = new Label { Location = new System.Drawing.Point(340, 60), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            lblHuHong = new Label { Location = new System.Drawing.Point(480, 60), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            lblPhiPhat = new Label { Location = new System.Drawing.Point(660, 60), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.Red };

            dgvPhat = new DataGridView { Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(890, 430), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true };

            Controls.AddRange(new Control[] {
                new Label{Text="Từ ngày:", Location=new System.Drawing.Point(20,20), AutoSize=true}, dtTu,
                new Label{Text="Đến ngày:", Location=new System.Drawing.Point(250,20), AutoSize=true}, dtDen,
                btnThongKe, lblMuon, lblQuaHan, lblMat, lblHuHong, lblPhiPhat, dgvPhat
            });

            btnThongKe.Click += (s, e) => TaiData();
        }

        private void TaiData()
        {
            ThongKeTongHop t = service.LayTongHop(dtTu.Value, dtDen.Value);
            lblMuon.Text = "Lượt mượn: " + t.LuotSachMuon;
            lblQuaHan.Text = "Quá hạn: " + t.SachQuaHan;
            lblMat.Text = "Mất: " + t.SachMat;
            lblHuHong.Text = "Hư hỏng: " + t.SachHuHong;
            lblPhiPhat.Text = "Tổng phạt: " + t.TongPhiPhat.ToString("N0") + " đ";
            dgvPhat.DataSource = service.LayChiTietPhat(dtTu.Value, dtDen.Value);
        }
    }
}