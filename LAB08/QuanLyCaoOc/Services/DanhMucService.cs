using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCaoOc.Data;

namespace QuanLyCaoOc.Services
{
    public class DanhMucService
    {
        public DataTable LayNhanVien()
        {
            return Db.Query("SELECT * FROM NhanVien ORDER BY MaNV");
        }

        public DataTable LayDichVu()
        {
            return Db.Query("SELECT * FROM DichVu ORDER BY MaDV");
        }

        public DataTable LayQuyDinhPhat()
        {
            return Db.Query("SELECT * FROM QuyDinhPhat ORDER BY MaQuyDinh");
        }

        public KetQuaXuLy ThemNhanVien(string ma, string ten, string vaiTro, string sdt)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(vaiTro))
                return KetQuaXuLy.Fail("Thông tin nhân viên chưa đầy đủ.");
            try
            {
                Db.Execute("INSERT INTO NhanVien VALUES(@m,@t,@v,@s)",
                    new SqlParameter("@m", ma), new SqlParameter("@t", ten),
                    new SqlParameter("@v", vaiTro), new SqlParameter("@s", (object)sdt ?? DBNull.Value));
                return KetQuaXuLy.Ok("Đã lưu nhân viên.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy ThemDichVu(string ma, string ten, string dvt, decimal gia)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(dvt))
                return KetQuaXuLy.Fail("Thông tin dịch vụ chưa đầy đủ.");
            if (gia < 0) return KetQuaXuLy.Fail("Đơn giá không được âm.");
            try
            {
                Db.Execute("INSERT INTO DichVu VALUES(@m,@t,@d,@g)",
                    new SqlParameter("@m", ma), new SqlParameter("@t", ten),
                    new SqlParameter("@d", dvt), new SqlParameter("@g", gia));
                return KetQuaXuLy.Ok("Đã lưu dịch vụ.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy CapNhatGiaDichVu(string ma, decimal gia)
        {
            if (string.IsNullOrWhiteSpace(ma)) return KetQuaXuLy.Fail("Chưa chọn dịch vụ.");
            if (gia < 0) return KetQuaXuLy.Fail("Đơn giá không được âm.");
            int n = Db.Execute("UPDATE DichVu SET DonGia=@g WHERE MaDV=@m",
                new SqlParameter("@g", gia), new SqlParameter("@m", ma));
            return n > 0 ? KetQuaXuLy.Ok("Đã cập nhật đơn giá (hóa đơn cũ giữ nguyên giá đã chốt).")
                         : KetQuaXuLy.Fail("Không tìm thấy dịch vụ.");
        }

        public KetQuaXuLy ThemQuyDinhPhat(string ma, string noiDung, decimal muc)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(noiDung))
                return KetQuaXuLy.Fail("Thông tin quy định chưa đầy đủ.");
            if (muc <= 0) return KetQuaXuLy.Fail("Mức phạt phải lớn hơn 0.");
            try
            {
                Db.Execute("INSERT INTO QuyDinhPhat VALUES(@m,@n,@p)",
                    new SqlParameter("@m", ma), new SqlParameter("@n", noiDung), new SqlParameter("@p", muc));
                return KetQuaXuLy.Ok("Đã lưu quy định phạt.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }
    }
}
