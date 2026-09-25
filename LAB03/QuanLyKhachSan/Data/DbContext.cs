using System.Data;
using System.Data.SqlClient;

namespace QuanLyKhachSan.Data
{
    public class DbContext
    {
     
        private string connectionString = @"Server=Quynh;Database=QuanLyKhachSan;User Id=sa;Password=123;TrustServerCertificate=True;";

        public DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void ExecuteQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}