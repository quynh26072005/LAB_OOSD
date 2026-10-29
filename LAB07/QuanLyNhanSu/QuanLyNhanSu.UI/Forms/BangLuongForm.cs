using System;
using System.Windows.Forms;
using QuanLyNhanSu.Service;

namespace QuanLyNhanSu.UI
{
    /// <summary>Tính và xem bảng lương hằng tháng.</summary>
    public partial class BangLuongForm : Form
    {
        private readonly LuongService _service = new LuongService();

        public BangLuongForm()
        {
            InitializeComponent();
            if (GridHelper.IsDesignTime) return;
            LoadBL();
        }

        private void LoadBL() => GridHelper.Bind(_gridBL, _service.GetDanhSach());

        private void LoadChiTiet()
        {
            string maBL = GridHelper.SelectedKey(_gridBL, "MaBL")?.ToString();
            if (maBL == null) return;
            var rows = _service.GetChiTiet(maBL);
            GridHelper.Bind(_gridCT, rows);

            decimal tong = 0;
            foreach (var r in rows) tong += r.ThucLanh;
            _lblTong.Text = $"Bảng lương {maBL} - Tổng thực lãnh: {tong:N0} VNĐ";
        }

        private void TinhLuong()
        {
            try
            {
                _service.TinhLuong(_txtMaBL.Text.Trim(), (int)_numThang.Value, (int)_numNam.Value, DateTime.Today);
                LoadBL();
                MessageBox.Show("Đã tính lương xong.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}