using System;
using System.Drawing;
using System.Windows.Forms;
using eShoppingPrototype.ServiceAdapter;

namespace eShoppingPrototype.UI
{
    public class FrmShoppingCart : Form
    {
        private readonly ShoppingCartService _cartService;
        private readonly AuthenticationService _authService;
        private readonly ProductServiceAdapter _productService;

        private DataGridView dgvCart;
        private Label lblTotal;

        public FrmShoppingCart(
            ShoppingCartService cartService,
            AuthenticationService authService,
            ProductServiceAdapter productService)
        {
            _cartService = cartService;
            _authService = authService;
            _productService = productService;

            InitializeForm();
            LoadCartData();
        }

        private void InitializeForm()
        {
            this.Text = "Giỏ hàng của tôi";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            CreateControls();
        }

        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "🛒 GIỎ HÀNG CỦA BẠN",
                Location = new Point(20, 20),
                Size = new Size(860, 35),
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // DataGridView for cart items
            dgvCart = new DataGridView
            {
                Location = new Point(20, 70),
                Size = new Size(840, 380),
                AllowUserToAddRows = false,
                ReadOnly = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaSP",
                HeaderText = "Mã SP",
                FillWeight = 10,
                ReadOnly = true
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenSP",
                HeaderText = "Tên sản phẩm",
                FillWeight = 40,
                ReadOnly = true
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn giá",
                FillWeight = 15,
                ReadOnly = true
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "Số lượng",
                FillWeight = 15,
                ReadOnly = false
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien",
                HeaderText = "Thành tiền",
                FillWeight = 20,
                ReadOnly = true
            });

            dgvCart.CellValueChanged += (s, e) =>
            {
                if (e.ColumnIndex == 3) // Quantity column
                    UpdateQuantity(e.RowIndex);
            };

            // Total Panel
            var pnlTotal = new Panel
            {
                Location = new Point(20, 460),
                Size = new Size(840, 50),
                BackColor = Color.FromArgb(240, 240, 240)
            };

            lblTotal = new Label
            {
                Text = "TỔNG TIỀN: 0 đ",
                Location = new Point(550, 10),
                Size = new Size(280, 30),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleRight
            };

            pnlTotal.Controls.Add(lblTotal);

            // Buttons
            var btnUpdate = new Button
            {
                Text = "Cập nhật giỏ hàng",
                Location = new Point(20, 520),
                Size = new Size(150, 35),
                BackColor = Color.FromArgb(0, 150, 136),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnUpdate.Click += (s, e) => LoadCartData();

            var btnRemove = new Button
            {
                Text = "Xóa sản phẩm",
                Location = new Point(190, 520),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemove.Click += BtnRemove_Click;

            var btnClear = new Button
            {
                Text = "Xóa tất cả",
                Location = new Point(330, 520),
                Size = new Size(100, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClear.Click += BtnClear_Click;

            var btnCheckout = new Button
            {
                Text = "Thanh toán ➜",
                Location = new Point(680, 520),
                Size = new Size(180, 35),
                BackColor = Color.FromArgb(255, 87, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            btnCheckout.Click += BtnCheckout_Click;

            this.Controls.AddRange(new Control[]
            {
                lblTitle, dgvCart, pnlTotal,
                btnUpdate, btnRemove, btnClear, btnCheckout
            });
        }

        private void LoadCartData()
        {
            dgvCart.Rows.Clear();

            var items = _cartService.GetCartItems();

            foreach (var item in items)
            {
                dgvCart.Rows.Add(
                    item.SanPham.MaSP,
                    item.SanPham.TenSP,
                    $"{item.SanPham.GiaBan:N0} đ",
                    item.SoLuong,
                    $"{item.ThanhTien:N0} đ"
                );
            }

            UpdateTotal();
        }

        private void UpdateQuantity(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvCart.Rows.Count)
                return;

            try
            {
                int maSP = Convert.ToInt32(dgvCart.Rows[rowIndex].Cells[0].Value);
                int newQuantity = Convert.ToInt32(dgvCart.Rows[rowIndex].Cells[3].Value);

                if (newQuantity <= 0)
                {
                    MessageBox.Show(Messages.QuantityMustBeGreaterThanZero, Messages.Error,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadCartData();
                    return;
                }

                _cartService.UpdateQuantity(maSP, newQuantity);
                LoadCartData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Messages.UpdateError, ex.Message), Messages.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadCartData();
            }
        }

        private void UpdateTotal()
        {
            decimal total = _cartService.GetTotalAmount();
            lblTotal.Text = $"TỔNG TIỀN: {total:N0} đ";
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show(Messages.SelectProductToDelete, Messages.Notification,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSP = Convert.ToInt32(dgvCart.SelectedRows[0].Cells[0].Value);
            string tenSP = dgvCart.SelectedRows[0].Cells[1].Value.ToString();

            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa '{tenSP}' khỏi giỏ hàng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _cartService.RemoveProduct(maSP);
                LoadCartData();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (_cartService.IsEmpty())
            {
                MessageBox.Show(Messages.CartIsEmpty, Messages.Notification,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                "Bạn có chắc muốn xóa tất cả sản phẩm khỏi giỏ hàng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _cartService.ClearCart();
                LoadCartData();
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (_cartService.IsEmpty())
            {
                MessageBox.Show(Messages.CartEmptySelectProducts,
                    Messages.Notification, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open checkout form
            var frmCheckout = new FrmCheckout(_cartService, _authService);
            if (frmCheckout.ShowDialog() == DialogResult.OK)
            {
                // Order completed successfully
                this.Close();
            }
        }
    }
}
