using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Models;

namespace QuanLyHoSoBenhAn.Services
{
    /// <summary>
    /// Tương ứng PhieuKham, ChiDinhXetNghiem, KetQuaXetNghiem, ToaThuoc, ChiTietToaThuoc
    /// trong biểu đồ lớp phân tích/chi tiết (mục 2.5, 2.10, 3.1-3.4) - luồng UC01.
    /// </summary>
    public class KhamBenhService
    {
        private readonly AuthService _auth = new AuthService();

        /* ---------- Tiếp nhận khám (3.2 Sequence tiếp nhận khám) ---------- */

        public int TiepNhan(int maHoSo, int maKhoa, int maBacSi, int? maLichHen, string loaiKham, int? nguoiThucHien)
        {
            const string sql = @"
                INSERT INTO PhieuKham (MaHoSo, MaKhoa, MaNhanVien, MaLichHen, LoaiKham, TrangThai)
                VALUES (@MaHoSo, @MaKhoa, @MaBacSi, @MaLichHen, @LoaiKham, N'ChoKham');
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            object id = DbHelper.ExecuteScalar(sql,
                DbHelper.P("@MaHoSo", maHoSo), DbHelper.P("@MaKhoa", maKhoa), DbHelper.P("@MaBacSi", maBacSi),
                DbHelper.P("@MaLichHen", maLichHen), DbHelper.P("@LoaiKham", loaiKham ?? "ThuongQuy"));
            int maPhieu = Convert.ToInt32(id);
            _auth.GhiNhatKy(nguoiThucHien, "TiepNhanKham", "PhieuKham", maPhieu);
            return maPhieu;
        }

        public DataTable LayHangDoiTiepNhan()
        {
            const string sql = @"
                SELECT pk.MaPhieuKham, bn.HoTen, bn.SoGiayTo, k.TenKhoa, nv.HoTen AS BacSi,
                       pk.ThoiGian, pk.LoaiKham, pk.TrangThai
                FROM PhieuKham pk
                JOIN HoSoBenhAn hs ON hs.MaHoSo = pk.MaHoSo
                JOIN BenhNhan bn ON bn.MaBenhNhan = hs.MaBenhNhan
                JOIN Khoa k ON k.MaKhoa = pk.MaKhoa
                JOIN NhanVien nv ON nv.MaNhanVien = pk.MaNhanVien
                WHERE pk.TrangThai IN (N'ChoKham', N'DangKham', N'ChoXetNghiem')
                ORDER BY pk.ThoiGian ASC;";
            return DbHelper.ExecuteQuery(sql);
        }

        public PhieuKham LayPhieuKham(int maPhieuKham)
        {
            const string sql = @"SELECT * FROM PhieuKham WHERE MaPhieuKham=@Ma;";
            DataRow row = DbHelper.ExecuteQuerySingleRow(sql, DbHelper.P("@Ma", maPhieuKham));
            return row == null ? null : MapPhieuKham(row);
        }

        public DataRow LayPhieuKhamChiTiet(int maPhieuKham)
        {
            const string sql = @"
                SELECT pk.*, bn.HoTen AS TenBenhNhan, bn.NgaySinh, bn.GioiTinh, bn.MaBenhNhan,
                       k.TenKhoa, nv.HoTen AS TenBacSi
                FROM PhieuKham pk
                JOIN HoSoBenhAn hs ON hs.MaHoSo = pk.MaHoSo
                JOIN BenhNhan bn ON bn.MaBenhNhan = hs.MaBenhNhan
                JOIN Khoa k ON k.MaKhoa = pk.MaKhoa
                JOIN NhanVien nv ON nv.MaNhanVien = pk.MaNhanVien
                WHERE pk.MaPhieuKham = @Ma;";
            return DbHelper.ExecuteQuerySingleRow(sql, DbHelper.P("@Ma", maPhieuKham));
        }

        /* ---------- Khám bệnh: ghi triệu chứng/sinh hiệu/chẩn đoán ---------- */

        public void GhiNhanKham(int maPhieuKham, string trieuChung, string sinhHieu, string chanDoan, int? nguoiThucHien)
        {
            const string sql = @"
                UPDATE PhieuKham SET TrieuChung=@TC, SinhHieu=@SH, ChanDoan=@CD,
                       TrangThai = CASE WHEN TrangThai=N'ChoKham' THEN N'DangKham' ELSE TrangThai END
                WHERE MaPhieuKham=@Ma;";
            DbHelper.ExecuteNonQuery(sql,
                DbHelper.P("@TC", trieuChung), DbHelper.P("@SH", sinhHieu), DbHelper.P("@CD", chanDoan),
                DbHelper.P("@Ma", maPhieuKham));
            _auth.GhiNhatKy(nguoiThucHien, "GhiNhanKham", "PhieuKham", maPhieuKham);
        }

        public void ChuyenTrangThai(int maPhieuKham, string trangThaiMoi, int? nguoiThucHien)
        {
            const string sql = @"UPDATE PhieuKham SET TrangThai=@TT WHERE MaPhieuKham=@Ma;";
            DbHelper.ExecuteNonQuery(sql, DbHelper.P("@TT", trangThaiMoi), DbHelper.P("@Ma", maPhieuKham));
            _auth.GhiNhatKy(nguoiThucHien, "ChuyenTrangThaiPhieuKham:" + trangThaiMoi, "PhieuKham", maPhieuKham);
        }

        /* ---------- Chỉ định xét nghiệm (BR06/BR07, TC09) ---------- */

        public int TaoChiDinhXetNghiem(int maPhieuKham, int maXetNghiem, int maBacSi, string ghiChu, int? nguoiThucHien)
        {
            // SCOPE_IDENTITY phải lấy ngay sau INSERT -> tách truy vấn để tránh UPDATE ghi đè identity scope
            object id = DbHelper.ExecuteScalar(@"
                INSERT INTO ChiDinhXetNghiem (MaPhieuKham, MaXetNghiem, MaNhanVien, GhiChu, TrangThai)
                VALUES (@MaPhieu, @MaXN, @MaBacSi, @GhiChu, N'ChoThucHien');
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                DbHelper.P("@MaPhieu", maPhieuKham), DbHelper.P("@MaXN", maXetNghiem),
                DbHelper.P("@MaBacSi", maBacSi), DbHelper.P("@GhiChu", ghiChu));
            int maChiDinh = Convert.ToInt32(id);

            DbHelper.ExecuteNonQuery(
                "UPDATE PhieuKham SET TrangThai=N'ChoXetNghiem' WHERE MaPhieuKham=@MaPhieu AND TrangThai NOT IN (N'HoanTat', N'Huy');",
                DbHelper.P("@MaPhieu", maPhieuKham));

            _auth.GhiNhatKy(nguoiThucHien, "ChiDinhXetNghiem", "ChiDinhXetNghiem", maChiDinh);
            return maChiDinh;
        }

        public DataTable LayChiDinhTheoPhieu(int maPhieuKham)
        {
            const string sql = @"
                SELECT cd.MaChiDinh, xn.TenXetNghiem, cd.NgayChiDinh, cd.TrangThai, cd.GhiChu,
                       kq.KetQua, kq.DonVi, kq.KhoangThamChieu, kq.NhanXet, kq.ThoiGian AS ThoiGianKetQua
                FROM ChiDinhXetNghiem cd
                JOIN XetNghiem xn ON xn.MaXetNghiem = cd.MaXetNghiem
                LEFT JOIN KetQuaXetNghiem kq ON kq.MaChiDinh = cd.MaChiDinh
                WHERE cd.MaPhieuKham = @Ma
                ORDER BY cd.NgayChiDinh DESC;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@Ma", maPhieuKham));
        }

        /* ---------- Hàng đợi và nhập kết quả xét nghiệm (TC09-TC11) ---------- */

        public DataTable LayHangDoiXetNghiem()
        {
            const string sql = @"
                SELECT cd.MaChiDinh, bn.HoTen, bn.SoGiayTo, xn.TenXetNghiem, cd.NgayChiDinh, cd.GhiChu, cd.MaPhieuKham
                FROM ChiDinhXetNghiem cd
                JOIN PhieuKham pk ON pk.MaPhieuKham = cd.MaPhieuKham
                JOIN HoSoBenhAn hs ON hs.MaHoSo = pk.MaHoSo
                JOIN BenhNhan bn ON bn.MaBenhNhan = hs.MaBenhNhan
                JOIN XetNghiem xn ON xn.MaXetNghiem = cd.MaXetNghiem
                WHERE cd.TrangThai = N'ChoThucHien'
                ORDER BY cd.NgayChiDinh ASC;";
            return DbHelper.ExecuteQuery(sql);
        }

        /// <summary>TC10/TC11: kết quả luôn gắn đúng MaChiDinh truyền vào, không thể "gắn nhầm" sang phiếu khác.</summary>
        public void NhapKetQua(int maChiDinh, int maNhanVienXetNghiem, string ketQua, string donVi,
            string khoangThamChieu, string nhanXet, int? nguoiThucHien)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        object daCo = DbHelper.ExecuteScalar(conn, tran,
                            "SELECT COUNT(*) FROM KetQuaXetNghiem WHERE MaChiDinh=@Ma;", DbHelper.P("@Ma", maChiDinh));
                        if (Convert.ToInt32(daCo) > 0)
                            throw new InvalidOperationException("Chỉ định này đã có kết quả.");

                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO KetQuaXetNghiem (MaChiDinh, MaNhanVien, KetQua, DonVi, KhoangThamChieu, NhanXet)
                            VALUES (@MaChiDinh, @MaNV, @KetQua, @DonVi, @Khoang, @NhanXet);",
                            DbHelper.P("@MaChiDinh", maChiDinh), DbHelper.P("@MaNV", maNhanVienXetNghiem),
                            DbHelper.P("@KetQua", ketQua), DbHelper.P("@DonVi", donVi),
                            DbHelper.P("@Khoang", khoangThamChieu), DbHelper.P("@NhanXet", nhanXet));

                        DbHelper.ExecuteNonQuery(conn, tran,
                            "UPDATE ChiDinhXetNghiem SET TrangThai=N'DaCoKetQua' WHERE MaChiDinh=@Ma;",
                            DbHelper.P("@Ma", maChiDinh));

                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO NhatKyTruyCap (MaNhanVien, HanhDong, DoiTuong, MaDoiTuong)
                            VALUES (@NV, N'NhapKetQuaXetNghiem', N'ChiDinhXetNghiem', @Ma);",
                            DbHelper.P("@NV", nguoiThucHien), DbHelper.P("@Ma", maChiDinh));

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        /* ---------- Toa thuốc (BR08/BR13, TC12/TC13) ---------- */

        /// <summary>
        /// Lập toa và toàn bộ dòng thuốc trong MỘT transaction nguyên tử: nếu bất kỳ dòng nào không
        /// hợp lệ hoặc lưu thất bại, toàn bộ toa được rollback (BR13/TC13). Một phiếu khám chỉ có
        /// tối đa một toa chính (UNIQUE MaPhieuKham trên bảng ToaThuoc - BR08).
        /// </summary>
        public int TaoToaVaChiTiet(int maPhieuKham, int maBacSi, string loiDan, List<ChiTietToaThuoc> dsThuoc, int? nguoiThucHien)
        {
            if (dsThuoc == null || dsThuoc.Count == 0)
                throw new ArgumentException("Toa thuốc xác nhận phải có ít nhất một dòng thuốc.");

            foreach (var ct in dsThuoc)
            {
                if (ct.SoLuong <= 0 || ct.SoNgay <= 0)
                    throw new ArgumentException($"Dòng thuốc mã {ct.MaThuoc} có số lượng/số ngày không hợp lệ.");
            }

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int maToa = DbHelper.ExecuteInsertReturnId(conn, tran, @"
                            INSERT INTO ToaThuoc (MaPhieuKham, MaNhanVien, LoiDan, TrangThai)
                            VALUES (@MaPhieu, @MaBacSi, @LoiDan, N'DaXacNhan');",
                            DbHelper.P("@MaPhieu", maPhieuKham), DbHelper.P("@MaBacSi", maBacSi), DbHelper.P("@LoiDan", loiDan));

                        foreach (var ct in dsThuoc)
                        {
                            DbHelper.ExecuteNonQuery(conn, tran, @"
                                INSERT INTO ChiTietToaThuoc (MaToaThuoc, MaThuoc, SoLuong, LieuDung, TanSuat, SoNgay, CachDung)
                                VALUES (@MaToa, @MaThuoc, @SoLuong, @LieuDung, @TanSuat, @SoNgay, @CachDung);",
                                DbHelper.P("@MaToa", maToa), DbHelper.P("@MaThuoc", ct.MaThuoc),
                                DbHelper.P("@SoLuong", ct.SoLuong), DbHelper.P("@LieuDung", ct.LieuDung),
                                DbHelper.P("@TanSuat", ct.TanSuat), DbHelper.P("@SoNgay", ct.SoNgay),
                                DbHelper.P("@CachDung", ct.CachDung));
                        }

                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO NhatKyTruyCap (MaNhanVien, HanhDong, DoiTuong, MaDoiTuong)
                            VALUES (@NV, N'LapToaThuoc', N'ToaThuoc', @MaToa);",
                            DbHelper.P("@NV", nguoiThucHien), DbHelper.P("@MaToa", maToa));

                        tran.Commit();
                        return maToa;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable LayToaTheoPhieu(int maPhieuKham)
        {
            const string sql = @"
                SELECT ct.MaChiTietToa, t.MaToaThuoc, th.TenThuoc, th.HamLuong, ct.SoLuong,
                       ct.LieuDung, ct.TanSuat, ct.SoNgay, ct.CachDung, th.DonGia,
                       (ct.SoLuong * th.DonGia) AS ThanhTien
                FROM ToaThuoc t
                JOIN ChiTietToaThuoc ct ON ct.MaToaThuoc = t.MaToaThuoc
                JOIN Thuoc th ON th.MaThuoc = ct.MaThuoc
                WHERE t.MaPhieuKham = @Ma;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@Ma", maPhieuKham));
        }

        /// <summary>Hoàn tất chuyên môn: chuyển phiếu sang Chờ viện phí (bước 11, UC01).</summary>
        public void HoanTatChuyenMon(int maPhieuKham, int? nguoiThucHien)
        {
            ChuyenTrangThai(maPhieuKham, "ChoVienPhi", nguoiThucHien);
        }

        private static PhieuKham MapPhieuKham(DataRow row)
        {
            return new PhieuKham
            {
                MaPhieuKham = Convert.ToInt32(row["MaPhieuKham"]),
                MaHoSo = Convert.ToInt32(row["MaHoSo"]),
                MaKhoa = Convert.ToInt32(row["MaKhoa"]),
                MaNhanVien = Convert.ToInt32(row["MaNhanVien"]),
                MaLichHen = row["MaLichHen"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["MaLichHen"]),
                ThoiGian = Convert.ToDateTime(row["ThoiGian"]),
                LoaiKham = row["LoaiKham"].ToString(),
                TrieuChung = row["TrieuChung"] as string,
                SinhHieu = row["SinhHieu"] as string,
                ChanDoan = row["ChanDoan"] as string,
                TrangThai = row["TrangThai"].ToString()
            };
        }
    }
}
