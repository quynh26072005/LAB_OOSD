using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using eShoppingPrototype.Models;
using eShoppingPrototype.ServiceAdapter;
using eShoppingPrototype.Data;

namespace eShoppingPrototype.UI
{
    public class FrmCheckout : Form
    {
        private readonly ShoppingCartService _cartService;
        private readonly AuthenticationService _authService;
        private readonly DonHangService _orderService;
        private readonly PaymentAdapter _paymentAdapter;
        private readonly EmailAdapter _emailAdapter;
        private readonly KhuVucDAO _regionDAO;

        private ListBox lstCart;
        private TextBox txtCartTotal, txtShippingFee, txtCardFee, txtFinalTotal;
        private ComboBox cboShippingType, cboCardType, cboRegion;
        private TextBox txtRecipientName, txtRecipientAddress, txtRecipientPhone;
        private TextBox txtCardNumber, txtCardHolder, txtCSV;
        private DateTimePicker dtpExpiryDate;

        public FrmCheckout(ShoppingCartService cartService, AuthenticationService authService)
        {
            _cartService = cartService;
            _authService = authService;
            _orderService = new DonHangService();
            _paymentAdapter = new PaymentAdapter();
            _emailAdapter = new EmailAdapter();
            _regionDAO = new KhuVucDAO();

            InitializeForm();
            LoadCartData();
            LoadRegions();
            UpdateTotals();
        }

        private void InitializeForm()
        {
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.Text = "Thanh toán đơn hàng";
            this.Size = new Size(900, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            CreateControls();
        }

        private void CreateControls()
        {
            int col1 = 20, col2 = 480;
            int yPos = 20;

            // Title
            AddLabel("THANH TOÁN ĐƠN HÀNG", 300, yPos, 300, 35,
                new Font("Arial", 16, FontStyle.Bold));
            yPos += 50;

            // === LEFT COLUMN: Cart & Recipient ===
            // Cart Section
            AddLabel("THÔNG TIN ĐƠN HÀNG", col1, yPos, 420, 25,
                new Font("Arial", 12, FontStyle.Bold));
            yPos += 30;

            lstCart = new ListBox
            {
                Location = new Point(col1, yPos),
                Size = new Size(420, 150),
                Font = new Font("Courier New", 9, FontStyle.Regular)
            };
            this.Controls.Add(lstCart);
            yPos += 160;

            // Recipient Section
            AddLabel("THÔNG TIN NGƯỜI NHẬN", col1, yPos, 420, 25,
                new Font("Arial", 12, FontStyle.Bold));
            yPos += 35;

            AddLabel("Họ và tên:", col1, yPos, 120, 20);
            txtRecipientName = AddTextBox(col1 + 130, yPos, 270);
            yPos += 30;

            AddLabel("Địa chỉ:", col1, yPos, 120, 20);
            txtRecipientAddress = AddTextBox(col1 + 130, yPos, 270);
            yPos += 30;

            AddLabel("Điện thoại:", col1, yPos, 120, 20);
            txtRecipientPhone = AddTextBox(col1 + 130, yPos, 270);
            yPos += 30;

            AddLabel("Khu vực:", col1, yPos, 120, 20);
            cboRegion = new ComboBox
            {
                Location = new Point(col1 + 130, yPos),
                Size = new Size(270, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            cboRegion.SelectedIndexChanged += (s, e) => UpdateTotals();
            this.Controls.Add(cboRegion);
            yPos += 30;

            AddLabel("Hình thức giao hàng:", col1, yPos, 120, 20);
            cboShippingType = new ComboBox
            {
                Location = new Point(col1 + 130, yPos),
                Size = new Size(270, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            cboShippingType.Items.AddRange(new object[]
            {
                "Thường",
                "Chuyển Phát Nhanh",
                "Chuyển Phát Nhanh Trong Ngày"
            });
            cboShippingType.SelectedIndex = 0;
            cboShippingType.SelectedIndexChanged += (s, e) => UpdateTotals();
            this.Controls.Add(cboShippingType);

            // === RIGHT COLUMN: Payment & Summary ===
            yPos = 100; // Reset position for right column

            // Payment Section
            AddLabel("THÔNG TIN THANH TOÁN", col2, yPos, 380, 25,
                new Font("Arial", 12, FontStyle.Bold));
            yPos += 35;

            AddLabel("Loại thẻ:", col2, yPos, 120, 20);
            cboCardType = new ComboBox
            {
                Location = new Point(col2 + 130, yPos),
                Size = new Size(230, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            cboCardType.Items.AddRange(new object[]
            {
                "VISA",
                "Master",
                "Discover",
                "American Express"
            });
            cboCardType.SelectedIndex = 0;
            cboCardType.SelectedIndexChanged += (s, e) => UpdateTotals();
            this.Controls.Add(cboCardType);
            yPos += 30;

            AddLabel("Số thẻ:", col2, yPos, 120, 20);
            txtCardNumber = AddTextBox(col2 + 130, yPos, 230);
            yPos += 30;

            AddLabel("Tên chủ thẻ:", col2, yPos, 120, 20);
            txtCardHolder = AddTextBox(col2 + 130, yPos, 230);
            yPos += 30;

            AddLabel("Ngày hết hạn:", col2, yPos, 120, 20);
            dtpExpiryDate = new DateTimePicker
            {
                Location = new Point(col2 + 130, yPos),
                Size = new Size(230, 25),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MM/yyyy",
                MinDate = DateTime.Now
            };
            this.Controls.Add(dtpExpiryDate);
            yPos += 30;

            AddLabel("Mã CSV:", col2, yPos, 120, 20);
            txtCSV = AddTextBox(col2 + 130, yPos, 100);
            yPos += 40;

            // Order Summary
            AddLabel("TỔNG KẾT ĐƠN HÀNG", col2, yPos, 380, 25,
                new Font("Arial", 12, FontStyle.Bold));
            yPos += 35;

            AddLabel("Tổng tiền hàng:", col2, yPos, 150, 20);
            txtCartTotal = AddTextBox(col2 + 160, yPos, 200, true);
            yPos += 30;

            AddLabel("Phí giao hàng:", col2, yPos, 150, 20);
            txtShippingFee = AddTextBox(col2 + 160, yPos, 200, true);
            yPos += 30;

            AddLabel("Lệ phí thẻ:", col2, yPos, 150, 20);
            txtCardFee = AddTextBox(col2 + 160, yPos, 200, true);
            yPos += 30;

            AddLabel("TỔNG THANH TOÁN:", col2, yPos, 150, 25,
                new Font("Arial", 11, FontStyle.Bold));
            txtFinalTotal = new TextBox
            {
                Location = new Point(col2 + 160, yPos),
                Size = new Size(200, 25),
                ReadOnly = true,
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Red,
                TextAlign = HorizontalAlignment.Right
            };
            this.Controls.Add(txtFinalTotal);
            yPos += 50;

            // Buttons
            var btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(col2, yPos),
                Size = new Size(120, 40),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => this.Close();

            var btnPay = new Button
            {
                Text = "Thanh toán",
                Location = new Point(col2 + 240, yPos),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(255, 87, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 11, FontStyle.Bold)
            };
            btnPay.Click += ProcessPayment;

            this.Controls.AddRange(new Control[] { btnCancel, btnPay });
        }

        private void LoadRegions()
        {
            var regions = _regionDAO.GetAllRegions();
            cboRegion.DataSource = regions;
            cboRegion.DisplayMember = "TenKhuVuc";
            cboRegion.ValueMember = "MaKhuVuc";
        }

        private void LoadCartData()
        {
            lstCart.Items.Clear();
            var items = _cartService.GetCartItems();

            foreach (var item in items)
            {
                lstCart.Items.Add(
                    $"{item.SanPham.TenSP,-35} x{item.SoLuong}  {item.ThanhTien,12:N0} đ");
            }
        }

        private void UpdateTotals()
        {
            if (cboRegion.SelectedValue == null) return;

            var orderDetails = _cartService.ConvertToOrderDetails();
            var selectedRegion = (KhuVuc)cboRegion.SelectedItem;

            var order = new DonHang
            {
                ChiTietDonHang = orderDetails,
                LoaiGiaoHang = cboShippingType.SelectedItem?.ToString() ?? "Thường",
                LoaiThe = cboCardType.SelectedItem?.ToString() ?? "VISA",
                NguoiNhan = new NguoiNhan { MaKhuVuc = selectedRegion.MaKhuVuc }
            };

            _orderService.CalculateOrderTotals(order);

            txtCartTotal.Text = $"{order.TongTriGiaSanPham:N0} đ";
            txtShippingFee.Text = $"{order.PhiGiaoHang:N0} đ";
            txtCardFee.Text = $"{order.LePhi:N0} đ";
            txtFinalTotal.Text = $"{order.TongTriGiaHoaDon:N0} đ";
        }

        private void ProcessPayment(object sender, EventArgs e)
        {
            // Validate recipient info
            if (string.IsNullOrWhiteSpace(txtRecipientName.Text) ||
                string.IsNullOrWhiteSpace(txtRecipientAddress.Text) ||
                string.IsNullOrWhiteSpace(txtRecipientPhone.Text))
            {
                MessageBox.Show(Messages.EnterRecipientInfo,
                    Messages.Notification, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate payment info
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text) ||
                string.IsNullOrWhiteSpace(txtCardHolder.Text) ||
                string.IsNullOrWhiteSpace(txtCSV.Text))
            {
                MessageBox.Show(Messages.EnterCardInfo,
                    Messages.Notification, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build order
            var order = BuildOrder();
            _orderService.CalculateOrderTotals(order);

            // Process payment
            var paymentResult = _paymentAdapter.ProcessPayment(order.TheTinDung, order.TongTriGiaHoaDon);
            if (!paymentResult.Success)
            {
                MessageBox.Show(string.Format(Messages.PaymentFailed, paymentResult.Message),
                    Messages.PaymentError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save order
            SaveOrder(order);
        }

        private DonHang BuildOrder()
        {
            var selectedRegion = (KhuVuc)cboRegion.SelectedItem;

            return new DonHang
            {
                MaKH = _authService.CurrentCustomer.MaKH,
                ChiTietDonHang = _cartService.ConvertToOrderDetails(),
                LoaiGiaoHang = cboShippingType.SelectedItem.ToString(),
                NguoiNhan = new NguoiNhan
                {
                    HoTen = txtRecipientName.Text,
                    DiaChi = txtRecipientAddress.Text,
                    DienThoai = txtRecipientPhone.Text,
                    MaKhuVuc = selectedRegion.MaKhuVuc,
                    TenKhuVuc = selectedRegion.TenKhuVuc
                },
                TheTinDung = new TheTinDung
                {
                    LoaiThe = cboCardType.SelectedItem.ToString(),
                    SoThe = txtCardNumber.Text,
                    NgayHetHan = dtpExpiryDate.Value,
                    TenChuThe = txtCardHolder.Text,
                    CSV = txtCSV.Text
                },
                LoaiThe = cboCardType.SelectedItem.ToString(),
                So4SoCuoi = txtCardNumber.Text.Length >= 4 
                    ? txtCardNumber.Text.Substring(txtCardNumber.Text.Length - 4) 
                    : txtCardNumber.Text,
                NgayHetHan = dtpExpiryDate.Value,
                TenChuThe = txtCardHolder.Text,
                TrangThai = "Confirmed"
            };
        }

        private void SaveOrder(DonHang order)
        {
            try
            {
                int orderId = _orderService.SaveOrder(order);
                order.MaDonHang = orderId;

                // Send email confirmation
                if (!string.IsNullOrEmpty(_authService.CurrentCustomer.Email))
                {
                    _emailAdapter.SendOrderConfirmationEmail(
                        _authService.CurrentCustomer.Email, order);
                }

                // Clear cart
                _cartService.ClearCart();

                MessageBox.Show(
                    $"Đặt hàng thành công!\n\n" +
                    $"Mã đơn hàng: {orderId}\n" +
                    $"Tổng tiền: {order.TongTriGiaHoaDon:N0} đ\n" +
                    $"Thanh toán: {order.LoaiThe} ****{order.So4SoCuoi}\n\n" +
                    $"Email xác nhận đã được gửi!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Messages.SaveOrderError, ex.Message),
                    Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddLabel(string text, int x, int y, int width, int height,
            Font font = null)
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

        private TextBox AddTextBox(int x, int y, int width, bool readOnly = false)
        {
            var textBox = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 25),
                ReadOnly = readOnly,
                Font = new Font("Segoe UI", 10, FontStyle.Regular)
            };
            this.Controls.Add(textBox);
            return textBox;
        }
    }
}
