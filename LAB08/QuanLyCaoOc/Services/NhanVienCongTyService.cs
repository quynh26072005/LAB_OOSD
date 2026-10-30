using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Services
{
    public class NhanVienCongTyService
    {
        public DataTable LayNhanVienCongTy()
        {
            return Db.Query(@"SELECT n.MaNVCT, n.HoTen, k.TenKhach AS CongTy, n.ChucVu, n.MaPhong, t.TenTang,
                                     n.NgayVaoLam, n.NgayBaoCao, n.TrangThai, n.HinhAnh, n.MaKhach
                              FROM NhanVienCongTy n
                              JOIN KhachHang k ON n.MaKhach=k.MaKhach
                              JOIN Phong p ON n.MaPhong=p.MaPhong
                              JOIN Tang t ON p.MaTang=t.MaTang
                              ORDER BY n.MaNVCT");
        }

        /// <summary>Cac phong cong ty dang thue (hop dong con hieu luc tai ngay hien tai).</summary>
        public DataTable LayPhongDangThue(string maKhach)
        {
            return Db.Query(@"SELECT DISTINCT c.MaPhong, t.TenTang
                              FROM ChiTietHopDong c
                              JOIN HopDong h ON c.SoHopDong=h.SoHopDong
                              JOIN Phong p ON c.MaPhong=p.MaPhong
                              JOIN Tang t ON p.MaTang=t.MaTang
                              WHERE h.MaKhach=@k AND c.NgayBatDau<=CAST(GETDATE() AS date)
                                AND c.NgayKetThuc>=CAST(GETDATE() AS date)
                              ORDER BY c.MaPhong",
                new SqlParameter("@k", maKhach));
        }

        public KetQuaXuLy KhaiBaoNhanVien(string ma, string hoTen, string hinhAnh, string chucVu,
                                          string maKhach, string maPhong, DateTime ngayVao, string maNV)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(chucVu) ||
                string.IsNullOrWhiteSpace(maKhach) || string.IsNullOrWhiteSpace(maPhong) || string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail("Thông tin nhân viên công ty chưa đầy đủ.");

            object ok = Db.Scalar(@"SELECT COUNT(*) FROM ChiTietHopDong c JOIN HopDong h ON c.SoHopDong=h.SoHopDong
                                    WHERE h.MaKhach=@k AND c.MaPhong=@p
                                      AND c.NgayBatDau<=CAST(GETDATE() AS date) AND c.NgayKetThuc>=CAST(GETDATE() AS date)",
                new SqlParameter("@k", maKhach), new SqlParameter("@p", maPhong));
            if (Convert.ToInt32(ok) == 0)
                return KetQuaXuLy.Fail("Công ty không có hợp đồng còn hiệu lực với phòng " + maPhong + ".");
            try
            {
                Db.Execute(@"INSERT INTO NhanVienCongTy(MaNVCT,HoTen,HinhAnh,ChucVu,MaKhach,MaPhong,NgayVaoLam,NgayBaoCao,MaNV)
                             VALUES(@m,@t,@a,@c,@k,@p,@nv,CAST(GETDATE() AS date),@n)",
                    new SqlParameter("@m", ma), new SqlParameter("@t", hoTen),
                    new SqlParameter("@a", string.IsNullOrWhiteSpace(hinhAnh) ? (object)DBNull.Value : hinhAnh),
                    new SqlParameter("@c", chucVu), new SqlParameter("@k", maKhach), new SqlParameter("@p", maPhong),
                    new SqlParameter("@nv", ngayVao.Date), new SqlParameter("@n", maNV));
                return KetQuaXuLy.Ok("Đã tiếp nhận báo cáo nhân viên mới.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy DanhDauNghi(string maNVCT)
        {
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var a = new SqlCommand("UPDATE NhanVienCongTy SET TrangThai=N'Đã nghỉ' WHERE MaNVCT=@m", cn, tx);
                    a.Parameters.AddWithValue("@m", maNVCT);
                    if (a.ExecuteNonQuery() == 0) { tx.Rollback(); return KetQuaXuLy.Fail("Không tìm thấy nhân viên."); }
                    var b = new SqlCommand("UPDATE TheGiuXe SET TrangThai=N'Đã thu hồi' WHERE MaNVCT=@m", cn, tx);
                    b.Parameters.AddWithValue("@m", maNVCT);
                    b.ExecuteNonQuery();
                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã cập nhật nghỉ việc và thu hồi thẻ giữ xe (nếu có).");
                }
                catch (Exception ex)
                {
                    try { tx.Rollback(); } catch { }
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        public DataTable LayTheGiuXe()
        {
            return Db.Query(@"SELECT g.MaThe, g.MaNVCT, n.HoTen, g.LoaiXe, g.BienSo, g.NhanHieu, g.MauSac, g.NgayCap, g.TrangThai
                              FROM TheGiuXe g JOIN NhanVienCongTy n ON g.MaNVCT=n.MaNVCT ORDER BY g.MaThe");
        }

        public KetQuaXuLy CapThe(string maThe, string maNVCT, string loaiXe, string bienSo,
                                 string nhanHieu, string mauSac, string maNV)
        {
            if (string.IsNullOrWhiteSpace(maThe) || string.IsNullOrWhiteSpace(maNVCT) ||
                string.IsNullOrWhiteSpace(loaiXe) || string.IsNullOrWhiteSpace(bienSo) || string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail("Thông tin thẻ giữ xe chưa đầy đủ.");
            object tt = Db.Scalar("SELECT TrangThai FROM NhanVienCongTy WHERE MaNVCT=@m", new SqlParameter("@m", maNVCT));
            if (tt == null) return KetQuaXuLy.Fail("Không tìm thấy nhân viên công ty.");
            if (Convert.ToString(tt) != "Đang làm") return KetQuaXuLy.Fail("Nhân viên đã nghỉ, không cấp thẻ.");
            try
            {
                Db.Execute(@"INSERT INTO TheGiuXe(MaThe,MaNVCT,NgayCap,LoaiXe,BienSo,NhanHieu,MauSac,MaNV)
                             VALUES(@t,@n,CAST(GETDATE() AS date),@l,@b,@h,@c,@nv)",
                    new SqlParameter("@t", maThe), new SqlParameter("@n", maNVCT), new SqlParameter("@l", loaiXe),
                    new SqlParameter("@b", bienSo), new SqlParameter("@h", (object)nhanHieu ?? DBNull.Value),
                    new SqlParameter("@c", (object)mauSac ?? DBNull.Value), new SqlParameter("@nv", maNV));
                return KetQuaXuLy.Ok("Đã cấp thẻ giữ xe " + maThe + ".");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    return KetQuaXuLy.Fail("Nhân viên đã có thẻ hoặc biển số đã được đăng ký.");
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        public KetQuaXuLy ThuHoiThe(string maThe)
        {
            int n = Db.Execute("UPDATE TheGiuXe SET TrangThai=N'Đã thu hồi' WHERE MaThe=@m", new SqlParameter("@m", maThe));
            return n > 0 ? KetQuaXuLy.Ok("Đã thu hồi thẻ.") : KetQuaXuLy.Fail("Không tìm thấy thẻ.");
        }
    }
}
