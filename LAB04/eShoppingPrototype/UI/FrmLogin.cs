using System;
using System.Drawing;
using System.Windows.Forms;
using eShoppingPrototype.ServiceAdapter;

namespace eShoppingPrototype.UI
{
    public partial class FrmLogin : Form
    {
        private readonly AuthenticationService _authService;

        public FrmLogin(AuthenticationService authService)
        {
            _authService = authService;
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Form properties
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.Text = "e-SHOPPING - Đăng nhập";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CreateControls();
        }

        private void CreateControls()
        {
            // Title Label
            var lblTitle = new Label
            {
                Text = "🛒 e-SHOPPING",
                Location = new Point(100, 20),
                Size = new Size(200, 40),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Username Label
            var lblUsername = new Label
            {
                Text = "Tên đăng nhập:",
                Location = new Point(50, 80),
                Size = new Size(120, 20)
            };

            // Username TextBox
            var txtUsername = new TextBox
            {
                Name = "txtUsername",
                Location = new Point(170, 78),
                Size = new Size(180, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };

            // Password Label
            var lblPassword = new Label
            {
                Text = "Mật khẩu:",
                Location = new Point(50, 115),
                Size = new Size(120, 20)
            };

            // Password TextBox
            var txtPassword = new TextBox
            {
                Name = "txtPassword",
                Location = new Point(170, 113),
                Size = new Size(180, 25),
                UseSystemPasswordChar = true,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };

            // Login Button
            var btnLogin = new Button
            {
                Text = "Đăng nhập",
                Location = new Point(90, 160),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogin.Click += (s, e) => LoginClick(txtUsername.Text, txtPassword.Text);

            // Register Button
            var btnRegister = new Button
            {
                Text = "Đăng ký",
                Location = new Point(210, 160),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(0, 150, 136),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRegister.Click += (s, e) => RegisterClick();

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                lblTitle, lblUsername, txtUsername, lblPassword, txtPassword,
                btnLogin, btnRegister
            });

            // Set Enter key to login
            this.AcceptButton = btnLogin;
        }

        private void LoginClick(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(Messages.EnterFullInfo, Messages.Notification,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = _authService.Login(username, password);

            if (result.Success)
            {
                MessageBox.Show(string.Format(Messages.Welcome, result.Customer.HoTen),
                    Messages.LoginSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, Messages.LoginError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterClick()
        {
            var frmRegister = new FrmRegister(_authService);
            if (frmRegister.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(Messages.RegisterSuccessPleaseLogin,
                    Messages.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public partial class FrmLogin
    {
        // Designer part (empty for 100% code-based UI)
    }
}
