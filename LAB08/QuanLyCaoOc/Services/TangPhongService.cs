using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Services
{
    public class TangPhongService
    {
        public DataTable LayTang()
        {
            return Db.Query("SELECT * FROM Tang ORDER BY SoTang");
        }

        public DataTable LayPhong()
        {
            return Db.Query(@"SELECT p.MaPhong, t.TenTang, p.DienTich, p.HuongPhong, p.DonGiaThang, p.TrangThai, p.MaTang
                              FROM Phong p JOIN Tang t ON p.MaTang=t.MaTang
                              ORDER BY t.SoTang, p.MaPhong");
        }

        /// <summary>Phong khong bi hop dong nao chong lan trong khoang [nhan, ketThuc].</summary>
        public DataTable LayPhongTrong(DateTime nhan, DateTime ketThuc)
        {
            return Db.Query(@"SELECT p.MaPhong, t.TenTang, p.DienTich, p.HuongPhong, p.DonGiaThang
                              FROM Phong p JOIN Tang t ON p.MaTang=t.MaTang
                              WHERE p.TrangThai<>N'Bảo trì'
                                AND NOT EXISTS(SELECT 1 FROM ChiTietHopDong c
                                               WHERE c.MaPhong=p.MaPhong
                                                 AND @nhan<=c.NgayKetThuc AND @kt>=c.NgayBatDau)
                              ORDER BY t.SoTang, p.MaPhong",
                new SqlParameter("@nhan", nhan.Date), new SqlParameter("@kt", ketThuc.Date));
        }

        public KetQuaXuLy ThemTang(string ma, string ten, int soTang, decimal doCao)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten))
                return KetQuaXuLy.Fail("Thông tin tầng chưa đầy đủ.");
            if (soTang <= 0 || doCao < 0) return KetQuaXuLy.Fail("Số tầng phải > 0 và độ cao không âm.");
            try
            {
                Db.Execute("INSERT INTO Tang VALUES(@m,@t,@s,@c)",
                    new SqlParameter("@m", ma), new SqlParameter("@t", ten),
                    new SqlParameter("@s", soTang), new SqlParameter("@c", doCao));
                return KetQuaXuLy.Ok("Đã lưu tầng.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy ThemPhong(string ma, string maTang, decimal dienTich, string huong, decimal donGia)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(maTang) || string.IsNullOrWhiteSpace(huong))
                return KetQuaXuLy.Fail("Thông tin phòng chưa đầy đủ.");
            if (dienTich <= 0) return KetQuaXuLy.Fail("Diện tích phải lớn hơn 0.");
            if (donGia < 0) return KetQuaXuLy.Fail("Đơn giá không được âm.");
            try
            {
                Db.Execute("INSERT INTO Phong(MaPhong,MaTang,DienTich,HuongPhong,DonGiaThang) VALUES(@m,@t,@d,@h,@g)",
                    new SqlParameter("@m", ma), new SqlParameter("@t", maTang), new SqlParameter("@d", dienTich),
                    new SqlParameter("@h", huong), new SqlParameter("@g", donGia));
                return KetQuaXuLy.Ok("Đã lưu phòng.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        /// <summary>Doi gia chi ap dung cho hop dong moi; hop dong cu giu gia da chot.</summary>
        public KetQuaXuLy CapNhatDonGia(string maPhong, decimal donGia)
        {
            if (string.IsNullOrWhiteSpace(maPhong)) return KetQuaXuLy.Fail("Chưa chọn phòng.");
            if (donGia < 0) return KetQuaXuLy.Fail("Đơn giá không được âm.");
            int n = Db.Execute("UPDATE Phong SET DonGiaThang=@g WHERE MaPhong=@m",
                new SqlParameter("@g", donGia), new SqlParameter("@m", maPhong));
            return n > 0 ? KetQuaXuLy.Ok("Đã cập nhật đơn giá phòng.") : KetQuaXuLy.Fail("Không tìm thấy phòng.");
        }

        public KetQuaXuLy DatBaoTri(string maPhong, bool baoTri)
        {
            object dangThue = Db.Scalar(@"SELECT COUNT(*) FROM ChiTietHopDong
                                          WHERE MaPhong=@m AND NgayKetThuc>=CAST(GETDATE() AS date)",
                new SqlParameter("@m", maPhong));
            if (baoTri && Convert.ToInt32(dangThue) > 0)
                return KetQuaXuLy.Fail("Phòng đang có hợp đồng còn hiệu lực/chưa đến hạn, không thể đặt bảo trì.");
            Db.Execute("UPDATE Phong SET TrangThai=@t WHERE MaPhong=@m",
                new SqlParameter("@t", baoTri ? "Bảo trì" : "Trống"), new SqlParameter("@m", maPhong));
            return KetQuaXuLy.Ok("Đã cập nhật trạng thái phòng.");
        }
    }
}
