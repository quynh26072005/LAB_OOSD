using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    /// <summary>
    /// Tương ứng lớp LichKhamDinhKy: DatLich(), DoiLich(), HuyLich().
    /// Quyền lợi khám định kỳ tính theo bệnh nhân/năm/quý (BR09); chỉ xác nhận khi còn suất (BR10).
    /// </summary>
    public class LichKhamService
    {
        private readonly AuthService _auth = new AuthService();

        public DataTable TimKiem(string tuKhoa, int? nam)
        {
            const string sql = @"
                SELECT lh.MaLichHen, bn.HoTen, bn.SoGiayTo, lh.Nam, lh.Quy,
                       lh.ThoiGianHen, lh.TrangThai, lh.MaBenhNhan
                FROM LichKhamDinhKy lh
                JOIN BenhNhan bn ON bn.MaBenhNhan = lh.MaBenhNhan
                WHERE (@TuKhoa IS NULL OR @TuKhoa = N'' OR bn.HoTen LIKE N'%'+@TuKhoa+N'%' OR bn.SoGiayTo LIKE N'%'+@TuKhoa+N'%')
                  AND (@Nam IS NULL OR lh.Nam = @Nam)
                ORDER BY lh.ThoiGianHen DESC;";
            return DbHelper.ExecuteQuery(sql, DbHelper.P("@TuKhoa", tuKhoa), DbHelper.P("@Nam", nam));
        }

        /// <summary>Kiểm tra bệnh nhân còn suất quý hay chưa (chưa có lịch hiệu lực trong quý đó) - BR09.</summary>
        public bool ConSuatQuy(int maBenhNhan, int nam, int quy)
        {
            const string sql = @"
                SELECT COUNT(*) FROM LichKhamDinhKy
                WHERE MaBenhNhan=@Ma AND Nam=@Nam AND Quy=@Quy AND TrangThai NOT IN (N'Huy', N'TuChoi');";
            object kq = DbHelper.ExecuteScalar(sql, DbHelper.P("@Ma", maBenhNhan), DbHelper.P("@Nam", nam), DbHelper.P("@Quy", quy));
            return Convert.ToInt32(kq) == 0;
        }

        /// <summary>
        /// TC06-TC08: đặt lịch. Dựa vào UNIQUE INDEX lọc (filtered) trong CSDL để chống hai nhân viên
        /// đặt trùng suất cùng lúc - nếu vi phạm, SqlException (số lỗi 2601/2627) được bắt và báo "đã dùng suất".
        /// </summary>
        public int DatLich(int maBenhNhan, int nam, int quy, DateTime thoiGianHen, int? nguoiThucHien)
        {
            try
            {
                const string sql = @"
                    INSERT INTO LichKhamDinhKy (MaBenhNhan, Nam, Quy, ThoiGianHen, TrangThai)
                    VALUES (@Ma, @Nam, @Quy, @TG, N'DaXacNhan');
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                object id = DbHelper.ExecuteScalar(sql,
                    DbHelper.P("@Ma", maBenhNhan), DbHelper.P("@Nam", nam), DbHelper.P("@Quy", quy), DbHelper.P("@TG", thoiGianHen));
                int maLich = Convert.ToInt32(id);
                _auth.GhiNhatKy(nguoiThucHien, "DatLichKhamDinhKy", "LichKhamDinhKy", maLich);
                return maLich;
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                throw new InvalidOperationException(
                    "Bệnh nhân đã dùng suất khám định kỳ quý này (có thể một nhân viên khác vừa đặt). Vui lòng tải lại.");
            }
        }

        public void DoiLich(int maLichHen, DateTime thoiGianMoi, int? nguoiThucHien)
        {
            const string sql = @"UPDATE LichKhamDinhKy SET ThoiGianHen=@TG WHERE MaLichHen=@Ma AND TrangThai NOT IN (N'Huy', N'HoanTat', N'TuChoi');";
            int rows = DbHelper.ExecuteNonQuery(sql, DbHelper.P("@TG", thoiGianMoi), DbHelper.P("@Ma", maLichHen));
            if (rows == 0) throw new InvalidOperationException("Không thể đổi lịch (lịch đã hủy/hoàn tất hoặc không tồn tại).");
            _auth.GhiNhatKy(nguoiThucHien, "DoiLichKhamDinhKy", "LichKhamDinhKy", maLichHen);
        }

        public void HuyLich(int maLichHen, int? nguoiThucHien)
        {
            const string sql = @"UPDATE LichKhamDinhKy SET TrangThai=N'Huy' WHERE MaLichHen=@Ma AND TrangThai NOT IN (N'HoanTat');";
            int rows = DbHelper.ExecuteNonQuery(sql, DbHelper.P("@Ma", maLichHen));
            if (rows == 0) throw new InvalidOperationException("Không thể hủy lịch đã hoàn tất.");
            _auth.GhiNhatKy(nguoiThucHien, "HuyLichKhamDinhKy", "LichKhamDinhKy", maLichHen);
        }

        public void TiepNhanLich(int maLichHen, int? nguoiThucHien)
        {
            const string sql = @"UPDATE LichKhamDinhKy SET TrangThai=N'DaTiepNhan' WHERE MaLichHen=@Ma AND TrangThai=N'DaXacNhan';";
            DbHelper.ExecuteNonQuery(sql, DbHelper.P("@Ma", maLichHen));
            _auth.GhiNhatKy(nguoiThucHien, "TiepNhanLichKhamDinhKy", "LichKhamDinhKy", maLichHen);
        }
    }
}
