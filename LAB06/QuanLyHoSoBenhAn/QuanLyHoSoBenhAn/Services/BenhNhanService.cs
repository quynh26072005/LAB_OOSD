using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Models;

namespace QuanLyHoSoBenhAn.Services
{
    /// <summary>Kết quả của thao tác thu lệ phí năm / cấp hồ sơ.</summary>
    public class KetQuaCapHoSo
    {
        public int MaLePhi { get; set; }
        public int MaHoSo { get; set; }
        public bool DaTaoHoSoMoi { get; set; }
    }

    /// <summary>
    /// Tương ứng các lớp BenhNhan, HoSoBenhAn, LePhiNam trong biểu đồ lớp phân tích.
    /// Thực hiện giao dịch "Đăng ký + thu lệ phí + cấp hồ sơ" (BR12) trong một transaction.
    /// </summary>
    public class BenhNhanService
    {
        private readonly AuthService _auth = new AuthService();

        public DataTable TimKiem(string tuKhoa)
        {
            const string sql = @"
                SELECT bn.MaBenhNhan, bn.HoTen, bn.NgaySinh, bn.GioiTinh, bn.DiaChi,
                       bn.DienThoai, bn.Email, bn.SoGiayTo, bn.Anh,
                       CASE WHEN hs.MaHoSo IS NULL THEN N'Chưa có hồ sơ' ELSE hs.TrangThai END AS TrangThaiHoSo,
                       hs.MaHoSo
                FROM BenhNhan bn
                LEFT JOIN HoSoBenhAn hs ON hs.MaBenhNhan = bn.MaBenhNhan
                WHERE (@TuKhoa IS NULL OR @TuKhoa = N''
                       OR bn.HoTen LIKE N'%' + @TuKhoa + N'%'
                       OR bn.SoGiayTo LIKE N'%' + @TuKhoa + N'%'
                       OR bn.DienThoai LIKE N'%' + @TuKhoa + N'%')
                ORDER BY bn.MaBenhNhan DESC;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@TuKhoa", tuKhoa));
        }

        public BenhNhan LayTheoMa(int maBenhNhan)
        {
            const string sql = @"SELECT * FROM BenhNhan WHERE MaBenhNhan = @Ma;";
            DataRow row = DbHelper.ExecuteQuerySingleRow(sql, DbHelper.P("@Ma", maBenhNhan));
            return row == null ? null : Map(row);
        }

        /// <summary>Dùng cho TC04: phát hiện giấy tờ đã tồn tại để mở hồ sơ cũ thay vì tạo trùng.</summary>
        public BenhNhan LayTheoSoGiayTo(string soGiayTo)
        {
            if (string.IsNullOrWhiteSpace(soGiayTo)) return null;
            const string sql = @"SELECT * FROM BenhNhan WHERE SoGiayTo = @SoGiayTo;";
            DataRow row = DbHelper.ExecuteQuerySingleRow(sql, DbHelper.P("@SoGiayTo", soGiayTo));
            return row == null ? null : Map(row);
        }

        public int ThemMoi(BenhNhan bn, int? nguoiThucHien)
        {
            const string sql = @"
                INSERT INTO BenhNhan (HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, Email, SoGiayTo, Anh)
                VALUES (@HoTen, @NgaySinh, @GioiTinh, @DiaChi, @DienThoai, @Email, @SoGiayTo, @Anh);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            object idObj = DbHelper.ExecuteScalar(sql,
                DbHelper.P("@HoTen", bn.HoTen), DbHelper.P("@NgaySinh", bn.NgaySinh),
                DbHelper.P("@GioiTinh", bn.GioiTinh), DbHelper.P("@DiaChi", bn.DiaChi),
                DbHelper.P("@DienThoai", bn.DienThoai), DbHelper.P("@Email", bn.Email),
                DbHelper.P("@SoGiayTo", string.IsNullOrWhiteSpace(bn.SoGiayTo) ? null : bn.SoGiayTo),
                DbHelper.P("@Anh", bn.Anh));
            int id = Convert.ToInt32(idObj);
            _auth.GhiNhatKy(nguoiThucHien, "DangKyBenhNhan", "BenhNhan", id);
            return id;
        }

        public void CapNhat(BenhNhan bn, int? nguoiThucHien)
        {
            const string sql = @"
                UPDATE BenhNhan SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, DiaChi=@DiaChi,
                       DienThoai=@DienThoai, Email=@Email, SoGiayTo=@SoGiayTo, Anh=@Anh
                WHERE MaBenhNhan=@Ma;";
            DbHelper.ExecuteNonQuery(sql,
                DbHelper.P("@HoTen", bn.HoTen), DbHelper.P("@NgaySinh", bn.NgaySinh),
                DbHelper.P("@GioiTinh", bn.GioiTinh), DbHelper.P("@DiaChi", bn.DiaChi),
                DbHelper.P("@DienThoai", bn.DienThoai), DbHelper.P("@Email", bn.Email),
                DbHelper.P("@SoGiayTo", string.IsNullOrWhiteSpace(bn.SoGiayTo) ? null : bn.SoGiayTo),
                DbHelper.P("@Anh", bn.Anh), DbHelper.P("@Ma", bn.MaBenhNhan));
            _auth.GhiNhatKy(nguoiThucHien, "CapNhatBenhNhan", "BenhNhan", bn.MaBenhNhan);
        }

        public HoSoBenhAn LayHoSoTheoBenhNhan(int maBenhNhan)
        {
            const string sql = @"SELECT * FROM HoSoBenhAn WHERE MaBenhNhan = @Ma;";
            DataRow row = DbHelper.ExecuteQuerySingleRow(sql, DbHelper.P("@Ma", maBenhNhan));
            if (row == null) return null;
            return new HoSoBenhAn
            {
                MaHoSo = Convert.ToInt32(row["MaHoSo"]),
                MaBenhNhan = Convert.ToInt32(row["MaBenhNhan"]),
                NgayCap = Convert.ToDateTime(row["NgayCap"]),
                TrangThai = row["TrangThai"].ToString()
            };
        }

        public bool DaThuLePhiNam(int maBenhNhan, int nam)
        {
            const string sql = @"SELECT COUNT(*) FROM LePhiNam WHERE MaBenhNhan=@Ma AND NamApDung=@Nam;";
            object kq = DbHelper.ExecuteScalar(sql, DbHelper.P("@Ma", maBenhNhan), DbHelper.P("@Nam", nam));
            return Convert.ToInt32(kq) > 0;
        }

        /// <summary>
        /// BR02/BR12/TC01/TC02/TC03: thu lệ phí năm cho bệnh nhân; nếu đây là lệ phí đầu tiên và
        /// chưa có hồ sơ chính thì cấp hồ sơ ngay trong cùng transaction. Mọi lỗi (kể cả vi phạm UNIQUE
        /// của SQL Server khi hai nhân viên cùng thu trùng năm) đều rollback toàn bộ (TC08-style).
        /// </summary>
        public KetQuaCapHoSo ThuLePhiVaCapHoSo(int maBenhNhan, int namApDung, decimal soTien, string ghiChuNguon, int? nguoiThucHien)
        {
            if (soTien <= 0)
                throw new ArgumentException(N("Số tiền lệ phí phải lớn hơn 0."));

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int maLePhi = DbHelper.ExecuteInsertReturnId(conn, tran, @"
                            INSERT INTO LePhiNam (MaBenhNhan, NamApDung, SoTien, TrangThai)
                            VALUES (@Ma, @Nam, @SoTien, N'DaThu');",
                            DbHelper.P("@Ma", maBenhNhan), DbHelper.P("@Nam", namApDung), DbHelper.P("@SoTien", soTien));

                        object maHoSoObj = DbHelper.ExecuteScalar(conn, tran,
                            "SELECT MaHoSo FROM HoSoBenhAn WHERE MaBenhNhan=@Ma;", DbHelper.P("@Ma", maBenhNhan));

                        int maHoSo;
                        bool taoMoi = false;
                        if (maHoSoObj == null || maHoSoObj == DBNull.Value)
                        {
                            maHoSo = DbHelper.ExecuteInsertReturnId(conn, tran, @"
                                INSERT INTO HoSoBenhAn (MaBenhNhan, TrangThai) VALUES (@Ma, N'DangMo');",
                                DbHelper.P("@Ma", maBenhNhan));
                            taoMoi = true;
                        }
                        else
                        {
                            maHoSo = Convert.ToInt32(maHoSoObj);
                        }

                        DbHelper.ExecuteNonQuery(conn, tran, @"
                            INSERT INTO NhatKyTruyCap (MaNhanVien, HanhDong, DoiTuong, MaDoiTuong)
                            VALUES (@NV, @HanhDong, N'HoSoBenhAn', @MaHoSo);",
                            DbHelper.P("@NV", nguoiThucHien),
                            DbHelper.P("@HanhDong", taoMoi ? "ThuLePhiVaCapHoSoMoi" : "ThuLePhiNam"),
                            DbHelper.P("@MaHoSo", maHoSo));

                        tran.Commit();
                        return new KetQuaCapHoSo { MaLePhi = maLePhi, MaHoSo = maHoSo, DaTaoHoSoMoi = taoMoi };
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>TC05: tra cứu lịch sử khám theo hồ sơ (chỉ nhân viên có quyền mới được gọi từ Form).</summary>
        public DataTable LayLichSuKham(int maHoSo)
        {
            const string sql = @"
                SELECT pk.MaPhieuKham, pk.ThoiGian, k.TenKhoa, nv.HoTen AS BacSi,
                       pk.LoaiKham, pk.ChanDoan, pk.TrangThai
                FROM PhieuKham pk
                JOIN Khoa k ON k.MaKhoa = pk.MaKhoa
                JOIN NhanVien nv ON nv.MaNhanVien = pk.MaNhanVien
                WHERE pk.MaHoSo = @MaHoSo
                ORDER BY pk.ThoiGian DESC;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@MaHoSo", maHoSo));
        }

        private static string N(string s) => s;

        private static BenhNhan Map(DataRow row)
        {
            return new BenhNhan
            {
                MaBenhNhan = Convert.ToInt32(row["MaBenhNhan"]),
                HoTen = row["HoTen"].ToString(),
                NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                GioiTinh = row["GioiTinh"].ToString(),
                DiaChi = row["DiaChi"] as string,
                DienThoai = row["DienThoai"] as string,
                Email = row["Email"] as string,
                SoGiayTo = row["SoGiayTo"] as string,
                Anh = row["Anh"] as string
            };
        }
    }
}
