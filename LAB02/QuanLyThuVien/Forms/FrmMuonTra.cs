using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Forms
{
    public partial class FrmMuonTra : Form
    {
        private readonly MuonTraService service = new MuonTraService();
        private readonly SachService sachService = new SachService();
        private readonly DocGiaService docGiaService = new DocGiaService();
        private readonly DanhMucService danhMuc = new DanhMucService();

        private ComboBox cboDocGiaMuon, cboNhanVienMuon, cboDocGiaTra, cboNhanVienTra, cboTinhTrang;
        private DateTimePicker dtNgayMuon, dtHenTra, dtNgayTra;
        private NumericUpDown numPhiPhat;
        private DataGridView dgvSachCon, dgvSachChon, dgvDangMuon;
        private DataTable tableChon;

        public FrmMuonTra()
        {
            InitializeComponent();
            this.Text = "Mượn - Trả sách";
            this.Size = new System.Drawing.Size(1100, 680);
            this.StartPosition = FormStartPosition.CenterParent;
            KhoiTaoUI();
            TaiData();
        }

        private void FrmMuonTra_Load(object sender, EventArgs e)
        {
        }

        private void KhoiTaoUI()
        {
            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            TabPage tabMuon = new TabPage("Mượn sách");
            TabPage tabTra = new TabPage("Trả sách");

            // ================= TAB MƯỢN =================
            cboDocGiaMuon = new ComboBox { Location = new System.Drawing.Point(110, 20), Width = 190, DropDownStyle = ComboBoxStyle.DropDownList };
            cboNhanVienMuon = new ComboBox { Location = new System.Drawing.Point(410, 20), Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
            dtNgayMuon = new DateTimePicker { Location = new System.Drawing.Point(110, 60), Width = 160, Format = DateTimePickerFormat.Short };
            dtHenTra = new DateTimePicker { Location = new System.Drawing.Point(410, 60), Width = 160, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(7) };

            Button btnThem = new Button { Text = "Thêm >>", Location = new System.Drawing.Point(440, 200), Width = 80 };
            Button btnBo = new Button { Text = "<< Bỏ", Location = new System.Drawing.Point(440, 250), Width = 80 };
            Button btnLap = new Button { Text = "Lập phiếu mượn", Location = new System.Drawing.Point(820, 530), Width = 150, Height = 40 };

            dgvSachCon = new DataGridView { Location = new System.Drawing.Point(20, 110), Size = new System.Drawing.Size(400, 400), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgvSachChon = new DataGridView { Location = new System.Drawing.Point(540, 110), Size = new System.Drawing.Size(430, 400), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            tabMuon.Controls.AddRange(new Control[] {
                new Label{Text="Độc giả:", Location=new System.Drawing.Point(20,20), AutoSize=true}, cboDocGiaMuon,
                new Label{Text="NV Lập:", Location=new System.Drawing.Point(320,20), AutoSize=true}, cboNhanVienMuon,
                new Label{Text="Ngày mượn:", Location=new System.Drawing.Point(20,60), AutoSize=true}, dtNgayMuon,
                new Label{Text="Hẹn trả:", Location=new System.Drawing.Point(320,60), AutoSize=true}, dtHenTra,
                btnThem, btnBo, btnLap, dgvSachCon, dgvSachChon
            });

            // ================= TAB TRẢ =================
            cboDocGiaTra = new ComboBox { Location = new System.Drawing.Point(110, 20), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cboNhanVienTra = new ComboBox { Location = new System.Drawing.Point(390, 20), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            dtNgayTra = new DateTimePicker { Location = new System.Drawing.Point(110, 60), Width = 150, Format = DateTimePickerFormat.Short };
            cboTinhTrang = new ComboBox { Location = new System.Drawing.Point(370, 60), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTinhTrang.Items.AddRange(new object[] { "Bình thường", "Rách/Hư hỏng", "Mất" }); cboTinhTrang.SelectedIndex = 0;
            numPhiPhat = new NumericUpDown { Location = new System.Drawing.Point(620, 60), Width = 140, Maximum = 100000000 };

            Button btnXacNhanTra = new Button { Text = "Xác nhận trả", Location = new System.Drawing.Point(780, 50), Width = 130, Height = 35 };
            dgvDangMuon = new DataGridView { Location = new System.Drawing.Point(20, 110), Size = new System.Drawing.Size(950, 420), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            tabTra.Controls.AddRange(new Control[] {
                new Label{Text="Độc giả:", Location=new System.Drawing.Point(20,20), AutoSize=true}, cboDocGiaTra,
                new Label{Text="NV Trả:", Location=new System.Drawing.Point(310,20), AutoSize=true}, cboNhanVienTra,
                new Label{Text="Ngày trả:", Location=new System.Drawing.Point(20,60), AutoSize=true}, dtNgayTra,
                new Label{Text="Tình trạng:", Location=new System.Drawing.Point(280,60), AutoSize=true}, cboTinhTrang,
                new Label{Text="Phí phạt:", Location=new System.Drawing.Point(540,60), AutoSize=true}, numPhiPhat,
                btnXacNhanTra, dgvDangMuon
            });

            tabs.TabPages.AddRange(new TabPage[] { tabMuon, tabTra });
            Controls.Add(tabs);

            // Setup Table chọn tạm
            tableChon = new DataTable();
            tableChon.Columns.Add("MaDauSach", typeof(string));
            tableChon.Columns.Add("TenSach", typeof(string));
            dgvSachChon.DataSource = tableChon;

            // Events
            btnThem.Click += (s, e) => {
                if (dgvSachCon.CurrentRow == null) return;
                if (tableChon.Rows.Count >= 3) { MessageBox.Show("Tối đa 3 cuốn!"); return; }
                DataRowView r = dgvSachCon.CurrentRow.DataBoundItem as DataRowView;
                string ma = Convert.ToString(r["MaDauSach"]);
                foreach (DataRow dr in tableChon.Rows) if (Convert.ToString(dr["MaDauSach"]) == ma) return;
                tableChon.Rows.Add(ma, Convert.ToString(r["TenSach"]));
            };

            btnBo.Click += (s, e) => { if (dgvSachChon.CurrentRow != null) dgvSachChon.Rows.Remove(dgvSachChon.CurrentRow); };

            btnLap.Click += (s, e) => {
                List<string> ds = new List<string>();
                foreach (DataRow r in tableChon.Rows) ds.Add(Convert.ToString(r["MaDauSach"]));
                KetQuaXuLy kq = service.LapPhieuMuon(Convert.ToString(cboDocGiaMuon.SelectedValue), Convert.ToString(cboNhanVienMuon.SelectedValue), ds, dtNgayMuon.Value, dtHenTra.Value);
                MessageBox.Show(kq.ThongBao);
                if (kq.ThanhCong) { tableChon.Rows.Clear(); TaiData(); }
            };

            cboDocGiaTra.SelectedIndexChanged += (s, e) => {
                if (cboDocGiaTra.SelectedValue != null)
                    dgvDangMuon.DataSource = service.LaySachDangMuon(Convert.ToString(cboDocGiaTra.SelectedValue));
            };

            btnXacNhanTra.Click += (s, e) => {
                if (dgvDangMuon.CurrentRow == null) return;
                DataRowView r = dgvDangMuon.CurrentRow.DataBoundItem as DataRowView;
                KetQuaXuLy kq = service.TraSach(Convert.ToString(r["MaChiTiet"]), Convert.ToString(cboNhanVienTra.SelectedValue), dtNgayTra.Value, Convert.ToString(cboTinhTrang.SelectedItem), numPhiPhat.Value);
                MessageBox.Show(kq.ThongBao);
                if (kq.ThanhCong) { dgvDangMuon.DataSource = service.LaySachDangMuon(Convert.ToString(cboDocGiaTra.SelectedValue)); dgvSachCon.DataSource = sachService.LaySachConTrongKho(); }
            };
        }

        private void TaiData()
        {
            DataTable dsDocGia = docGiaService.LayComboDocGia();
            cboDocGiaMuon.DataSource = dsDocGia.Copy(); cboDocGiaMuon.DisplayMember = "HoTen"; cboDocGiaMuon.ValueMember = "MaDocGia";
            cboDocGiaTra.DataSource = dsDocGia.Copy(); cboDocGiaTra.DisplayMember = "HoTen"; cboDocGiaTra.ValueMember = "MaDocGia";

            DataTable dsNV = danhMuc.LayNhanVien();
            cboNhanVienMuon.DataSource = dsNV.Copy(); cboNhanVienMuon.DisplayMember = "MaNhanVien"; cboNhanVienMuon.ValueMember = "MaNhanVien";
            cboNhanVienTra.DataSource = dsNV.Copy(); cboNhanVienTra.DisplayMember = "MaNhanVien"; cboNhanVienTra.ValueMember = "MaNhanVien";

            dgvSachCon.DataSource = sachService.LaySachConTrongKho();
        }
    }
}