using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyNhanSu.Data
{
    /// <summary>Một câu lệnh SQL cùng tham số, dùng trong giao dịch.</summary>
    public sealed class SqlCommandSpec
    {
        public string Sql { get; set; }
        public SqlParameter[] Parameters { get; set; }

        public SqlCommandSpec(string sql, SqlParameter[] parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }
    }

    /// <summary>
    /// Lớp tiện ích ADO.NET: mở kết nối, thực thi câu lệnh, đọc dữ liệu
    /// và chuyển đổi kết quả sang thực thể (qua DbMapping).
    /// </summary>
    public static class SqlHelper
    {
        public static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();
            return conn;
        }

        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                AttachParameters(cmd, parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                AttachParameters(cmd, parameters);
                return cmd.ExecuteScalar();
            }
        }

        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                AttachParameters(cmd, parameters);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        /// <summary>Thực thi câu lệnh và ánh xạ kết quả sang danh sách đối tượng T.</summary>
        public static List<T> ExecuteList<T>(string sql, params SqlParameter[] parameters) where T : class, new()
        {
            List<T> result = new List<T>();
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                AttachParameters(cmd, parameters);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T item = new T();
                        DbMapping.Map(reader, item);
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        /// <summary>Thực thi câu lệnh và trả về đối tượng T đầu tiên (nếu có).</summary>
        public static T ExecuteSingle<T>(string sql, params SqlParameter[] parameters) where T : class, new()
        {
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                AttachParameters(cmd, parameters);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        T item = new T();
                        DbMapping.Map(reader, item);
                        return item;
                    }
                }
            }
            return null;
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>Thực thi một loạt câu lệnh trong một giao dịch (tất cả hoặc không).</summary>
        public static void ExecuteTransaction(IEnumerable<SqlCommandSpec> commands)
        {
            using (SqlConnection conn = OpenConnection())
            using (SqlTransaction tx = conn.BeginTransaction())
            {
                try
                {
                    foreach (SqlCommandSpec spec in commands)
                    {
                        using (SqlCommand cmd = new SqlCommand(spec.Sql, conn, tx))
                        {
                            if (spec.Parameters != null)
                                cmd.Parameters.AddRange(spec.Parameters);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        private static void AttachParameters(SqlCommand cmd, SqlParameter[] parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                {
                    if (p.Value == null)
                        p.Value = DBNull.Value;
                    cmd.Parameters.Add(p);
                }
            }
        }
    }
}