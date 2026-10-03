using System;
using eShoppingPrototype.Models;
using eShoppingPrototype.Data;

namespace eShoppingPrototype.ServiceAdapter
{
    public class AuthenticationService
    {
        private readonly KhachHangDAO _customerDAO;
        private KhachHang _currentCustomer;

        public AuthenticationService()
        {
            _customerDAO = new KhachHangDAO();
        }

        public KhachHang CurrentCustomer
        {
            get { return _currentCustomer; }
        }

        public bool IsLoggedIn
        {
            get { return _currentCustomer != null; }
        }

        public (bool Success, string Message, KhachHang Customer) Login(string username, string password)
        {
            var customer = _customerDAO.Login(username, password);

            if (customer != null)
            {
                _currentCustomer = customer;
                return (true, "Đăng nhập thành công!", customer);
            }

            return (false, "Tên đăng nhập hoặc mật khẩu không đúng!", null);
        }

        public (bool Success, string Message) Register(KhachHang khachHang)
        {
            // Validate username
            if (string.IsNullOrWhiteSpace(khachHang.TenDangNhap))
                return (false, "Tên đăng nhập không được để trống!");

            if (_customerDAO.IsUsernameExists(khachHang.TenDangNhap))
                return (false, "Tên đăng nhập đã tồn tại!");

            // Validate password
            if (string.IsNullOrWhiteSpace(khachHang.MatKhau))
                return (false, "Mật khẩu không được để trống!");

            if (khachHang.MatKhau.Length < 6)
                return (false, "Mật khẩu phải có ít nhất 6 ký tự!");

            // Register
            try
            {
                int maKH = _customerDAO.RegisterCustomer(khachHang);
                return (true, $"Đăng ký thành công! Mã khách hàng: {maKH}");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi đăng ký: {ex.Message}");
            }
        }

        public void Logout()
        {
            _currentCustomer = null;
        }
    }
}
