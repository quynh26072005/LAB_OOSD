using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    /// <summary>
    /// Service thống kê
    /// </summary>
    public class ThongKeService
    {
        /// <summary>
        /// Lấy tổng hợp thống kê theo khoảng thời gian
        /// </summary>
        public ThongKe LayTongHop(DateTime tuNgay, DateTime denNgay)
        {
            // Đổi 2 mốc nếu người dùng nhập ngược
            if (tuNgay > denNgay)
            {
                DateTime temp = tuNgay;
                tuNgay = denNgay;
                denNgay = temp;
            }

            ThongKe tk = new ThongKe();

            // Số lượt mượn
            string sqlMuon = @"SELECT COUNT(DISTINCT MaPhieuMuon) 
                              FROM PhieuMuon 
                              WHERE NgayMuon BETWEEN @Tu AND @Den";
            tk.SoLuotMuon = Convert.ToInt32(Db.ExecuteScalar(sqlMuon,
                new SqlParameter("@Tu", tuNgay),
                new SqlParameter("@Den", denNgay)
            ));

            // Sách quá hạn (chưa trả và quá ngày hẹn trả)
            string sqlQuaHan = @"SELECT COUNT(*) 
                                FROM ChiTietPhieuMuon ct
                                JOIN PhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                                WHERE ct.NgayTraThucTe IS NULL 
                                  AND pm.NgayHenTra < GETDATE()
                                  AND pm.NgayMuon BETWEEN @Tu AND @Den";
            tk.SachQuaHan = Convert.ToInt32(Db.ExecuteScalar(sqlQuaHan,
                new SqlParameter("@Tu", tuNgay),
                new SqlParameter("@Den", denNgay)
            ));

            // Sách mất
            string sqlMat = @"SELECT COUNT(*) 
                             FROM ChiTietPhieuMuon ct
                             JOIN PhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                             WHERE ct.TinhTrang = N'Mất'
                               AND ct.NgayTraThucTe BETWEEN @Tu AND @Den";
            tk.SachMat = Convert.ToInt32(Db.ExecuteScalar(sqlMat,
                new SqlParameter("@Tu", tuNgay),
                new SqlParameter("@Den", denNgay)
            ));

            // Sách hư hỏng
            string sqlHu = @"SELECT COUNT(*) 
                            FROM ChiTietPhieuMuon ct
                            JOIN PhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                            WHERE ct.TinhTrang = N'Rách-Hư'
                              AND ct.NgayTraThucTe BETWEEN @Tu AND @Den";
            tk.SachHuHong = Convert.ToInt32(Db.ExecuteScalar(sqlHu,
                new SqlParameter("@Tu", tuNgay),
                new SqlParameter("@Den", denNgay)
            ));

            // Tổng phí phạt
            string sqlPhi = @"SELECT ISNULL(SUM(PhiPhat), 0) 
                             FROM PhieuPhat 
                             WHERE NgayPhat BETWEEN @Tu AND @Den";
            tk.TongPhiPhat = Convert.ToDecimal(Db.ExecuteScalar(sqlPhi,
                new SqlParameter("@Tu", tuNgay),
                new SqlParameter("@Den", denNgay)
            ));

            return tk;
        }

        /// <summary>
        /// Lấy chi tiết phiếu phạt theo khoảng thời gian
        /// </summary>
        public DataTable LayChiTietPhat(DateTime tuNgay, DateTime denNgay)
        {
            if (tuNgay > denNgay)
            {
                DateTime temp = tuNgay;
                tuNgay = denNgay;
                denNgay = temp;
            }

            string sql = @"SELECT 
                            pp.MaPhieuPhat,
                            pp.NgayPhat,
                            dg.MaDocGia,
                            dg.Ho + ' ' + dg.Ten AS HoTenDocGia,
                            ds.TenSach,
                            pp.LyDo,
                            pp.PhiPhat,
                            nv.Ho + ' ' + nv.Ten AS NguoiLap
                          FROM PhieuPhat pp
                          JOIN ChiTietPhieuMuon ct ON pp.MaChiTiet = ct.MaChiTiet
                          JOIN PhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                          JOIN DocGia dg ON pm.MaDocGia = dg.MaDocGia
                          JOIN DauSach ds ON ct.MaDauSach = ds.MaDauSach
                          JOIN NhanVien nv ON pp.MaNhanVien = nv.MaNhanVien
                          WHERE pp.NgayPhat BETWEEN @Tu AND @Den
                          ORDER BY pp.NgayPhat DESC";

            return Db.Query(sql,
                new SqlParameter("@Tu", tuNgay),
                new SqlParameter("@Den", denNgay)
            );
        }

        /// <summary>
        /// Thống kê top sách được mượn nhiều nhất
        /// </summary>
        public DataTable LayTopSachMuonNhieu(int top = 10)
        {
            string sql = $@"SELECT TOP {top}
                            ds.MaDauSach,
                            ds.TenSach,
                            COUNT(*) AS SoLanMuon
                          FROM ChiTietPhieuMuon ct
                          JOIN DauSach ds ON ct.MaDauSach = ds.MaDauSach
                          GROUP BY ds.MaDauSach, ds.TenSach
                          ORDER BY COUNT(*) DESC";

            return Db.Query(sql);
        }
    }
}
