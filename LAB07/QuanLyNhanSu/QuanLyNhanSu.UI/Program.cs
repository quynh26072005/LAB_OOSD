using System;
using System.Configuration;
using System.Windows.Forms;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.UI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                string cs = ConfigurationManager.ConnectionStrings["QuanLyNhanSu"]?.ConnectionString;
                if (!string.IsNullOrWhiteSpace(cs))
                    DatabaseConfig.ConnectionString = cs;

                using (var conn = SqlHelper.OpenConnection())
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể kết nối CSDL QuanLyNhanSu.\nHãy chạy script Database\\Script_QuanLyNhanSu.sql trước.\n\n" + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm());
        }
    }
}