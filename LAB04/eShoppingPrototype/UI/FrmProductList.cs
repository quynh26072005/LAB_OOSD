using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using eShoppingPrototype.Models;
using eShoppingPrototype.ServiceAdapter;

namespace eShoppingPrototype.UI
{
    public class FrmProductList : Form
    {
        private readonly ProductServiceAdapter _productService;
        private readonly ShoppingCartService _cartService;
        private readonly AuthenticationService _authService;

        private ComboBox cboCategories;
        private ListView lvProducts;
        private Label lblCartCount;
        private TextBox txtSearch;

        public FrmProductList(
            ProductServiceAdapter productService,
            ShoppingCartService cartService,
            AuthenticationService authService)
        {
            try
            {
                _productService = productService;
                _cartService = cartService;
                _authService = authService;

                // Verify authService and CurrentCustomer
                if (_authService == null)
                    throw new Exception("AuthenticationService is null");
                
                if (_authService.CurrentCustomer == null)
                    throw new Exception("CurrentCustomer is null");

                InitializeForm();
                LoadCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FrmProductList constructor error: " + ex.ToString(), "Error");
                throw;
            }
        }

        private void InitializeForm()
        {
            // Set form encoding to UTF-8
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            
            this.Text = "e-SHOPPING - Danh sách sản phẩm";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            CreateControls();
        }

        private void CreateControls()
        {
            // Top Panel
            var pnlTop = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(984, 80),
                BackColor = Color.FromArgb(0, 120, 215)
            };

            // Welcome Label
            var lblWelcome = new Label
            {
                Text = $"Xin chào, {_authService.CurrentCustomer.HoTen}!",
                Location = new Point(20, 15),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White
            };

            // Cart Button
            var btnCart = new Button
            {
                Text = "🛒 Giỏ hàng",
                Location = new Point(850, 15),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(255, 87, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnCart.Click += (s, e) => OpenShoppingCart();

            // Cart Count Label
            lblCartCount = new Label
            {
                Text = "0",
                Location = new Point(945, 12),
                Size = new Size(25, 25),
                BackColor = Color.Red,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            // Search TextBox
            txtSearch = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(300, 25),
                Text = "Tìm kiếm sản phẩm...",
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.Text == "Tìm kiếm sản phẩm...")
                    txtSearch.Text = "";
            };
            txtSearch.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                    SearchProducts();
            };

            // Search Button
            var btnSearch = new Button
            {
                Text = "Tìm",
                Location = new Point(330, 48),
                Size = new Size(70, 29),
                BackColor = Color.White
            };
            btnSearch.Click += (s, e) => SearchProducts();

            pnlTop.Controls.AddRange(new Control[]
            {
                lblWelcome, btnCart, lblCartCount, txtSearch, btnSearch
            });

            // Category Panel
            var pnlCategory = new Panel
            {
                Location = new Point(0, 80),
                Size = new Size(200, 580),
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var lblCategory = new Label
            {
                Text = "DANH MỤC",
                Location = new Point(10, 10),
                Size = new Size(180, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            cboCategories = new ComboBox
            {
                Location = new Point(10, 50),
                Size = new Size(180, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            cboCategories.SelectedIndexChanged += (s, e) => LoadProducts();

            pnlCategory.Controls.AddRange(new Control[] { lblCategory, cboCategories });

            // Products ListView
            lvProducts = new ListView
            {
                Location = new Point(200, 80),
                Size = new Size(784, 580),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };

            lvProducts.Columns.Add("Mã SP", 60);
            lvProducts.Columns.Add("Tên sản phẩm", 300);
            lvProducts.Columns.Add("Nhà sản xuất", 150);
            lvProducts.Columns.Add("Giá bán", 120);
            lvProducts.Columns.Add("Tình trạng", 100);

            lvProducts.DoubleClick += (s, e) => ViewProductDetail();

            // Context Menu for ListView
            var contextMenu = new ContextMenuStrip();
            var menuViewDetail = new ToolStripMenuItem("Xem chi tiết");
            var menuAddToCart = new ToolStripMenuItem("Thêm vào giỏ hàng");

            menuViewDetail.Click += (s, e) => ViewProductDetail();
            menuAddToCart.Click += (s, e) => AddToCart();

            contextMenu.Items.AddRange(new ToolStripItem[] { menuViewDetail, menuAddToCart });
            lvProducts.ContextMenuStrip = contextMenu;

            this.Controls.AddRange(new Control[] { pnlTop, pnlCategory, lvProducts });

            UpdateCartCount();
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _productService.GetAllCategories();

                cboCategories.Items.Clear();
                
                // Tạo item "Tất cả sản phẩm" bằng object thực thay vì anonymous type
                var allProductsItem = new NhomSanPham 
                { 
                    MaNhom = 0, 
                    TenNhom = Messages.AllProducts
                };
                cboCategories.Items.Add(allProductsItem);

                foreach (var category in categories)
                {
                    cboCategories.Items.Add(category);
                }

                cboCategories.DisplayMember = "TenNhom";
                cboCategories.ValueMember = "MaNhom";
                cboCategories.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadCategories error: " + ex.ToString(), "Error");
                throw;
            }
        }

        private void LoadProducts()
        {
            try
            {
                lvProducts.Items.Clear();

                var selectedItem = cboCategories.SelectedItem;
                if (selectedItem == null) return;

                int maNhom;
                if (selectedItem is NhomSanPham)
                {
                    maNhom = ((NhomSanPham)selectedItem).MaNhom;
                }
                else
                {
                    // Anonymous type with MaNhom = 0
                    var propInfo = selectedItem.GetType().GetProperty("MaNhom");
                    maNhom = (int)propInfo.GetValue(selectedItem, null);
                }

                var products = maNhom == 0
                    ? _productService.GetAllCategories()
                        .SelectMany(c => _productService.GetProductsByCategory(c.MaNhom))
                        .ToList()
                    : _productService.GetProductsByCategory(maNhom);

                foreach (var product in products)
                {
                    var item = new ListViewItem(product.MaSP.ToString());
                    item.SubItems.Add(product.TenSP);
                    item.SubItems.Add(product.NhaSanXuat ?? "");
                    item.SubItems.Add(string.Format("{0:N0} đ", product.GiaBan));
                    item.SubItems.Add(product.TinhTrang);
                    item.Tag = product;

                    lvProducts.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadProducts error: " + ex.ToString(), "Error");
            }
        }

        private void SearchProducts()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) ||
                txtSearch.Text == "Tìm kiếm sản phẩm...")
                return;

            lvProducts.Items.Clear();
            var products = _productService.SearchProducts(txtSearch.Text);

            foreach (var product in products)
            {
                var item = new ListViewItem(product.MaSP.ToString());
                item.SubItems.Add(product.TenSP);
                item.SubItems.Add(product.NhaSanXuat ?? "");
                item.SubItems.Add($"{product.GiaBan:N0} đ");
                item.SubItems.Add(product.TinhTrang);
                item.Tag = product;

                lvProducts.Items.Add(item);
            }
        }

        private void ViewProductDetail()
        {
            if (lvProducts.SelectedItems.Count == 0) return;

            var product = (SanPham)lvProducts.SelectedItems[0].Tag;
            var frmDetail = new FrmProductDetail(product, _cartService);
            frmDetail.ShowDialog();

            UpdateCartCount();
        }

        private void AddToCart()
        {
            if (lvProducts.SelectedItems.Count == 0) return;

            var product = (SanPham)lvProducts.SelectedItems[0].Tag;
            _cartService.AddProduct(product, 1);

            UpdateCartCount();

            MessageBox.Show(string.Format(Messages.AddedToCart, product.TenSP),
                Messages.Notification, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenShoppingCart()
        {
            var frmCart = new FrmShoppingCart(_cartService, _authService, _productService);
            frmCart.ShowDialog();

            UpdateCartCount();
        }

        private void UpdateCartCount()
        {
            lblCartCount.Text = _cartService.GetTotalItems().ToString();
        }
    }
}
