using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Forms;

namespace QuanLyCongTyDuLich
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => MessageBox.Show(e.Exception.Message,
                "Có lỗi xảy ra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
