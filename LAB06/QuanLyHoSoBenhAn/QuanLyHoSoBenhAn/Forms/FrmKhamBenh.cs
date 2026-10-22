using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>
    /// FrmKhamBenh: lịch sử, triệu chứng, chẩn đoán, chỉ định, toa (mục 3.7) - lõi của UC01
    /// "Khám bệnh đến nhận toa" (mục 2.9).
    /// </summary>
    public partial class FrmKhamBenh : Form
    {
        private readonly NhanVien _nguoiDung;
        private readonly KhamBenhService _khamService = new KhamBenhService();
        private readonly BenhNhanService _bnService = new BenhNhanService();
        private readonly DanhMucService _dmService = new DanhMucService();

        private int? _maPhieuDangChon;

        public FrmKhamBenh(NhanVien nguoiDung)
        {
            _nguoiDung = nguoiDung;
            Text = "Khám bệnh";
            InitializeComponent();
            TaiDanhMuc();
            TaiHangDoi();
        }

        private class DongThuoc
        {
            public int MaThuoc { get; set; }
            public string TenThuoc { get; set; }
            public int SoLuong { get; set; }
            public string LieuDung { get; set; }
            public string TanSuat { get; set; }
            public int SoNgay { get; set; }
            public string CachDung { get; set; }
        }

        private void TaiDanhMuc()
        {
            cboXetNghiem.DataSource = _dmService.LayDanhSachXetNghiem();
            cboThuoc.DataSource = _dmService.LayDanhSachThuoc();
        }

        private void TaiHangDoi()
        {
            gridHangDoi.DataSource = _khamService.LayHangDoiTiepNhan();
        }

        private void GridHangDoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            _maPhieuDangChon = Convert.ToInt32(gridHangDoi.Rows[e.RowIndex].Cells["MaPhieuKham"].Value);
            NapPhieuKham();
        }

        private void NapPhieuKham()
        {
            if (_maPhieuDangChon == null) return;
            DataRow r = _khamService.LayPhieuKhamChiTiet(_maPhieuDangChon.Value);
            if (r == null) return;

            lblPhieuDangChon.Text =
                $"Phiếu #{_maPhieuDangChon} - BN: {r["TenBenhNhan"]} ({(r["GioiTinh"])}, sinh {Convert.ToDateTime(r["NgaySinh"]):dd/MM/yyyy}) " +
                $"- Khoa: {r["TenKhoa"]} - Bác sĩ: {r["TenBacSi"]} - Trạng thái: {r["TrangThai"]}";

            txtTrieuChung.Text = r["TrieuChung"] as string;
            txtSinhHieu.Text = r["SinhHieu"] as string;
            txtChanDoan.Text = r["ChanDoan"] as string;

            gridChiDinh.DataSource = _khamService.LayChiDinhTheoPhieu(_maPhieuDangChon.Value);
            gridToaDaLuu.DataSource = _khamService.LayToaTheoPhieu(_maPhieuDangChon.Value);
            _dsThuocMoi.Clear();
        }

        private void BtnLuuKham_Click(object sender, EventArgs e)
        {
            if (!KiemTraDaChonPhieu()) return;
            _khamService.GhiNhanKham(_maPhieuDangChon.Value, txtTrieuChung.Text, txtSinhHieu.Text, txtChanDoan.Text, _nguoiDung.MaNhanVien);
            MessageBox.Show("Đã lưu thông tin khám.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TaiHangDoi();
        }

        private void BtnChiDinh_Click(object sender, EventArgs e)
        {
            if (!KiemTraDaChonPhieu()) return;
            if (cboXetNghiem.SelectedValue == null) return;

            _khamService.TaoChiDinhXetNghiem(
                _maPhieuDangChon.Value, Convert.ToInt32(cboXetNghiem.SelectedValue),
                _nguoiDung.MaNhanVien, txtGhiChuChiDinh.Text, _nguoiDung.MaNhanVien);

            MessageBox.Show("Đã tạo chỉ định xét nghiệm.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gridChiDinh.DataSource = _khamService.LayChiDinhTheoPhieu(_maPhieuDangChon.Value);
            TaiHangDoi();
        }

        private void BtnLuuToa_Click(object sender, EventArgs e)
        {
            if (!KiemTraDaChonPhieu()) return;
            if (_dsThuocMoi.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một dòng thuốc.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var ds = _dsThuocMoi.Select(d => new Models.ChiTietToaThuoc
                {
                    MaThuoc = d.MaThuoc,
                    SoLuong = d.SoLuong,
                    LieuDung = d.LieuDung,
                    TanSuat = d.TanSuat,
                    SoNgay = d.SoNgay,
                    CachDung = d.CachDung
                }).ToList();

                _khamService.TaoToaVaChiTiet(_maPhieuDangChon.Value, _nguoiDung.MaNhanVien, txtLoiDan.Text, ds, _nguoiDung.MaNhanVien);
                MessageBox.Show("Lưu toa thuốc thành công (nguyên tử - tất cả dòng được lưu cùng lúc).", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _dsThuocMoi.Clear();
                gridToaDaLuu.DataSource = _khamService.LayToaTheoPhieu(_maPhieuDangChon.Value);
            }
            catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                MessageBox.Show("Phiếu khám này đã có toa thuốc chính (chỉ cho phép 1 toa).", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // TC13: một dòng không hợp lệ hoặc lỗi lưu -> toàn bộ toa rollback, không có dòng nào được lưu.
                MessageBox.Show("Không thể lưu toa (toàn bộ đã được hoàn tác): " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHoanTat_Click(object sender, EventArgs e)
        {
            if (!KiemTraDaChonPhieu()) return;
            _khamService.HoanTatChuyenMon(_maPhieuDangChon.Value, _nguoiDung.MaNhanVien);
            MessageBox.Show("Đã chuyển phiếu sang Chờ viện phí.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TaiHangDoi();
            NapPhieuKham();
        }

        private bool KiemTraDaChonPhieu()
        {
            if (_maPhieuDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu khám từ hàng đợi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
