using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using eShoppingPrototype.Models;

namespace eShoppingPrototype.Data
{
    public class DonHangDAO
    {
        private readonly string _connectionString;

        public DonHangDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["eShoppingDB"].ConnectionString;
        }

        public int InsertDonHang(DonHang donHang)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert NguoiNhan
                    int maNguoiNhan = InsertNguoiNhan(conn, transaction, donHang.NguoiNhan);
                    
                    // Insert DonHang
                    int maDonHang = InsertDonHangMain(conn, transaction, donHang, maNguoiNhan);
                    
                    // Insert ChiTietDonHang
                    InsertChiTietDonHang(conn, transaction, maDonHang, donHang.ChiTietDonHang);
                    
                    transaction.Commit();
                    return maDonHang;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private int InsertNguoiNhan(SqlConnection conn, SqlTransaction trans, NguoiNhan nguoiNhan)
        {
            string sql = @"
                INSERT INTO NguoiNhan (HoTen, DiaChi, DienThoai, MaKhuVuc) 
                VALUES (@HoTen, @DiaChi, @DienThoai, @MaKhuVuc);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@HoTen", nguoiNhan.HoTen);
                cmd.Parameters.AddWithValue("@DiaChi", nguoiNhan.DiaChi);
                cmd.Parameters.AddWithValue("@DienThoai", nguoiNhan.DienThoai);
                cmd.Parameters.AddWithValue("@MaKhuVuc", nguoiNhan.MaKhuVuc);

                return (int)cmd.ExecuteScalar();
            }
        }

        private int InsertDonHangMain(SqlConnection conn, SqlTransaction trans, DonHang donHang, int maNguoiNhan)
        {
            string sql = @"
                INSERT INTO DonHang 
                (MaKH, MaNguoiNhan, LoaiGiaoHang, PhiGiaoHang, 
                 LoaiThe, SoThe, PhiThanhToan,
                 TongTienHang, TongTriGiaHoaDon)
                VALUES 
                (@MaKH, @MaNguoiNhan, @LoaiGiaoHang, @PhiGiaoHang,
                 @LoaiThe, @SoThe, @PhiThanhToan,
                 @TongTienHang, @TongTriGiaHoaDon);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
            {
                cmd.Parameters.AddWithValue("@MaKH", donHang.MaKH);
                cmd.Parameters.AddWithValue("@MaNguoiNhan", maNguoiNhan);
                cmd.Parameters.AddWithValue("@LoaiGiaoHang", donHang.LoaiGiaoHang ?? "Thường");
                cmd.Parameters.AddWithValue("@PhiGiaoHang", donHang.PhiGiaoHang);
                cmd.Parameters.AddWithValue("@LoaiThe", donHang.LoaiThe ?? "VISA");
                cmd.Parameters.AddWithValue("@SoThe", donHang.TheTinDung != null ? donHang.TheTinDung.SoThe : "");
                cmd.Parameters.AddWithValue("@PhiThanhToan", donHang.LePhi);
                cmd.Parameters.AddWithValue("@TongTienHang", donHang.TongTriGiaSanPham);
                cmd.Parameters.AddWithValue("@TongTriGiaHoaDon", donHang.TongTriGiaHoaDon);

                return (int)cmd.ExecuteScalar();
            }
        }

        private void InsertChiTietDonHang(SqlConnection conn, SqlTransaction trans, int maDonHang, 
            List<ChiTietDonHang> chiTietList)
        {
            string sql = @"
                INSERT INTO ChiTietDonHang (MaDonHang, MaSP, SoLuong, DonGia, ThanhTien)
                VALUES (@MaDonHang, @MaSP, @SoLuong, @DonGia, @ThanhTien);";

            foreach (var chiTiet in chiTietList)
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn, trans))
                {
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    cmd.Parameters.AddWithValue("@MaSP", chiTiet.MaSP);
                    cmd.Parameters.AddWithValue("@SoLuong", chiTiet.SoLuong);
                    cmd.Parameters.AddWithValue("@DonGia", chiTiet.DonGia);
                    cmd.Parameters.AddWithValue("@ThanhTien", chiTiet.ThanhTien);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
