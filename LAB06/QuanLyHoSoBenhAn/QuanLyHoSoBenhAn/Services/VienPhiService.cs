using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Models;

namespace QuanLyHoSoBenhAn.Services
{
    /// <summary>
    /// Tương ứng HoaDonVienPhi, ChiTietVienPhi, ThanhToan (mục 2.4, 2.8, 3.5).
    /// Chỉ chuyển phiếu khám sang "Hoàn tất" khi tổng đã thu >= tổng phải thu (BR11).
    /// </summary>
    public class VienPhiService
    {
        private readonly AuthService _auth = new AuthService();

        /// <summary>
        /// Lập hóa đơn cho một phiếu khám, tự động gom chi phí xét nghiệm + thuốc đã phát sinh
        /// (nếu hóa đơn đã tồn tại thì trả về hóa đơn hiện có thay vì tạo trùng - UNIQUE MaPhieuKham).
        /// </summary>
        public int LapHoaDonTuDong(int maPhieuKham, int? nguoiThucHien)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        object maHoaDonObj = DbHelper.ExecuteScalar(conn, tran,
                            "SELECT MaHoaDon FROM HoaDonVienPhi WHERE MaPhieuKham=@Ma;", DbHelper.P("@Ma", maPhieuKham));
                        if (maHoaDonObj != null && maHoaDonObj != DBNull.Value)
                        {
                            tran.Commit();
                            return Convert.ToInt32(maHoaDonObj);
                        }

                        int maHoaDon = DbHelper.ExecuteInsertReturnId(conn, tran, @"
                            INSERT INTO HoaDonVienPhi (MaPhieuKham, TrangThai) VALUES (@Ma, N'ChuaThanhToan');",
                            DbHelper.P("@Ma", maPhieuKham));

                        // Chi phí xét nghiệm
                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO ChiTietVienPhi (MaHoaDon, NoiDung, SoLuong, DonGia)
                            SELECT @MaHoaDon, N'Xét nghiệm: ' + xn.TenXetNghiem, 1, xn.DonGia
                            FROM ChiDinhXetNghiem cd
                            JOIN XetNghiem xn ON xn.MaXetNghiem = cd.MaXetNghiem
                            WHERE cd.MaPhieuKham = @MaPhieu AND cd.TrangThai <> N'Huy';",
                            DbHelper.P("@MaHoaDon", maHoaDon), DbHelper.P("@MaPhieu", maPhieuKham));

                        // Chi phí thuốc
                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO ChiTietVienPhi (MaHoaDon, NoiDung, SoLuong, DonGia)
                            SELECT @MaHoaDon, N'Thuốc: ' + th.TenThuoc + N' ' + ISNULL(th.HamLuong,N''), ct.SoLuong, th.DonGia
                            FROM ToaThuoc t
                            JOIN ChiTietToaThuoc ct ON ct.MaToaThuoc = t.MaToaThuoc
                            JOIN Thuoc th ON th.MaThuoc = ct.MaThuoc
                            WHERE t.MaPhieuKham = @MaPhieu AND t.TrangThai <> N'Huy';",
                            DbHelper.P("@MaHoaDon", maHoaDon), DbHelper.P("@MaPhieu", maPhieuKham));

                        // Phí khám cơ bản (luôn có)
                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO ChiTietVienPhi (MaHoaDon, NoiDung, SoLuong, DonGia)
                            VALUES (@MaHoaDon, N'Phí khám bệnh', 1, 50000);",
                            DbHelper.P("@MaHoaDon", maHoaDon));

                        CapNhatTongTien(conn, tran, maHoaDon);

                        DbHelper.ExecuteNonQuery(conn, tran,
                            "UPDATE PhieuKham SET TrangThai=N'ChoVienPhi' WHERE MaPhieuKham=@Ma AND TrangThai NOT IN (N'HoanTat', N'Huy');",
                            DbHelper.P("@Ma", maPhieuKham));

                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO NhatKyTruyCap (MaNhanVien, HanhDong, DoiTuong, MaDoiTuong)
                            VALUES (@NV, N'LapHoaDonVienPhi', N'HoaDonVienPhi', @MaHoaDon);",
                            DbHelper.P("@NV", nguoiThucHien), DbHelper.P("@MaHoaDon", maHoaDon));

                        tran.Commit();
                        return maHoaDon;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        private void CapNhatTongTien(SqlConnection conn, SqlTransaction tran, int maHoaDon)
        {
            DbHelper.ExecuteNonQuery(conn, tran, @"
                UPDATE HoaDonVienPhi SET TongTien = (
                    SELECT ISNULL(SUM(ThanhTien), 0) FROM ChiTietVienPhi WHERE MaHoaDon = @Ma
                ) WHERE MaHoaDon = @Ma;", DbHelper.P("@Ma", maHoaDon));
        }

        public DataRow LayHoaDonChiTiet(int maHoaDon)
        {
            const string sql = @"
                SELECT hd.*, pk.MaPhieuKham, bn.HoTen AS TenBenhNhan,
                       ISNULL((SELECT SUM(SoTien) FROM ThanhToan WHERE MaHoaDon = hd.MaHoaDon), 0) AS DaThanhToan
                FROM HoaDonVienPhi hd
                JOIN PhieuKham pk ON pk.MaPhieuKham = hd.MaPhieuKham
                JOIN HoSoBenhAn hs ON hs.MaHoSo = pk.MaHoSo
                JOIN BenhNhan bn ON bn.MaBenhNhan = hs.MaBenhNhan
                WHERE hd.MaHoaDon = @Ma;";
            return DbHelper.ExecuteQuerySingleRow(sql, DbHelper.P("@Ma", maHoaDon));
        }

        public DataTable LayChiTietHoaDon(int maHoaDon)
        {
            const string sql = @"SELECT * FROM ChiTietVienPhi WHERE MaHoaDon=@Ma;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@Ma", maHoaDon));
        }

        /// <summary>Phiếu khám đã xong chuyên môn nhưng chưa được lập hóa đơn.</summary>
        public DataTable LayPhieuChoLapHoaDon()
        {
            const string sql = @"
                SELECT pk.MaPhieuKham, bn.HoTen, bn.SoGiayTo, k.TenKhoa, pk.ThoiGian
                FROM PhieuKham pk
                JOIN HoSoBenhAn hs ON hs.MaHoSo = pk.MaHoSo
                JOIN BenhNhan bn ON bn.MaBenhNhan = hs.MaBenhNhan
                JOIN Khoa k ON k.MaKhoa = pk.MaKhoa
                WHERE pk.TrangThai = N'ChoVienPhi'
                  AND NOT EXISTS (SELECT 1 FROM HoaDonVienPhi hd WHERE hd.MaPhieuKham = pk.MaPhieuKham)
                ORDER BY pk.ThoiGian ASC;";
            return DbHelper.ExecuteQuery(sql);
        }

        public DataTable LayHangDoiHoaDonChuaXong()
        {
            const string sql = @"
                SELECT hd.MaHoaDon, bn.HoTen, bn.SoGiayTo, hd.NgayLap, hd.TongTien, hd.TrangThai,
                       ISNULL((SELECT SUM(SoTien) FROM ThanhToan WHERE MaHoaDon = hd.MaHoaDon), 0) AS DaThanhToan
                FROM HoaDonVienPhi hd
                JOIN PhieuKham pk ON pk.MaPhieuKham = hd.MaPhieuKham
                JOIN HoSoBenhAn hs ON hs.MaHoSo = pk.MaHoSo
                JOIN BenhNhan bn ON bn.MaBenhNhan = hs.MaBenhNhan
                WHERE hd.TrangThai IN (N'ChuaThanhToan', N'MotPhan')
                ORDER BY hd.NgayLap ASC;";
            return DbHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// TC14/TC15/BR11: ghi một giao dịch thanh toán; chỉ chuyển hóa đơn "Đã thanh toán" và phiếu khám
        /// "Hoàn tất" khi tổng đã thu >= tổng phải thu. Toàn bộ trong một transaction (nghiệp vụ "Thanh toán/hoàn tất").
        /// </summary>
        public bool GhiThanhToanVaHoanTatNeuDu(int maHoaDon, decimal soTien, string phuongThuc, string maGiaoDich, int? nguoiThucHien)
        {
            if (soTien <= 0) throw new ArgumentException("Số tiền thanh toán phải lớn hơn 0.");

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO ThanhToan (MaHoaDon, SoTien, PhuongThuc, MaGiaoDich)
                            VALUES (@MaHoaDon, @SoTien, @PT, @MaGD);",
                            DbHelper.P("@MaHoaDon", maHoaDon), DbHelper.P("@SoTien", soTien),
                            DbHelper.P("@PT", phuongThuc), DbHelper.P("@MaGD", maGiaoDich));

                        decimal tongPhaiThu;
                        int maPhieuKham;
                        using (var cmd = new SqlCommand(
                            "SELECT TongTien, MaPhieuKham FROM HoaDonVienPhi WHERE MaHoaDon=@Ma;", conn, tran))
                        {
                            cmd.Parameters.Add(DbHelper.P("@Ma", maHoaDon));
                            using (var rd = cmd.ExecuteReader())
                            {
                                rd.Read();
                                tongPhaiThu = rd.GetDecimal(0);
                                maPhieuKham = rd.GetInt32(1);
                            }
                        }

                        object daThuObj = DbHelper.ExecuteScalar(conn, tran,
                            "SELECT ISNULL(SUM(SoTien),0) FROM ThanhToan WHERE MaHoaDon=@Ma;", DbHelper.P("@Ma", maHoaDon));
                        decimal daThu = Convert.ToDecimal(daThuObj);

                        bool duTien = daThu >= tongPhaiThu;
                        string trangThaiHD = duTien ? "DaThanhToan" : "MotPhan";

                        DbHelper.ExecuteNonQuery(conn, tran,
                            "UPDATE HoaDonVienPhi SET TrangThai=@TT WHERE MaHoaDon=@Ma;",
                            DbHelper.P("@TT", trangThaiHD), DbHelper.P("@Ma", maHoaDon));

                        if (duTien)
                        {
                            DbHelper.ExecuteNonQuery(conn, tran,
                                "UPDATE PhieuKham SET TrangThai=N'HoanTat' WHERE MaPhieuKham=@Ma AND TrangThai=N'ChoVienPhi';",
                                DbHelper.P("@Ma", maPhieuKham));
                        }

                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO NhatKyTruyCap (MaNhanVien, HanhDong, DoiTuong, MaDoiTuong)
                            VALUES (@NV, @HanhDong, N'HoaDonVienPhi', @Ma);",
                            DbHelper.P("@NV", nguoiThucHien),
                            DbHelper.P("@HanhDong", duTien ? "ThanhToanVaHoanTat" : "ThanhToanMotPhan"),
                            DbHelper.P("@Ma", maHoaDon));

                        tran.Commit();
                        return duTien;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable LayLichSuThanhToan(int maHoaDon)
        {
            const string sql = @"SELECT * FROM ThanhToan WHERE MaHoaDon=@Ma ORDER BY NgayThanhToan DESC;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@Ma", maHoaDon));
        }
    }
}
