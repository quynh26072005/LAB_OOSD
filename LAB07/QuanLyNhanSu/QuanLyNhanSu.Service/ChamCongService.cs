using System.Collections.Generic;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Service
{
    public class KeHoachPhanCongService : BaseService<KeHoachPhanCong>
    {
        public List<KeHoachPhanCong> GetDanhSach()
        {
            return SqlHelper.ExecuteList<KeHoachPhanCong>(
                @"SELECT kh.MaKH, kh.Tuan, kh.MaPB, pb.TenPB, kh.NguoiLap
                  FROM KeHoachPhanCong kh
                  LEFT JOIN PhongBan pb ON pb.MaPB = kh.MaPB
                  ORDER BY kh.MaKH");
        }
    }

    public class ChiTietPhanCongService : BaseService<ChiTietPhanCong>
    {
        public List<ChiTietPhanCong> GetByKeHoach(string maKH)
        {
            string sql =
                @"SELECT ct.MaKH, ct.MaNV, nv.HoTen, ct.Ngay, ct.MaCa, ca.TenCa, ct.CoPhuTroi
                  FROM ChiTietPhanCong ct
                  LEFT JOIN NhanVien  nv ON nv.MaNV = ct.MaNV
                  LEFT JOIN CaLamViec ca ON ca.MaCa = ct.MaCa
                  WHERE ct.MaKH = @p ORDER BY ct.Ngay, ct.MaNV";
            return SqlHelper.ExecuteList<ChiTietPhanCong>(sql, SqlHelper.P("@p", maKH));
        }
    }

    public class BangChamCongService : BaseService<BangChamCong>
    {
        public List<BangChamCong> GetDanhSach()
        {
            return SqlHelper.ExecuteList<BangChamCong>(
                @"SELECT cc.MaCC, cc.Thang, cc.Nam, cc.MaPB, pb.TenPB
                  FROM BangChamCong cc
                  LEFT JOIN PhongBan pb ON pb.MaPB = cc.MaPB
                  ORDER BY cc.Nam DESC, cc.Thang DESC");
        }
    }

    public class ChiTietChamCongService : BaseService<ChiTietChamCong>
    {
        public List<ChiTietChamCong> GetByBang(string maCC)
        {
            string sql =
                @"SELECT ct.MaCC, ct.MaNV, nv.HoTen, ct.Ngay, ct.MaCa, ca.TenCa, ct.TrangThai
                  FROM ChiTietChamCong ct
                  LEFT JOIN NhanVien  nv ON nv.MaNV = ct.MaNV
                  LEFT JOIN CaLamViec ca ON ca.MaCa = ct.MaCa
                  WHERE ct.MaCC = @p ORDER BY ct.Ngay, ct.MaNV";
            return SqlHelper.ExecuteList<ChiTietChamCong>(sql, SqlHelper.P("@p", maCC));
        }
    }

    public class PhieuPhuTroiService : BaseService<PhieuPhuTroi> { }

    public class ChiTietPhuTroiService : BaseService<ChiTietPhuTroi>
    {
        public List<ChiTietPhuTroi> GetByPhieu(string soPhieu)
        {
            string sql =
                @"SELECT ct.SoPhieu, ct.MaNV, nv.HoTen, ct.SoGio
                  FROM ChiTietPhuTroi ct
                  LEFT JOIN NhanVien nv ON nv.MaNV = ct.MaNV
                  WHERE ct.SoPhieu = @p ORDER BY ct.MaNV";
            return SqlHelper.ExecuteList<ChiTietPhuTroi>(sql, SqlHelper.P("@p", soPhieu));
        }
    }
}