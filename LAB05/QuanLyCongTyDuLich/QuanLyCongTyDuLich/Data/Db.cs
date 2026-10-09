using System.Configuration;
using System.Data;
using System.Data.SqlClient;
 
namespace QuanLyCongTyDuLich.Data
{
    /// <summary>Lớp hỗ trợ truy cập SQL Server; Form không viết SQL trực tiếp.</summary>
    public static class Db
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["QuanLyCongTyDuLichDB"].ConnectionString; }
        }
 
        public static SqlConnection OpenConnection()
        {
            var cn = new SqlConnection(ConnectionString);
            cn.Open();
            return cn;
        }
 
        /// <summary>Kiểm tra kết nối CSDL; trả về false kèm lý do nếu chưa cấu hình / chưa chạy script SQL.</summary>
        public static bool KiemTraKetNoi(out string loi)
        {
            try { using (var cn = OpenConnection()) { loi = null; return true; } }
            catch (System.Exception ex) { loi = ex.Message; return false; }
        }

        public static DataTable Query(string sql, params SqlParameter[] ps)
        {
            using (var cn = OpenConnection())
            using (var cmd = new SqlCommand(sql, cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
 
        public static int Execute(string sql, params SqlParameter[] ps)
        {
            using (var cn = OpenConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }
 
        public static object Scalar(string sql, params SqlParameter[] ps)
        {
            using (var cn = OpenConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteScalar();
            }
        }
 
        /// <summary>Tạo SqlParameter, chuỗi rỗng / null được ghi thành DBNull.</summary>
        public static SqlParameter P(string name, object value)
        {
            var s = value as string;
            if (value == null || (s != null && s.Trim().Length == 0)) return new SqlParameter(name, System.DBNull.Value);
            return new SqlParameter(name, s != null ? s.Trim() : value);
        }
    }
}
