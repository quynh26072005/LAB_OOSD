using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Models;

namespace QuanLyHoSoBenhAn.Forms
{
    /// <summary>
    /// FrmMain: điều hướng theo vai trò (mục 3.7 kiến trúc WinForms). Mỗi vai trò chỉ thấy
    /// menu chức năng thuộc phạm vi của mình (Actor-UseCase, mục 2.1/table Actor-Nhiệm vụ).
    /// </summary>
    public partial class FrmMain : Form
    {
        private readonly NhanVien _nhanVien;

        public FrmMain(NhanVien nhanVien)
        {
            _nhanVien = nhanVien;
            IsMdiContainer = true;
            Text = $"Quản lý Hồ sơ Bệnh án - {_nhanVien.HoTen} ({TenVaiTro(_nhanVien.VaiTro)})";
            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        /// <summary>Danh sách (tên menu, hành động) theo vai trò - đúng bảng Actor/Nhiệm vụ mục 2.1.</summary>
        private List<Tuple<string, Action>> LayMenuTheoVaiTro()
        {
            var ds = new List<Tuple<string, Action>>();
            string vt = _nhanVien.VaiTro;

            if (vt == "LeTan" || vt == "QuanTri")
            {
                ds.Add(Tuple.Create<string, Action>("Bệnh nhân / Đăng ký", () => MoForm(new FrmBenhNhan(_nhanVien))));
                ds.Add(Tuple.Create<string, Action>("Lệ phí & Hồ sơ", () => MoForm(new FrmLePhiHoSo(_nhanVien))));
                ds.Add(Tuple.Create<string, Action>("Lịch khám định kỳ", () => MoForm(new FrmLichKhamDinhKy(_nhanVien))));
                ds.Add(Tuple.Create<string, Action>("Tiếp nhận khám", () => MoForm(new FrmTiepNhanKham(_nhanVien))));
            }

            if (vt == "BacSi" || vt == "QuanTri")
            {
                ds.Add(Tuple.Create<string, Action>("Khám bệnh", () => MoForm(new FrmKhamBenh(_nhanVien))));
            }

            if (vt == "XetNghiem" || vt == "QuanTri")
            {
                ds.Add(Tuple.Create<string, Action>("Xét nghiệm", () => MoForm(new FrmXetNghiem(_nhanVien))));
            }

            if (vt == "VienPhi" || vt == "QuanTri")
            {
                ds.Add(Tuple.Create<string, Action>("Viện phí", () => MoForm(new FrmVienPhi(_nhanVien))));
            }

            if (vt == "DuocSi" || vt == "QuanTri")
            {
                ds.Add(Tuple.Create<string, Action>("Danh mục thuốc", () => MoForm(new FrmDanhMuc(_nhanVien, 1))));
            }

            if (vt == "QuanLy" || vt == "QuanTri")
            {
                ds.Add(Tuple.Create<string, Action>("Thống kê báo cáo", () => MoForm(new FrmThongKe(_nhanVien))));
            }

            if (vt == "QuanTri")
            {
                ds.Add(Tuple.Create<string, Action>("Danh mục hệ thống", () => MoForm(new FrmDanhMuc(_nhanVien, 0))));
                ds.Add(Tuple.Create<string, Action>("Tài khoản & Nhật ký", () => MoForm(new FrmNguoiDung(_nhanVien))));
            }

            return ds;
        }

        private void MoForm(Form f)
        {
            foreach (Form child in MdiChildren)
            {
                if (child.GetType() == f.GetType())
                {
                    child.Activate();
                    f.Dispose();
                    return;
                }
            }

            f.MdiParent = this;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        public static string TenVaiTro(string vaiTro)
        {
            switch (vaiTro)
            {
                case "LeTan": return "Lễ tân / CSKH";
                case "BacSi": return "Bác sĩ";
                case "XetNghiem": return "Nhân viên xét nghiệm";
                case "DuocSi": return "Dược sĩ";
                case "VienPhi": return "Nhân viên viện phí";
                case "QuanLy": return "Quản lý";
                case "QuanTri": return "Quản trị hệ thống";
                default: return vaiTro;
            }
        }
    }
}
