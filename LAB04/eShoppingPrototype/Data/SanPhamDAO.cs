using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using eShoppingPrototype.Models;

namespace eShoppingPrototype.Data
{
    public class SanPhamDAO
    {
        private readonly string _connectionString;

        public SanPhamDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["eShoppingDB"].ConnectionString;
        }

        public List<NhomSanPham> GetAllCategories()
        {
            var categories = new List<NhomSanPham>();

            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT MaNhom, TenNhom, MoTa FROM NhomSanPham ORDER BY TenNhom";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new NhomSanPham
                            {
                                MaNhom = reader.GetInt32(0),
                                TenNhom = reader.GetString(1),
                                MoTa = reader.IsDBNull(2) ? null : reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return categories;
        }

        public List<SanPham> GetProductsByCategory(int maNhom)
        {
            var products = new List<SanPham>();

            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT sp.MaSP, sp.TenSP, sp.MaNhom, sp.NhaSanXuat, sp.GiaBan, 
                           sp.TinhTrang, sp.MoTa, sp.HinhAnh, n.TenNhom
                    FROM SanPham sp
                    INNER JOIN NhomSanPham n ON sp.MaNhom = n.MaNhom
                    WHERE sp.MaNhom = @MaNhom
                    ORDER BY sp.TenSP";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNhom", maNhom);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(MapProduct(reader));
                        }
                    }
                }
            }

            return products;
        }

        public SanPham GetProductById(int maSP)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT sp.MaSP, sp.TenSP, sp.MaNhom, sp.NhaSanXuat, sp.GiaBan, 
                           sp.TinhTrang, sp.MoTa, sp.HinhAnh, n.TenNhom
                    FROM SanPham sp
                    INNER JOIN NhomSanPham n ON sp.MaNhom = n.MaNhom
                    WHERE sp.MaSP = @MaSP";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapProduct(reader);
                        }
                    }
                }
            }

            return null;
        }

        public List<SanPham> SearchProducts(string keyword)
        {
            var products = new List<SanPham>();

            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT sp.MaSP, sp.TenSP, sp.MaNhom, sp.NhaSanXuat, sp.GiaBan, 
                           sp.TinhTrang, sp.MoTa, sp.HinhAnh, n.TenNhom
                    FROM SanPham sp
                    INNER JOIN NhomSanPham n ON sp.MaNhom = n.MaNhom
                    WHERE sp.TenSP LIKE @Keyword OR sp.NhaSanXuat LIKE @Keyword
                    ORDER BY sp.TenSP";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", string.Format("%{0}%", keyword));
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(MapProduct(reader));
                        }
                    }
                }
            }

            return products;
        }

        private SanPham MapProduct(SqlDataReader reader)
        {
            return new SanPham
            {
                MaSP = reader.GetInt32(0),           // MaSP
                TenSP = reader.GetString(1),         // TenSP
                MaNhom = reader.GetInt32(2),         // MaNhom
                NhaSanXuat = reader.IsDBNull(3) ? null : reader.GetString(3),  // NhaSanXuat
                GiaBan = reader.GetDecimal(4),       // GiaBan
                TinhTrang = reader.IsDBNull(5) ? null : reader.GetString(5),   // TinhTrang
                MoTa = reader.IsDBNull(6) ? null : reader.GetString(6),        // MoTa
                HinhAnh = reader.IsDBNull(7) ? null : reader.GetString(7),     // HinhAnh
                TenNhom = reader.GetString(8)        // TenNhom
            };
        }
    }
}
