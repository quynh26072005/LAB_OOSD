using System;
using System.Drawing;
using System.Windows.Forms;
using eShoppingPrototype.Models;
using eShoppingPrototype.ServiceAdapter;

namespace eShoppingPrototype.UI
{
    public class FrmRegister : Form
    {
        private readonly AuthenticationService _authService;
        private TextBox txtHoTen, txtDienThoai, txtEmail, txtUsername, txtPassword, txtConfirmPassword;

        public FrmRegister(AuthenticationService authService)
        {
            _authService = authService;
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.Text = "Đăng ký tài khoản";
            this.Size = new Size(450, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CreateControls();
        }

        private void CreateControls()
        {
            int yPos = 20;
            int labelWidth = 130;
            int textBoxX = 150;
            int textBoxWidth = 250;

            // Title
            AddLabel("ĐĂNG KÝ TÀI KHOẢN", 120, yPos, 200, 30, new Font("Arial", 14, FontStyle.Bold));
            yPos += 50;

            // Full Name
            AddLabel("Họ và tên:", 20, yPos, labelWidth, 20);
            txtHoTen = AddTextBox("txtHoTen", textBoxX, yPos, textBoxWidth);
            yPos += 35;

            // Phone
            AddLabel("Điện thoại:", 20, yPos, labelWidth, 20);
            txtDienThoai = AddTextBox("txtDienThoai", textBoxX, yPos, textBoxWidth);
            yPos += 35;

            // Email
            AddLabel("Email:", 20, yPos, labelWidth, 20);
            txtEmail = AddTextBox("txtEmail", textBoxX, yPos, textBoxWidth);
            yPos += 35;

            // Username
            AddLabel("Tên đăng nhập:", 20, yPos, labelWidth, 20);
            txtUsername = AddTextBox("txtUsername", textBoxX, yPos, textBoxWidth);
            yPos += 35;

            // Password
            AddLabel("Mật khẩu:", 20, yPos, labelWidth, 20);
            txtPassword = AddTextBox("txtPassword", textBoxX, yPos, textBoxWidth, true);
            yPos += 35;

            // Confirm Password
            AddLabel("Xác nhận mật khẩu:", 20, yPos, labelWidth, 20);
            txtConfirmPassword = AddTextBox("txtConfirmPassword", textBoxX, yPos, textBoxWidth, true);
            yPos += 50;

            // Register Button
            var btnRegister = new Button
            {
                Text = "Đăng ký",
                Location = new Point(120, yPos),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(0, 150, 136),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRegister.Click += BtnRegister_Click;

            // Cancel Button
            var btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(240, yPos),
                Size = new Size(100, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { btnRegister, btnCancel });
        }

        private void AddLabel(string text, int x, int y, int width, int height, Font font = null)
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = font ?? this.Font
            };
            this.Controls.Add(label);
        }

        private TextBox AddTextBox(string name, int x, int y, int width, bool isPassword = false)
        {
            var textBox = new TextBox
            {
                Name = name,
                Location = new Point(x, y),
                Size = new Size(width, 25),
                UseSystemPasswordChar = isPassword,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            this.Controls.Add(textBox);
            return textBox;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show(Messages.EnterFullName, Messages.Notification,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show(Messages.EnterUsername, Messages.Notification,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(Messages.EnterPassword, Messages.Notification,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show(Messages.PasswordMismatch, Messages.Notification,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            // Create customer object
            var khachHang = new KhachHang
            {
                HoTen = txtHoTen.Text,
                DienThoai = txtDienThoai.Text,
                Email = txtEmail.Text,
                TenDangNhap = txtUsername.Text,
                MatKhau = txtPassword.Text
            };

            // Register
            var result = _authService.Register(khachHang);

            if (result.Success)
            {
                MessageBox.Show(result.Message, Messages.Success,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, Messages.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
