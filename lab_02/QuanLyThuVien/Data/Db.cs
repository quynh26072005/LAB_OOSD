using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyThuVien.Data
{
    /// <summary>
    /// Lớp truy cập dữ liệu cơ sở - Data Access Layer
    /// </summary>
    public static class Db
    {
        /// <summary>
        /// Lấy connection string từ App.config
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["QuanLyThuVienDb"].ConnectionString;
            }
        }

        /// <summary>
        /// Mở kết nối đến database
        /// </summary>
        public static SqlConnection OpenConnection()
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            cn.Open();
            return cn;
        }

        /// <summary>
        /// Thực hiện truy vấn SELECT và trả về DataTable
        /// </summary>
        public static DataTable Query(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection cn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
        }

        /// <summary>
        /// Thực hiện truy vấn INSERT, UPDATE, DELETE
        /// </summary>
        public static int Execute(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection cn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Thực hiện truy vấn trả về giá trị đơn
        /// </summary>
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection cn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteScalar();
            }
        }
    }
}
