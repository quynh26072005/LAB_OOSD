using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Service
{
    public class NhanVienService : BaseService<NhanVien>
    {
        private const string SelectView =
            @"SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.DiaChi, nv.SDT, nv.CCCD,
                     nv.MaPB, pb.TenPB, nv.MaCV, cv.TenCV
              FROM NhanVien nv
              LEFT JOIN PhongBan pb ON pb.MaPB = nv.MaPB
              LEFT JOIN ChucVu   cv ON cv.MaCV = nv.MaCV";

        /// <summary>Danh sách nhân viên kèm tên phòng ban, chức vụ (đã join).</summary>
        public List<NhanVien> GetNhanVienView()
        {
            return SqlHelper.ExecuteList<NhanVien>(SelectView + " ORDER BY nv.MaNV");
        }

        public List<NhanVien> TimKiem(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
                return GetNhanVienView();

            string sql = SelectView +
                         " WHERE nv.MaNV LIKE @k OR nv.HoTen LIKE @k OR pb.TenPB LIKE @k";
            return SqlHelper.ExecuteList<NhanVien>(sql, SqlHelper.P("@k", "%" + tuKhoa.Trim() + "%"));
        }
    }
}