using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Service
{
    /// <summary>Bản ghi hiển thị một quyết định (gộp lớp cha + thông tin riêng loại con).</summary>
    public class QuyetDinhView
    {
        public string SoQD { get; set; }
        public string LoaiQD { get; set; }
        public DateTime? NgayLap { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
        public string NoiDung { get; set; }
        public string NguoiKy { get; set; }
        public string ChiTiet { get; set; }

        public string LoaiHienThi
        {
            get
            {
                switch (LoaiQD)
                {
                    case LoaiQuyetDinh.TuyenDung: return "Tuyển dụng";
                    case LoaiQuyetDinh.ChucVu: return "Chức vụ";
                    case LoaiQuyetDinh.Luong: return "Lương";
                    case LoaiQuyetDinh.KhenThuong: return "Khen thưởng";
                    case LoaiQuyetDinh.KyLuat: return "Kỷ luật";
                    default: return LoaiQD;
                }
            }
        }
    }

    /// <summary>
    /// Quản lý các quyết định: tuyển dụng, chức vụ, lương, khen thưởng, kỷ luật.
    /// Lớp cha QuyetDinh lưu thông tin chung, các bảng con lưu thông tin riêng (TPT).
    /// </summary>
    public class QuyetDinhService
    {
        private readonly Repository<QuyetDinh> _repo = new Repository<QuyetDinh>();
        private readonly Repository<QdTuyenDung> _td = new Repository<QdTuyenDung>();
        private readonly Repository<QdChucVu> _cv = new Repository<QdChucVu>();
        private readonly Repository<QdLuong> _luong = new Repository<QdLuong>();
        private readonly Repository<QdKhenThuong> _kt = new Repository<QdKhenThuong>();
        private readonly Repository<QdKyLuat> _kl = new Repository<QdKyLuat>();

        public List<QuyetDinhView> GetDanhSach()
        {
            var result = new List<QuyetDinhView>();
            foreach (QuyetDinh qd in _repo.GetAll("SoQD"))
            {
                result.Add(ToView(qd));
            }
            return result;
        }

        public QuyetDinhView GetById(string soQD)
        {
            QuyetDinh qd = _repo.GetById(soQD);
            return qd == null ? null : ToView(qd);
        }

        public List<QdTuyenDung> GetQdTuyenDung() => _td.GetAll("SoQD");
        public List<QdChucVu> GetQdChucVu() => _cv.GetAll("SoQD");
        public List<QdLuong> GetQdLuong() => _luong.GetAll("SoQD");
        public List<QdKhenThuong> GetQdKhenThuong() => _kt.GetAll("SoQD");
        public List<QdKyLuat> GetQdKyLuat() => _kl.GetAll("SoQD");

        public QdTuyenDung GetChiTietTuyenDung(string soQD) => _td.GetById(soQD);
        public QdChucVu GetChiTietChucVu(string soQD) => _cv.GetById(soQD);
        public QdLuong GetChiTietLuong(string soQD) => _luong.GetById(soQD);
        public QdKhenThuong GetChiTietKhenThuong(string soQD) => _kt.GetById(soQD);
        public QdKyLuat GetChiTietKyLuat(string soQD) => _kl.GetById(soQD);

        /// <summary>Ngày hết thử việc = ngày hiệu lực + số tháng thử việc.</summary>
        public DateTime? TinhNgayHetThuViec(DateTime? ngayHieuLuc, int thoiGianThuViec)
        {
            if (ngayHieuLuc == null) return null;
            return ngayHieuLuc.Value.AddMonths(thoiGianThuViec);
        }

        public void TaoQD(QuyetDinh qd, params SqlCommandSpec[] detailCommands)
        {
            if (qd == null) throw new ArgumentNullException(nameof(qd));

            List<SqlCommandSpec> all = new List<SqlCommandSpec>
            {
                BuildInsertBase(qd)
            };
            if (detailCommands != null) all.AddRange(detailCommands);

            SqlHelper.ExecuteTransaction(all);
        }

        public void XoaQD(string soQD)
        {
            // Xóa bảng con tương ứng rồi xóa lớp cha.
            var commands = new List<SqlCommandSpec>
            {
                new SqlCommandSpec("DELETE FROM QD_TuyenDung  WHERE SoQD = @p", new[] { SqlHelper.P("@p", soQD) }),
                new SqlCommandSpec("DELETE FROM QD_ChucVu     WHERE SoQD = @p", new[] { SqlHelper.P("@p", soQD) }),
                new SqlCommandSpec("DELETE FROM QD_Luong      WHERE SoQD = @p", new[] { SqlHelper.P("@p", soQD) }),
                new SqlCommandSpec("DELETE FROM QD_KhenThuong WHERE SoQD = @p", new[] { SqlHelper.P("@p", soQD) }),
                new SqlCommandSpec("DELETE FROM QD_KyLuat     WHERE SoQD = @p", new[] { SqlHelper.P("@p", soQD) }),
                new SqlCommandSpec("DELETE FROM QuyetDinh     WHERE SoQD = @p", new[] { SqlHelper.P("@p", soQD) })
            };
            SqlHelper.ExecuteTransaction(commands);
        }

        public void CapNhatQD(QuyetDinh qd)
        {
            _repo.Update(qd);
        }

        public void CapNhatChiTietTuyenDung(QdTuyenDung d) => _td.Update(d);
        public void CapNhatChiTietChucVu(QdChucVu d) => _cv.Update(d);
        public void CapNhatChiTietLuong(QdLuong d) => _luong.Update(d);
        public void CapNhatChiTietKhenThuong(QdKhenThuong d) => _kt.Update(d);
        public void CapNhatChiTietKyLuat(QdKyLuat d) => _kl.Update(d);

        private QuyetDinhView ToView(QuyetDinh qd)
        {
            var v = new QuyetDinhView
            {
                SoQD = qd.SoQD,
                LoaiQD = qd.LoaiQD,
                NgayLap = qd.NgayLap,
                NgayHieuLuc = qd.NgayHieuLuc,
                NoiDung = qd.NoiDung,
                NguoiKy = qd.NguoiKy
            };

            switch (qd.LoaiQD)
            {
                case LoaiQuyetDinh.TuyenDung:
                    QdTuyenDung td = _td.GetById(qd.SoQD);
                    if (td != null)
                        v.ChiTiet = $"NV {td.MaNV} - PB {td.MaPB}, thử việc {td.ThoiGianThuViec} tháng, lương {td.LuongThuViec:N0}";
                    break;
                case LoaiQuyetDinh.ChucVu:
                    QdChucVu cv = _cv.GetById(qd.SoQD);
                    if (cv != null)
                        v.ChiTiet = $"NV {cv.MaNV} - chức vụ {cv.MaCV}, phụ cấp {cv.PhuCapChucVu:N0}";
                    break;
                case LoaiQuyetDinh.Luong:
                    QdLuong l = _luong.GetById(qd.SoQD);
                    if (l != null)
                        v.ChiTiet = $"NV {l.MaNV} - LCB {l.LuongCoBan:N0}, PCL {l.PhuCapLuong:N0}, PCĐH {l.PhuCapDocHai:N0}";
                    break;
                case LoaiQuyetDinh.KhenThuong:
                    QdKhenThuong kt = _kt.GetById(qd.SoQD);
                    if (kt != null)
                        v.ChiTiet = $"{kt.LoaiKhenThuong}: {kt.SoTien:N0} - {kt.DoiTuong}";
                    break;
                case LoaiQuyetDinh.KyLuat:
                    QdKyLuat kl = _kl.GetById(qd.SoQD);
                    if (kl != null)
                        v.ChiTiet = $"{kl.HinhThucKyLuat}: phạt {kl.SoTienPhat:N0} - {kl.DoiTuong}";
                    break;
            }
            return v;
        }

        public static SqlCommandSpec BuildInsertBase(QuyetDinh qd)
        {
            string sql = @"INSERT INTO QuyetDinh (SoQD, LoaiQD, NgayLap, NgayHieuLuc, NoiDung, NguoiKy)
                           VALUES (@SoQD, @LoaiQD, @NgayLap, @NgayHieuLuc, @NoiDung, @NguoiKy)";
            var ps = new[]
            {
                SqlHelper.P("@SoQD", qd.SoQD),
                SqlHelper.P("@LoaiQD", qd.LoaiQD),
                SqlHelper.P("@NgayLap", qd.NgayLap),
                SqlHelper.P("@NgayHieuLuc", (object)qd.NgayHieuLuc ?? DBNull.Value),
                SqlHelper.P("@NoiDung", qd.NoiDung),
                SqlHelper.P("@NguoiKy", qd.NguoiKy)
            };
            return new SqlCommandSpec(sql, ps);
        }

        public SqlCommandSpec BuildInsertTuyenDung(QdTuyenDung d)
        {
            return new SqlCommandSpec(
                @"INSERT INTO QD_TuyenDung (SoQD, ThoiGianThuViec, LuongThuViec, DieuKhoan, MaNV, MaPB)
                  VALUES (@SoQD, @ThoiGianThuViec, @LuongThuViec, @DieuKhoan, @MaNV, @MaPB)",
                new[]
                {
                    SqlHelper.P("@SoQD", d.SoQD),
                    SqlHelper.P("@ThoiGianThuViec", d.ThoiGianThuViec),
                    SqlHelper.P("@LuongThuViec", d.LuongThuViec),
                    SqlHelper.P("@DieuKhoan", d.DieuKhoan),
                    SqlHelper.P("@MaNV", d.MaNV),
                    SqlHelper.P("@MaPB", d.MaPB)
                });
        }

        public SqlCommandSpec BuildInsertChucVu(QdChucVu d)
        {
            return new SqlCommandSpec(
                @"INSERT INTO QD_ChucVu (SoQD, MaNV, MaCV, PhuCapChucVu)
                  VALUES (@SoQD, @MaNV, @MaCV, @PhuCapChucVu)",
                new[]
                {
                    SqlHelper.P("@SoQD", d.SoQD),
                    SqlHelper.P("@MaNV", d.MaNV),
                    SqlHelper.P("@MaCV", d.MaCV),
                    SqlHelper.P("@PhuCapChucVu", d.PhuCapChucVu)
                });
        }

        public SqlCommandSpec BuildInsertLuong(QdLuong d)
        {
            return new SqlCommandSpec(
                @"INSERT INTO QD_Luong (SoQD, MaNV, LuongCoBan, PhuCapLuong, PhuCapDocHai)
                  VALUES (@SoQD, @MaNV, @LuongCoBan, @PhuCapLuong, @PhuCapDocHai)",
                new[]
                {
                    SqlHelper.P("@SoQD", d.SoQD),
                    SqlHelper.P("@MaNV", d.MaNV),
                    SqlHelper.P("@LuongCoBan", d.LuongCoBan),
                    SqlHelper.P("@PhuCapLuong", d.PhuCapLuong),
                    SqlHelper.P("@PhuCapDocHai", d.PhuCapDocHai)
                });
        }

        public SqlCommandSpec BuildInsertKhenThuong(QdKhenThuong d)
        {
            return new SqlCommandSpec(
                @"INSERT INTO QD_KhenThuong (SoQD, LoaiKhenThuong, SoTien, DoiTuong)
                  VALUES (@SoQD, @LoaiKhenThuong, @SoTien, @DoiTuong)",
                new[]
                {
                    SqlHelper.P("@SoQD", d.SoQD),
                    SqlHelper.P("@LoaiKhenThuong", d.LoaiKhenThuong),
                    SqlHelper.P("@SoTien", d.SoTien),
                    SqlHelper.P("@DoiTuong", d.DoiTuong)
                });
        }

        public SqlCommandSpec BuildInsertKyLuat(QdKyLuat d)
        {
            return new SqlCommandSpec(
                @"INSERT INTO QD_KyLuat (SoQD, HinhThucKyLuat, SoTienPhat, DoiTuong)
                  VALUES (@SoQD, @HinhThucKyLuat, @SoTienPhat, @DoiTuong)",
                new[]
                {
                    SqlHelper.P("@SoQD", d.SoQD),
                    SqlHelper.P("@HinhThucKyLuat", d.HinhThucKyLuat),
                    SqlHelper.P("@SoTienPhat", d.SoTienPhat),
                    SqlHelper.P("@DoiTuong", d.DoiTuong)
                });
        }
    }
}