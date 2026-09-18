using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Services
{
    /// <summary>
    /// Service xử lý nghiệp vụ mượn trả sách
    /// </summary>
    public class MuonTraService
    {
        private readonly DocGiaService docGiaService = new DocGiaService();

        #region Kiểm tra điều kiện mượn

        /// <summary>
        /// Kiểm tra tất cả điều kiện trước khi cho phép mượn sách
        /// </summary>
        public KetQuaXuLy KiemTraDieuKienMuon(string maDocGia, int soSachMuonMoi)
        {
            if (string.IsNullOrWhiteSpace(maDocGia))
                return KetQuaXuLy.Loi("Vui lòng chọn độc giả.");

            if (soSachMuonMoi < 1)
                return KetQuaXuLy.Loi("Phải chọn ít nhất 1 cuốn sách.");

            // BR01 & BR03: Kiểm tra thẻ hợp lệ
            TheDocGia the = docGiaService.LayTheHopLe(maDocGia);
            if (the == null)
                return KetQuaXuLy.Loi("Độc giả chưa có thẻ thư viện!");

            if (!the.ConHieuLuc)
                return KetQuaXuLy.Loi("Thẻ không hợp lệ! (Hết hạn hoặc chưa đóng lệ phí)");

            // BR05: Kiểm tra sách quá hạn chưa trả
            if (CoSachQuaHanChuaTra(maDocGia))
                return KetQuaXuLy.Loi("Độc giả còn sách quá hạn chưa trả! Không thể mượn thêm.");

            // BR04: Kiểm tra số lượng tối đa 3 cuốn
            int soSachDangMuon = DemSachDangMuon(maDocGia);
            if (soSachDangMuon + soSachMuonMoi > 3)
                return KetQuaXuLy.Loi($"Độc giả đang mượn {soSachDangMuon} cuốn. Chỉ được mượn tối đa 3 cuốn!");

            return KetQuaXuLy.OK("Đủ điều kiện mượn sách.");
        }

        private bool CoSachQuaHanChuaTra(string maDocGia)
        {
            string sql = @"SELECT COUNT(*) 
                          FROM ChiTietPhieuMuon ct
                          JOIN PhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                          WHERE pm.MaDocGia = @Ma 
                            AND ct.NgayTraThucTe IS NULL 
                            AND pm.NgayHenTra < @HomNay";

            int count = Convert.ToInt32(Db.ExecuteScalar(sql,
                new SqlParameter("@Ma", maDocGia),
                new SqlParameter("@HomNay", DateTime.Today)
            ));

            return count > 0;
        }

        private int DemSachDangMuon(string maDocGia)
        {
            string sql = @"SELECT COUNT(*) 
                          FROM ChiTietPhieuMuon ct
                          JOIN PhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                          WHERE pm.MaDocGia = @Ma AND ct.NgayTraThucTe IS NULL";

            return Convert.ToInt32(Db.ExecuteScalar(sql, new SqlParameter("@Ma", maDocGia)));
        }

        #endregion

        #region Mượn sách

        /// <summary>
        /// Lập phiếu mượn sách
        /// </summary>
        public KetQuaXuLy LapPhieuMuon(string maDocGia, string maNhanVien, DateTime ngayMuon, 
                                       DateTime ngayHenTra, DataTable danhSachSachMuon)
        {
            // Validate ngày
            if (ngayHenTra < ngayMuon)
                return KetQuaXuLy.Loi("Ngày hẹn trả phải sau ngày mượn!");

            // Kiểm tra danh sách sách
            if (danhSachSachMuon == null || danhSachSachMuon.Rows.Count == 0)
                return KetQuaXuLy.Loi("Chưa chọn sách để mượn!");

            if (danhSachSachMuon.Rows.Count > 3)
                return KetQuaXuLy.Loi("Chỉ được mượn tối đa 3 cuốn!");

            // Kiểm tra điều kiện
            KetQuaXuLy kiemTra = KiemTraDieuKienMuon(maDocGia, danhSachSachMuon.Rows.Count);
            if (!kiemTra.ThanhCong)
                return kiemTra;

            // BR06: Kiểm tra trùng đầu sách
            DataView dv = danhSachSachMuon.DefaultView;
            DataTable distinct = dv.ToTable(true, "MaDauSach");
            if (distinct.Rows.Count != danhSachSachMuon.Rows.Count)
                return KetQuaXuLy.Loi("Không được mượn 2 sách cùng đầu sách trong 1 phiếu!");

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction trans = cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    // Tạo mã phiếu mượn
                    string maPhieuMuon = "PM" + DateTime.Now.ToString("yyMMddHHmmss");

                    // Thêm phiếu mượn
                    string sqlPhieu = @"INSERT INTO PhieuMuon (MaPhieuMuon, MaDocGia, MaNhanVien, NgayMuon, NgayHenTra)
                                       VALUES (@MaPM, @MaDG, @MaNV, @NgayMuon, @NgayHenTra)";

                    SqlCommand cmdPhieu = new SqlCommand(sqlPhieu, cn, trans);
                    cmdPhieu.Parameters.AddRange(new[] {
                        new SqlParameter("@MaPM", maPhieuMuon),
                        new SqlParameter("@MaDG", maDocGia),
                        new SqlParameter("@MaNV", maNhanVien),
                        new SqlParameter("@NgayMuon", ngayMuon),
                        new SqlParameter("@NgayHenTra", ngayHenTra)
                    });
                    cmdPhieu.ExecuteNonQuery();

                    // Thêm chi tiết và giảm tồn kho
                    foreach (DataRow row in danhSachSachMuon.Rows)
                    {
                        string maDauSach = row["MaDauSach"].ToString();

                        // BR07: Kiểm tra tồn kho
                        string checkKho = "SELECT SoLuongHienCo FROM DauSach WHERE MaDauSach = @Ma";
                        SqlCommand cmdCheck = new SqlCommand(checkKho, cn, trans);
                        cmdCheck.Parameters.AddWithValue("@Ma", maDauSach);
                        int soLuong = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (soLuong <= 0)
                        {
                            trans.Rollback();
                            return KetQuaXuLy.Loi($"Sách '{row["TenSach"]}' đã hết! Hủy giao dịch.");
                        }

                        // Thêm chi tiết
                        string maChiTiet = "CT" + DateTime.Now.ToString("yyMMddHHmmss") + row["MaDauSach"].ToString().Substring(0, 2);
                        string sqlChiTiet = @"INSERT INTO ChiTietPhieuMuon (MaChiTiet, MaPhieuMuon, MaDauSach)
                                             VALUES (@MaCT, @MaPM, @MaDS)";

                        SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, cn, trans);
                        cmdChiTiet.Parameters.AddRange(new[] {
                            new SqlParameter("@MaCT", maChiTiet),
                            new SqlParameter("@MaPM", maPhieuMuon),
                            new SqlParameter("@MaDS", maDauSach)
                        });
                        cmdChiTiet.ExecuteNonQuery();

                        // Giảm tồn kho
                        string sqlGiam = "UPDATE DauSach SET SoLuongHienCo = SoLuongHienCo - 1 WHERE MaDauSach = @Ma";
                        SqlCommand cmdGiam = new SqlCommand(sqlGiam, cn, trans);
                        cmdGiam.Parameters.AddWithValue("@Ma", maDauSach);
                        cmdGiam.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return KetQuaXuLy.OK($"Lập phiếu mượn thành công! Mã phiếu: {maPhieuMuon}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
                }
            }
        }

        #endregion

        #region Trả sách

        /// <summary>
        /// Lấy danh sách sách đang mượn của độc giả
        /// </summary>
        public DataTable LaySachDangMuon(string maDocGia)
        {
            string sql = @"SELECT ct.MaChiTiet, ct.MaPhieuMuon, ct.MaDauSach, ds.TenSach,
                                  pm.NgayMuon, pm.NgayHenTra, ct.NgayTraThucTe,
                                  CASE 
                                    WHEN GETDATE() > pm.NgayHenTra THEN N'Quá hạn'
                                    ELSE N'Trong hạn'
                                  END AS TrangThai
                          FROM ChiTietPhieuMuon ct
                          JOIN PhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                          JOIN DauSach ds ON ct.MaDauSach = ds.MaDauSach
                          WHERE pm.MaDocGia = @Ma AND ct.NgayTraThucTe IS NULL
                          ORDER BY pm.NgayHenTra";

            return Db.Query(sql, new SqlParameter("@Ma", maDocGia));
        }

        /// <summary>
        /// Trả sách và lập phiếu phạt nếu cần
        /// </summary>
        public KetQuaXuLy TraSach(string maChiTiet, DateTime ngayTraThucTe, string tinhTrang, 
                                  string maNhanVien, decimal phiPhat)
        {
            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction trans = cn.BeginTransaction())
            {
                try
                {
                    // Lấy thông tin chi tiết
                    string sqlInfo = @"SELECT ct.*, pm.NgayHenTra, ds.MaDauSach, ds.TenSach
                                      FROM ChiTietPhieuMuon ct
                                      JOIN PhieuMuon pm ON ct.MaPhieuMuon = pm.MaPhieuMuon
                                      JOIN DauSach ds ON ct.MaDauSach = ds.MaDauSach
                                      WHERE ct.MaChiTiet = @Ma";

                    SqlCommand cmdInfo = new SqlCommand(sqlInfo, cn, trans);
                    cmdInfo.Parameters.AddWithValue("@Ma", maChiTiet);
                    SqlDataReader reader = cmdInfo.ExecuteReader();

                    if (!reader.Read())
                    {
                        reader.Close();
                        trans.Rollback();
                        return KetQuaXuLy.Loi("Không tìm thấy thông tin mượn sách!");
                    }

                    DateTime ngayHenTra = reader.GetDateTime(reader.GetOrdinal("NgayHenTra"));
                    string maDauSach = reader.GetString(reader.GetOrdinal("MaDauSach"));
                    string tenSach = reader.GetString(reader.GetOrdinal("TenSach"));
                    reader.Close();

                    // Cập nhật trả sách
                    string sqlTra = @"UPDATE ChiTietPhieuMuon 
                                     SET NgayTraThucTe = @NgayTra, TinhTrang = @TinhTrang
                                     WHERE MaChiTiet = @Ma";

                    SqlCommand cmdTra = new SqlCommand(sqlTra, cn, trans);
                    cmdTra.Parameters.AddRange(new[] {
                        new SqlParameter("@Ma", maChiTiet),
                        new SqlParameter("@NgayTra", ngayTraThucTe),
                        new SqlParameter("@TinhTrang", tinhTrang)
                    });
                    cmdTra.ExecuteNonQuery();

                    // BR09, BR10: Lập phiếu phạt nếu cần
                    bool canPhat = false;
                    string lyDo = "";

                    if (ngayTraThucTe > ngayHenTra)
                    {
                        canPhat = true;
                        int soNgayTre = (ngayTraThucTe - ngayHenTra).Days;
                        lyDo = $"Trả trễ {soNgayTre} ngày";
                    }

                    if (tinhTrang == "Rách-Hư")
                    {
                        canPhat = true;
                        lyDo += (string.IsNullOrEmpty(lyDo) ? "" : "; ") + "Sách rách/hư hỏng";
                    }

                    if (tinhTrang == "Mất")
                    {
                        canPhat = true;
                        lyDo += (string.IsNullOrEmpty(lyDo) ? "" : "; ") + "Mất sách";
                    }

                    if (canPhat && phiPhat > 0)
                    {
                        string maPhat = "PP" + DateTime.Now.ToString("yyMMddHHmmss");
                        string sqlPhat = @"INSERT INTO PhieuPhat (MaPhieuPhat, MaChiTiet, NgayPhat, LyDo, PhiPhat, MaNhanVien)
                                          VALUES (@MaPP, @MaCT, @NgayPhat, @LyDo, @Phi, @MaNV)";

                        SqlCommand cmdPhat = new SqlCommand(sqlPhat, cn, trans);
                        cmdPhat.Parameters.AddRange(new[] {
                            new SqlParameter("@MaPP", maPhat),
                            new SqlParameter("@MaCT", maChiTiet),
                            new SqlParameter("@NgayPhat", DateTime.Today),
                            new SqlParameter("@LyDo", lyDo),
                            new SqlParameter("@Phi", phiPhat),
                            new SqlParameter("@MaNV", maNhanVien)
                        });
                        cmdPhat.ExecuteNonQuery();
                    }

                    // Tăng tồn kho nếu sách không mất
                    if (tinhTrang != "Mất")
                    {
                        string sqlTang = "UPDATE DauSach SET SoLuongHienCo = SoLuongHienCo + 1 WHERE MaDauSach = @Ma";
                        SqlCommand cmdTang = new SqlCommand(sqlTang, cn, trans);
                        cmdTang.Parameters.AddWithValue("@Ma", maDauSach);
                        cmdTang.ExecuteNonQuery();
                    }

                    trans.Commit();

                    string thongBao = canPhat && phiPhat > 0
                        ? $"Trả sách thành công! Phí phạt: {phiPhat:N0} đ. Lý do: {lyDo}"
                        : "Trả sách thành công!";

                    return KetQuaXuLy.OK(thongBao);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return KetQuaXuLy.Loi($"Lỗi: {ex.Message}");
                }
            }
        }

        #endregion
    }
}
