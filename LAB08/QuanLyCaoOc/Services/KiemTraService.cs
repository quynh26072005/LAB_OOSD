using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Services
{
    public class KiemTraService
    {
        public DataTable LayPhongDenHan()
        {
            // BR12: moi phong kiem tra 1 nam/lan; chua tung kiem tra cung tinh den han
            return Db.Query(@"SELECT p.MaPhong, t.TenTang, MAX(d.NgayKiemTra) AS LanKiemTraCuoi,
                                     CASE WHEN MAX(d.NgayKiemTra) IS NULL THEN N'Chưa kiểm tra lần nào'
                                          ELSE CONVERT(nvarchar(10),DATEADD(YEAR,1,MAX(d.NgayKiemTra)),103) END AS HanKiemTra
                              FROM Phong p
                              JOIN Tang t ON p.MaTang=t.MaTang
                              LEFT JOIN ChiTietKiemTra c ON c.MaPhong=p.MaPhong
                              LEFT JOIN DotKiemTra d ON d.MaDot=c.MaDot
                              GROUP BY p.MaPhong, t.TenTang, t.SoTang
                              HAVING MAX(d.NgayKiemTra) IS NULL OR DATEADD(YEAR,1,MAX(d.NgayKiemTra))<=CAST(GETDATE() AS date)
                              ORDER BY t.SoTang, p.MaPhong");
        }

        public DataTable LayDot()
        {
            return Db.Query("SELECT MaDot, NgayKiemTra, GhiChu, MaNV FROM DotKiemTra ORDER BY NgayKiemTra DESC, MaDot DESC");
        }

        public DataTable LayChiTiet(string maDot)
        {
            return Db.Query(@"SELECT MaPhong, TinhTrang, ThayDoiKetCau, KeHoachTuSua
                              FROM ChiTietKiemTra WHERE MaDot=@d ORDER BY MaPhong",
                new SqlParameter("@d", maDot));
        }

        public KetQuaXuLy LapDotKiemTra(string maDot, DateTime ngay, string ghiChu, string maNV, List<KiemTraPhongItem> ds)
        {
            if (string.IsNullOrWhiteSpace(maDot) || string.IsNullOrWhiteSpace(maNV) || ds == null || ds.Count == 0)
                return KetQuaXuLy.Fail("Đợt kiểm tra chưa đủ thông tin hoặc chưa ghi nhận phòng nào.");
            foreach (var x in ds)
                if (string.IsNullOrWhiteSpace(x.TinhTrang))
                    return KetQuaXuLy.Fail("Phòng " + x.MaPhong + " chưa ghi tình trạng.");
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var h = new SqlCommand("INSERT INTO DotKiemTra VALUES(@d,@n,@g,@nv)", cn, tx);
                    h.Parameters.AddWithValue("@d", maDot);
                    h.Parameters.AddWithValue("@n", ngay.Date);
                    h.Parameters.AddWithValue("@g", (object)ghiChu ?? DBNull.Value);
                    h.Parameters.AddWithValue("@nv", maNV);
                    h.ExecuteNonQuery();
                    foreach (var x in ds)
                    {
                        var c = new SqlCommand("INSERT INTO ChiTietKiemTra VALUES(@d,@p,@t,@k,@s)", cn, tx);
                        c.Parameters.AddWithValue("@d", maDot);
                        c.Parameters.AddWithValue("@p", x.MaPhong);
                        c.Parameters.AddWithValue("@t", x.TinhTrang);
                        c.Parameters.AddWithValue("@k", x.ThayDoiKetCau);
                        c.Parameters.AddWithValue("@s", string.IsNullOrWhiteSpace(x.KeHoachTuSua) ? (object)DBNull.Value : x.KeHoachTuSua);
                        c.ExecuteNonQuery();
                    }
                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã lưu đợt kiểm tra " + maDot + ".");
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        public DataTable LayBienBan()
        {
            return Db.Query(@"SELECT b.SoBienBan, b.MaDot, b.MaPhong, q.NoiDung, k.TenKhach, b.NgayLap, b.SoTien, b.TrangThai
                              FROM BienBanPhat b
                              JOIN QuyDinhPhat q ON b.MaQuyDinh=q.MaQuyDinh
                              JOIN KhachHang k ON b.MaKhach=k.MaKhach
                              ORDER BY b.NgayLap DESC, b.SoBienBan DESC");
        }

        /// <summary>BR13: phat phong tu y thay doi ket cau theo quy dinh cua cao oc.</summary>
        public KetQuaXuLy LapBienBanPhat(string soBB, string maDot, string maPhong, string maQuyDinh, string maNV)
        {
            if (string.IsNullOrWhiteSpace(soBB) || string.IsNullOrWhiteSpace(maDot) || string.IsNullOrWhiteSpace(maPhong) ||
                string.IsNullOrWhiteSpace(maQuyDinh) || string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail("Biên bản phạt chưa đủ thông tin.");

            DataTable ct = Db.Query(@"SELECT c.ThayDoiKetCau, d.NgayKiemTra FROM ChiTietKiemTra c
                                      JOIN DotKiemTra d ON c.MaDot=d.MaDot WHERE c.MaDot=@d AND c.MaPhong=@p",
                new SqlParameter("@d", maDot), new SqlParameter("@p", maPhong));
            if (ct.Rows.Count == 0) return KetQuaXuLy.Fail("Phòng chưa được ghi nhận trong đợt kiểm tra này.");
            if (!Convert.ToBoolean(ct.Rows[0]["ThayDoiKetCau"]))
                return KetQuaXuLy.Fail("Phòng không bị ghi nhận thay đổi kết cấu, không thể lập biên bản phạt.");
            DateTime ngayKT = Convert.ToDateTime(ct.Rows[0]["NgayKiemTra"]);

            object muc = Db.Scalar("SELECT MucPhat FROM QuyDinhPhat WHERE MaQuyDinh=@q", new SqlParameter("@q", maQuyDinh));
            if (muc == null) return KetQuaXuLy.Fail("Không tìm thấy quy định phạt.");

            object khach = Db.Scalar(@"SELECT TOP 1 h.MaKhach FROM ChiTietHopDong c JOIN HopDong h ON c.SoHopDong=h.SoHopDong
                                       WHERE c.MaPhong=@p AND c.NgayBatDau<=@n AND c.NgayKetThuc>=@n",
                new SqlParameter("@p", maPhong), new SqlParameter("@n", ngayKT));
            if (khach == null) return KetQuaXuLy.Fail("Không xác định được khách đang thuê phòng vào ngày kiểm tra.");
            try
            {
                Db.Execute(@"INSERT INTO BienBanPhat(SoBienBan,MaDot,MaPhong,MaQuyDinh,MaKhach,NgayLap,SoTien,MaNV)
                             VALUES(@b,@d,@p,@q,@k,CAST(GETDATE() AS date),@t,@nv)",
                    new SqlParameter("@b", soBB), new SqlParameter("@d", maDot), new SqlParameter("@p", maPhong),
                    new SqlParameter("@q", maQuyDinh), new SqlParameter("@k", Convert.ToString(khach)),
                    new SqlParameter("@t", Convert.ToDecimal(muc)), new SqlParameter("@nv", maNV));
                return KetQuaXuLy.Ok("Đã lập biên bản phạt " + soBB + " - " + Convert.ToDecimal(muc).ToString("N0") + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy NopPhat(string soBB)
        {
            int n = Db.Execute(@"UPDATE BienBanPhat SET TrangThai=N'Đã nộp', NgayNop=CAST(GETDATE() AS date)
                                 WHERE SoBienBan=@b AND TrangThai=N'Chưa nộp'", new SqlParameter("@b", soBB));
            return n > 0 ? KetQuaXuLy.Ok("Đã ghi nhận nộp phạt.") : KetQuaXuLy.Fail("Biên bản không tồn tại hoặc đã nộp.");
        }
    }
}
