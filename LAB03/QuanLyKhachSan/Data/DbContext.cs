using System.Data;
using System.Data.SqlClient;
using System.Configuration; // 1. Thêm namespace này để đọc App.config

namespace QuanLyKhachSan.Data
{
    public class DbContext
    {
        // 2. Đọc chuỗi kết nối tên "HotelDB" từ file App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;

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