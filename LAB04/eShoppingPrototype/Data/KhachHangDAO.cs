using System;
using System.Configuration;
using System.Data.SqlClient;
using eShoppingPrototype.Models;

namespace eShoppingPrototype.Data
{
    public class KhachHangDAO
    {
        private readonly string _connectionString;

        public KhachHangDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["eShoppingDB"].ConnectionString;
        }

        public KhachHang Login(string tenDangNhap, string matKhau)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT MaKH, HoTen, DienThoai, Email, DiaChi, TenDangNhap, MatKhau
                    FROM KhachHang
                    WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new KhachHang
                            {
                                MaKH = reader.GetInt32(0),
                                HoTen = reader.GetString(1),
                                DienThoai = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                DiaChi = reader.IsDBNull(4) ? null : reader.GetString(4),
                                TenDangNhap = reader.GetString(5),
                                MatKhau = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public int RegisterCustomer(KhachHang khachHang)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    INSERT INTO KhachHang (HoTen, DienThoai, Email, DiaChi, TenDangNhap, MatKhau)
                    VALUES (@HoTen, @DienThoai, @Email, @DiaChi, @TenDangNhap, @MatKhau);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", khachHang.HoTen);
                    cmd.Parameters.AddWithValue("@DienThoai", (object)khachHang.DienThoai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)khachHang.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiaChi", (object)khachHang.DiaChi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenDangNhap", khachHang.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", khachHang.MatKhau);

                    conn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public bool IsUsernameExists(string tenDangNhap)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT COUNT(*) FROM KhachHang WHERE TenDangNhap = @TenDangNhap";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    conn.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public KhachHang GetCustomerById(int maKH)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT MaKH, HoTen, DienThoai, Email, DiaChi, TenDangNhap, MatKhau
                    FROM KhachHang
                    WHERE MaKH = @MaKH";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new KhachHang
                            {
                                MaKH = reader.GetInt32(0),
                                HoTen = reader.GetString(1),
                                DienThoai = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                DiaChi = reader.IsDBNull(4) ? null : reader.GetString(4),
                                TenDangNhap = reader.GetString(5),
                                MatKhau = reader.GetString(6)
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
