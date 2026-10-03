using System;
using System.Drawing;
using System.Windows.Forms;
using eShoppingPrototype.Models;
using eShoppingPrototype.ServiceAdapter;

namespace eShoppingPrototype.UI
{
    public class FrmProductDetail : Form
    {
        private readonly SanPham _product;
        private readonly ShoppingCartService _cartService;
        private NumericUpDown nudQuantity;

        public FrmProductDetail(SanPham product, ShoppingCartService cartService)
        {
            _product = product;
            _cartService = cartService;

            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Chi tiết sản phẩm";
            this.Size = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CreateControls();
        }

        private void CreateControls()
        {
            int yPos = 20;

            // Product Name
            AddLabel($"Tên sản phẩm: {_product.TenSP}", 20, yPos, 560, 30,
                new Font("Arial", 14, FontStyle.Bold));
            yPos += 40;

            // Category
            AddLabel($"Danh mục: {_product.TenNhom}", 20, yPos, 560, 25);
            yPos += 30;

            // Manufacturer
            AddLabel($"Nhà sản xuất: {_product.NhaSanXuat ?? "N/A"}", 20, yPos, 560, 25);
            yPos += 30;

            // Price
            AddLabel($"Giá bán: {_product.GiaBan:N0} đ", 20, yPos, 560, 30,
                new Font("Arial", 12, FontStyle.Bold), Color.Red);
            yPos += 40;

            // Status
            AddLabel($"Tình trạng: {_product.TinhTrang}", 20, yPos, 560, 25);
            yPos += 40;

            // Description
            AddLabel("Mô tả sản phẩm:", 20, yPos, 560, 25, new Font("Arial", 10, FontStyle.Bold));
            yPos += 30;

            var txtDescription = new TextBox
            {
                Location = new Point(20, yPos),
                Size = new Size(540, 80),
                Multiline = true,
                ReadOnly = true,
                Text = _product.MoTa ?? "Không có mô tả",
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            this.Controls.Add(txtDescription);
            yPos += 90;

            // Technical Specifications
            AddLabel("Thông số kỹ thuật:", 20, yPos, 560, 25, new Font("Arial", 10, FontStyle.Bold));
            yPos += 30;

            var txtSpecs = new TextBox
            {
                Location = new Point(20, yPos),
                Size = new Size(540, 60),
                Multiline = true,
                ReadOnly = true,
                Text = _product.ThongSoKyThuat ?? "Không có thông số",
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            this.Controls.Add(txtSpecs);
            yPos += 70;

            // Quantity
            AddLabel("Số lượng:", 20, yPos, 80, 25);

            nudQuantity = new NumericUpDown
            {
                Location = new Point(100, yPos),
                Size = new Size(80, 25),
                Minimum = 1,
                Maximum = 99,
                Value = 1,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            this.Controls.Add(nudQuantity);

            // Add to Cart Button
            var btnAddToCart = new Button
            {
                Text = "🛒 Thêm vào giỏ hàng",
                Location = new Point(200, yPos - 5),
                Size = new Size(180, 35),
                BackColor = Color.FromArgb(255, 87, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnAddToCart.Click += BtnAddToCart_Click;

            // Close Button
            var btnClose = new Button
            {
                Text = "Đóng",
                Location = new Point(400, yPos - 5),
                Size = new Size(100, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { btnAddToCart, btnClose });
        }

        private void AddLabel(string text, int x, int y, int width, int height, 
            Font font = null, Color? color = null)
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = font ?? this.Font,
                ForeColor = color ?? Color.Black
            };
            this.Controls.Add(label);
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            int quantity = (int)nudQuantity.Value;
            _cartService.AddProduct(_product, quantity);

            MessageBox.Show(
                $"Đã thêm {quantity} x '{_product.TenSP}' vào giỏ hàng!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.Close();
        }
    }
}
