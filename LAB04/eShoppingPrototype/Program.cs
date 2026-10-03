using System;
using System.Windows.Forms;
using eShoppingPrototype.UI;
using eShoppingPrototype.ServiceAdapter;

namespace eShoppingPrototype
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Add global exception handler
            Application.ThreadException += Application_ThreadException;

            try
            {
                // Initialize services - SHARED INSTANCES
                var authService = new AuthenticationService();

                // Show login form
                var frmLogin = new FrmLogin(authService);
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    // User logged in successfully
                    // Verify CurrentCustomer is set
                    if (authService.CurrentCustomer == null)
                    {
                        MessageBox.Show("Login succeeded but CurrentCustomer is null!", "Error");
                        return;
                    }

                    // Create new instances AFTER login
                    var productService = new ProductServiceAdapter();
                    var cartService = new ShoppingCartService();
                    
                    // Show product list with the SAME authService instance
                    var frmProductList = new FrmProductList(productService, cartService, authService);
                    Application.Run(frmProductList);
                }
                else
                {
                    // User cancelled login
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal error: " + ex.ToString(), "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Thread exception: " + e.Exception.ToString(), "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
