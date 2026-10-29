using System;

namespace QuanLyNhanSu.Data
{
    /// <summary>
    /// Cấu hình kết nối CSDL. Chuỗi kết nối được gán từ tầng UI
    /// (đọc từ App.config) trước khi gọi bất kỳ thao tác dữ liệu nào.
    /// </summary>
    public static class DatabaseConfig
    {
        private static string _connectionString =
            "Data Source=.;Initial Catalog=QuanLyNhanSu;Integrated Security=True;";

        public static string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Chuỗi kết nối không được rỗng.", nameof(value));
                _connectionString = value;
            }
        }
    }
}