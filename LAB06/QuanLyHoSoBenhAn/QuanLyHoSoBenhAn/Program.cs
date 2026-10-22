using System;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Forms;

namespace QuanLyHoSoBenhAn
{
    internal static class Program
    {
        /// <summary>
        /// Điểm khởi đầu của ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var frmLogin = new FrmLogin())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK && frmLogin.NhanVienDangNhap != null)
                {
                    Application.Run(new FrmMain(frmLogin.NhanVienDangNhap));
                }
            }
        }
    }
}
