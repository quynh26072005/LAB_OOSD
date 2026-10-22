using System;
using System.Data;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>FrmThongKe: báo cáo theo kỳ/khoa/bác sĩ/doanh thu (mục 3.7).</summary>
    public partial class FrmThongKe : System.Windows.Forms.Form
    {
        private readonly ThongKeService _service = new ThongKeService();

        public FrmThongKe(NhanVien nguoiDung)
        {
            Text = "Thống kê báo cáo";
            InitializeComponent();
            TaiBaoCao();
        }

        private void TaiBaoCao()
        {
            DateTime tu = dtpTu.Value.Date, den = dtpDen.Value.Date;
            DataRow tq = _service.ThongKeTongQuan(tu, den);
            lblTongQuan.Text =
                $"Bệnh nhân mới: {tq["BenhNhanMoi"]}   |   Lượt khám: {tq["TongLuotKham"]} (hoàn tất: {tq["LuotHoanTat"]})   |   " +
                $"Lệ phí thu: {Convert.ToDecimal(tq["TongLePhi"]):N0}đ   |   Doanh thu viện phí: {Convert.ToDecimal(tq["TongDoanhThu"]):N0}đ";

            gridKhoa.DataSource = _service.ThongKeLuotKhamTheoKhoa(tu, den);
            gridBacSi.DataSource = _service.ThongKeLuotKhamTheoBacSi(tu, den);
            gridDoanhThu.DataSource = _service.ThongKeDoanhThuTheoNgay(tu, den);
        }
    }
}
