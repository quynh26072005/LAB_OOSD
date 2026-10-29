using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Service
{
    /// <summary>
    /// Tính lương hằng tháng: tổng hợp lương cơ bản + phụ cấp (lương, độc hại,
    /// chức vụ) + tiền phụ trội (hệ số giờ × đơn giá) + tiền thưởng - tiền phạt
    /// - bảo hiểm (BHYT, BHXH) thành Thực lãnh, lưu vào BangLuong và ChiTietBangLuong.
    /// </summary>
    public class LuongService
    {
        private const double NgayCong = 22.0;   // ngày công chuẩn/tháng
        private const double GioCong = 8.0;     // giờ công chuẩn/ngày

        private readonly NhanVienService _nhanVienSvc = new NhanVienService();
        private readonly Repository<BangLuong> _bl = new Repository<BangLuong>();

        public void TinhLuong(string maBL, int thang, int nam, DateTime ngayLap)
        {
            if (_bl.GetById(maBL) != null)
                throw new InvalidOperationException($"Bảng lương {maBL} đã tồn tại.");

            List<ChiTietBangLuong> rows = new List<ChiTietBangLuong>();
            foreach (NhanVien nv in _nhanVienSvc.GetAll())
            {
                QdLuong luong = GetLatestLuong(nv.MaNV);
                if (luong == null) continue;   // chỉ tính lương cho nhân viên đã có quyết định lương

                QdChucVu chucVu = GetLatestChucVu(nv.MaNV);

                decimal luongCB = luong.LuongCoBan;
                decimal phuCap = luong.PhuCapLuong + luong.PhuCapDocHai + (chucVu?.PhuCapChucVu ?? 0);
                double donGio = (double)luongCB / (NgayCong * GioCong);

                decimal tienPhuTroi = (decimal)(GetTongGioPhuTroiQuyDoi(nv.MaNV, thang, nam) * donGio);
                decimal tienThuong = GetTienThuong(nv.MaNV, thang, nam);
                decimal tienPhat = GetTienPhat(nv.MaNV, thang, nam);
                decimal bhyt = GetBaoHiem(nv.MaNV, "BHYT");
                decimal bhxh = GetBaoHiem(nv.MaNV, "BHXH");
                decimal thucLanh = luongCB + phuCap + tienPhuTroi + tienThuong - tienPhat - bhyt - bhxh;

                rows.Add(new ChiTietBangLuong
                {
                    MaBL = maBL,
                    MaNV = nv.MaNV,
                    LuongCB = luongCB,
                    PhuCap = phuCap,
                    TienPhuTroi = tienPhuTroi,
                    TienThuong = tienThuong,
                    TienPhat = tienPhat,
                    BHYT = bhyt,
                    BHXH = bhxh,
                    ThucLanh = thucLanh
                });
            }

            var commands = new List<SqlCommandSpec>
            {
                new SqlCommandSpec(
                    @"INSERT INTO BangLuong (MaBL, Thang, Nam, NgayLap) VALUES (@MaBL, @Thang, @Nam, @NgayLap)",
                    new[]
                    {
                        SqlHelper.P("@MaBL", maBL),
                        SqlHelper.P("@Thang", thang),
                        SqlHelper.P("@Nam", nam),
                        SqlHelper.P("@NgayLap", ngayLap)
                    })
            };

            foreach (ChiTietBangLuong r in rows)
            {
                commands.Add(new SqlCommandSpec(
                    @"INSERT INTO ChiTietBangLuong
                      (MaBL, MaNV, LuongCB, PhuCap, TienPhuTroi, TienThuong, TienPhat, BHYT, BHXH, ThucLanh)
                      VALUES (@MaBL, @MaNV, @LuongCB, @PhuCap, @TienPhuTroi, @TienThuong, @TienPhat, @BHYT, @BHXH, @ThucLanh)",
                    new[]
                    {
                        SqlHelper.P("@MaBL", r.MaBL),
                        SqlHelper.P("@MaNV", r.MaNV),
                        SqlHelper.P("@LuongCB", r.LuongCB),
                        SqlHelper.P("@PhuCap", r.PhuCap),
                        SqlHelper.P("@TienPhuTroi", r.TienPhuTroi),
                        SqlHelper.P("@TienThuong", r.TienThuong),
                        SqlHelper.P("@TienPhat", r.TienPhat),
                        SqlHelper.P("@BHYT", r.BHYT),
                        SqlHelper.P("@BHXH", r.BHXH),
                        SqlHelper.P("@ThucLanh", r.ThucLanh)
                    }));
            }

            SqlHelper.ExecuteTransaction(commands);
        }

        public List<BangLuong> GetDanhSach()
        {
            return _bl.GetAll("Nam DESC, Thang DESC");
        }

        public List<ChiTietBangLuong> GetChiTiet(string maBL)
        {
            return SqlHelper.ExecuteList<ChiTietBangLuong>(
                @"SELECT ct.MaBL, ct.MaNV, nv.HoTen, ct.LuongCB, ct.PhuCap, ct.TienPhuTroi,
                         ct.TienThuong, ct.TienPhat, ct.BHYT, ct.BHXH, ct.ThucLanh
                  FROM ChiTietBangLuong ct
                  LEFT JOIN NhanVien nv ON nv.MaNV = ct.MaNV
                  WHERE ct.MaBL = @p ORDER BY ct.MaNV",
                SqlHelper.P("@p", maBL));
        }

        private QdLuong GetLatestLuong(string maNV)
        {
            return SqlHelper.ExecuteSingle<QdLuong>(
                @"SELECT TOP 1 l.*
                  FROM QD_Luong l
                  JOIN QuyetDinh b ON b.SoQD = l.SoQD
                  WHERE l.MaNV = @p
                  ORDER BY b.NgayLap DESC, b.NgayHieuLuc DESC",
                SqlHelper.P("@p", maNV));
        }

        private QdChucVu GetLatestChucVu(string maNV)
        {
            return SqlHelper.ExecuteSingle<QdChucVu>(
                @"SELECT TOP 1 c.*
                  FROM QD_ChucVu c
                  JOIN QuyetDinh b ON b.SoQD = c.SoQD
                  WHERE c.MaNV = @p
                  ORDER BY b.NgayLap DESC, b.NgayHieuLuc DESC",
                SqlHelper.P("@p", maNV));
        }

        private double GetTongGioPhuTroiQuyDoi(string maNV, int thang, int nam)
        {
            object v = SqlHelper.ExecuteScalar(
                @"SELECT ISNULL(SUM(ct.SoGio * pp.HeSo), 0)
                  FROM ChiTietPhuTroi ct
                  JOIN PhieuPhuTroi pp ON pp.SoPhieu = ct.SoPhieu
                  WHERE ct.MaNV = @p AND MONTH(pp.NgayPhuTroi) = @t AND YEAR(pp.NgayPhuTroi) = @n",
                SqlHelper.P("@p", maNV), SqlHelper.P("@t", thang), SqlHelper.P("@n", nam));
            return Convert.ToDouble(v);
        }

        private decimal GetTienThuong(string maNV, int thang, int nam)
        {
            object v = SqlHelper.ExecuteScalar(
                @"SELECT ISNULL(SUM(kt.SoTien), 0)
                  FROM QD_KhenThuong kt
                  JOIN QuyetDinh b ON b.SoQD = kt.SoQD
                  WHERE kt.DoiTuong = @p AND MONTH(b.NgayLap) = @t AND YEAR(b.NgayLap) = @n",
                SqlHelper.P("@p", maNV), SqlHelper.P("@t", thang), SqlHelper.P("@n", nam));
            return Convert.ToDecimal(v);
        }

        private decimal GetTienPhat(string maNV, int thang, int nam)
        {
            object v = SqlHelper.ExecuteScalar(
                @"SELECT ISNULL(SUM(kl.SoTienPhat), 0)
                  FROM QD_KyLuat kl
                  JOIN QuyetDinh b ON b.SoQD = kl.SoQD
                  WHERE kl.DoiTuong = @p AND MONTH(b.NgayLap) = @t AND YEAR(b.NgayLap) = @n",
                SqlHelper.P("@p", maNV), SqlHelper.P("@t", thang), SqlHelper.P("@n", nam));
            return Convert.ToDecimal(v);
        }

        private decimal GetBaoHiem(string maNV, string loaiBH)
        {
            object v = SqlHelper.ExecuteScalar(
                @"SELECT ISNULL(SUM(bh.MucDong), 0)
                  FROM BaoHiem bh
                  JOIN HopDong hd ON hd.SoHD = bh.SoHD
                  WHERE hd.MaNV = @p AND bh.LoaiBH = @loai",
                SqlHelper.P("@p", maNV), SqlHelper.P("@loai", loaiBH));
            return Convert.ToDecimal(v);
        }
    }
}