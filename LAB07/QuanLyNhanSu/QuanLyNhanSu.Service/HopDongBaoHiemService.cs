using System;
using System.Collections.Generic;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Service
{
    public static class LoaiHopDong
    {
        public const string DaiHan = "Dài hạn";
        public const string NganHan = "Ngắn hạn";
    }

    public class HopDongService : BaseService<HopDong>
    {
        public System.Collections.Generic.List<HopDong> GetDanhSach()
        {
            return SqlHelper.ExecuteList<HopDong>(
                @"SELECT hd.SoHD, hd.LoaiHD, hd.NgayKy, hd.NgayBatDau, hd.NgayKetThuc, hd.MaNV, nv.HoTen
                  FROM HopDong hd
                  LEFT JOIN NhanVien nv ON nv.MaNV = hd.MaNV
                  ORDER BY hd.NgayKy DESC");
        }

        /// <summary>
        /// Ký hợp đồng chính thức cho nhân viên. Với hợp đồng dài hạn,
        /// hệ thống tự lập hồ sơ BHYT + BHXH (mức đóng lấy từ tham số).
        /// </summary>
        public void KyHopDongChinhThuc(HopDong hd, decimal mucDongBHYT, decimal mucDongBHXH)
        {
            if (hd == null) throw new ArgumentNullException(nameof(hd));
            if (string.IsNullOrWhiteSpace(hd.LoaiHD))
                hd.LoaiHD = LoaiHopDong.NganHan;

            var commands = new List<SqlCommandSpec>
            {
                new SqlCommandSpec(
                    @"INSERT INTO HopDong (SoHD, LoaiHD, NgayKy, NgayBatDau, NgayKetThuc, MaNV)
                      VALUES (@SoHD, @LoaiHD, @NgayKy, @NgayBatDau, @NgayKetThuc, @MaNV)",
                    new[]
                    {
                        SqlHelper.P("@SoHD", hd.SoHD),
                        SqlHelper.P("@LoaiHD", hd.LoaiHD),
                        SqlHelper.P("@NgayKy", hd.NgayKy),
                        SqlHelper.P("@NgayBatDau", hd.NgayBatDau),
                        SqlHelper.P("@NgayKetThuc", (object)hd.NgayKetThuc ?? DBNull.Value),
                        SqlHelper.P("@MaNV", hd.MaNV)
                    })
            };

            if (hd.LoaiHD == LoaiHopDong.DaiHan)
            {
                commands.Add(new SqlCommandSpec(
                    @"INSERT INTO BaoHiem (MaBH, LoaiBH, SoSo, MucDong, NgayBatDau, SoHD)
                      VALUES (@MaBH, 'BHYT', @SoSo, @MucDong, @NgayBatDau, @SoHD)",
                    new[]
                    {
                        SqlHelper.P("@MaBH", "BH/" + hd.SoHD + "/BHYT"),
                        SqlHelper.P("@SoSo", (object)null),
                        SqlHelper.P("@MucDong", mucDongBHYT),
                        SqlHelper.P("@NgayBatDau", hd.NgayBatDau),
                        SqlHelper.P("@SoHD", hd.SoHD)
                    }));
                commands.Add(new SqlCommandSpec(
                    @"INSERT INTO BaoHiem (MaBH, LoaiBH, SoSo, MucDong, NgayBatDau, SoHD)
                      VALUES (@MaBH, 'BHXH', @SoSo, @MucDong, @NgayBatDau, @SoHD)",
                    new[]
                    {
                        SqlHelper.P("@MaBH", "BH/" + hd.SoHD + "/BHXH"),
                        SqlHelper.P("@SoSo", (object)null),
                        SqlHelper.P("@MucDong", mucDongBHXH),
                        SqlHelper.P("@NgayBatDau", hd.NgayBatDau),
                        SqlHelper.P("@SoHD", hd.SoHD)
                    }));
            }

            SqlHelper.ExecuteTransaction(commands);
        }
    }

    public class BaoHiemService : BaseService<BaoHiem>
    {
        public System.Collections.Generic.List<BaoHiem> GetDanhSach()
        {
            return SqlHelper.ExecuteList<BaoHiem>(
                @"SELECT bh.MaBH, bh.LoaiBH, bh.SoSo, bh.MucDong, bh.NgayBatDau, bh.SoHD, nv.HoTen
                  FROM BaoHiem bh
                  JOIN HopDong hd ON hd.SoHD = bh.SoHD
                  LEFT JOIN NhanVien nv ON nv.MaNV = hd.MaNV
                  ORDER BY bh.MaBH");
        }
    }
}