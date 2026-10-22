using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyHoSoBenhAn.Data
{
    /// <summary>
    /// Tầng Data: bọc các thao tác ADO.NET thuần (không dùng SQL trong code-behind form,
    /// theo yêu cầu phi chức năng "Bảo trì" trong tài liệu phân tích - mục 1.5).
    /// Toàn bộ Form/Service chỉ gọi vào đây hoặc vào các lớp Service.
    /// </summary>
    public static class DbHelper
    {
        private static readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["QuanLyHoSoBenhAn"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>Thực thi câu lệnh không trả dữ liệu (INSERT/UPDATE/DELETE), trả về số dòng ảnh hưởng.</summary>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Thực thi trong một transaction đã mở sẵn (dùng cho nghiệp vụ nhiều bảng).</summary>
        public static int ExecuteNonQuery(SqlConnection conn, SqlTransaction tran, string sql, params SqlParameter[] parameters)
        {
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Thực thi và lấy giá trị định danh vừa tạo (SCOPE_IDENTITY) trong transaction.</summary>
        public static int ExecuteInsertReturnId(SqlConnection conn, SqlTransaction tran, string sql, params SqlParameter[] parameters)
        {
            string sqlWithIdentity = sql + "; SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var cmd = new SqlCommand(sqlWithIdentity, conn, tran))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                object result = cmd.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
            }
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        public static object ExecuteScalar(SqlConnection conn, SqlTransaction tran, string sql, params SqlParameter[] parameters)
        {
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        public static DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        public static DataRow ExecuteQuerySingleRow(string sql, params SqlParameter[] parameters)
        {
            DataTable table = ExecuteQuery(sql, parameters);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }

        /// <summary>Băm mật khẩu bằng SHA-256, trả về chuỗi hex thường (khớp dữ liệu mẫu trong script SQL).</summary>
        public static string HashPassword(string plainPassword)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plainPassword ?? string.Empty));
                var sb = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        /// <summary>Kiểm tra kết nối tới CSDL, dùng cho form đăng nhập báo lỗi rõ ràng.</summary>
        public static bool TestConnection(out string error)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                }
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }
    }
}
