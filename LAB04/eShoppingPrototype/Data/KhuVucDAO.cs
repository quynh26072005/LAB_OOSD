using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using eShoppingPrototype.Models;

namespace eShoppingPrototype.Data
{
    public class KhuVucDAO
    {
        private readonly string _connectionString;

        public KhuVucDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["eShoppingDB"].ConnectionString;
        }

        public List<KhuVuc> GetAllRegions()
        {
            var regions = new List<KhuVuc>();

            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT MaKhuVuc, TenKhuVuc, PhiGiaoHangThuong, 
                           PhiGiaoHangNhanh, PhiGiaoHangTrongNgay
                    FROM KhuVuc
                    ORDER BY TenKhuVuc";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            regions.Add(new KhuVuc
                            {
                                MaKhuVuc = reader.GetInt32(0),
                                TenKhuVuc = reader.GetString(1),
                                PhiGiaoHangThuong = reader.GetDecimal(2),
                                PhiGiaoHangNhanh = reader.GetDecimal(3),
                                PhiGiaoHangTrongNgay = reader.GetDecimal(4)
                            });
                        }
                    }
                }
            }

            return regions;
        }

        public KhuVuc GetRegionById(int maKhuVuc)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT MaKhuVuc, TenKhuVuc, PhiGiaoHangThuong, 
                           PhiGiaoHangNhanh, PhiGiaoHangTrongNgay
                    FROM KhuVuc
                    WHERE MaKhuVuc = @MaKhuVuc";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new KhuVuc
                            {
                                MaKhuVuc = reader.GetInt32(0),
                                TenKhuVuc = reader.GetString(1),
                                PhiGiaoHangThuong = reader.GetDecimal(2),
                                PhiGiaoHangNhanh = reader.GetDecimal(3),
                                PhiGiaoHangTrongNgay = reader.GetDecimal(4)
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
