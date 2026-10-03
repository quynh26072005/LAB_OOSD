using System;

namespace eShoppingPrototype.UI
{
    /// <summary>
    /// Chứa các message tiếng Việt dạng Unicode escape để tránh lỗi encoding
    /// </summary>
    public static class Messages
    {
        // Common
        public static readonly string Notification = "Th\u00F4ng b\u00E1o";
        public static readonly string Error = "L\u1ED7i";
        public static readonly string Success = "Th\u00E0nh c\u00F4ng";
        
        // Login
        public static readonly string EnterFullInfo = "Vui l\u00F2ng nh\u1EADp \u0111\u1EA7y \u0111\u1EE7 th\u00F4ng tin!";
        public static readonly string LoginError = "L\u1ED7i \u0111\u0103ng nh\u1EADp";
        public static readonly string Welcome = "Ch\u00E0o m\u1EEBng {0}!";
        public static readonly string LoginSuccess = "\u0110\u0103ng nh\u1EADp th\u00E0nh c\u00F4ng";
        public static readonly string RegisterSuccessPleaseLogin = "\u0110\u0103ng k\u00FD th\u00E0nh c\u00F4ng! Vui l\u00F2ng \u0111\u0103ng nh\u1EADp.";
        
        // Register
        public static readonly string EnterFullName = "Vui l\u00F2ng nh\u1EADp h\u1ECDa t\u00EAn!";
        public static readonly string EnterUsername = "Vui l\u00F2ng nh\u1EADp t\u00EAn \u0111\u0103ng nh\u1EADp!";
        public static readonly string EnterPassword = "Vui l\u00F2ng nh\u1EADp m\u1EADt kh\u1EA9u!";
        public static readonly string PasswordMismatch = "M\u1EADt kh\u1EA9u x\u00E1c nh\u1EADn kh\u00F4ng kh\u1EDBp!";
        
        // Product List
        public static readonly string AddedToCart = "\u0110\u00E3 th\u00EAm '{0}' v\u00E0o gi\u1ECF h\u00E0ng!";
        public static readonly string AllProducts = "\u002D\u002D\u0020T\u1EA5t\u0020c\u1EA3\u0020s\u1EA3n\u0020ph\u1EA9m\u0020\u002D\u002D";
        
        // Shopping Cart
        public static readonly string QuantityMustBeGreaterThanZero = "S\u1ED1 l\u01B0\u1EE3ng ph\u1EA3i l\u1EDBn h\u01A1n 0!";
        public static readonly string UpdateError = "L\u1ED7i c\u1EADp nh\u1EADt: {0}";
        public static readonly string SelectProductToDelete = "Vui l\u00F2ng ch\u1ECDn s\u1EA3n ph\u1EA9m c\u1EA7n x\u00F3a!";
        public static readonly string CartIsEmpty = "Gi\u1ECF h\u00E0ng \u0111\u00E3 tr\u1ED1ng!";
        public static readonly string CartEmptySelectProducts = "Gi\u1ECF h\u00E0ng tr\u1ED1ng! Vui l\u00F2ng ch\u1ECDn s\u1EA3n ph\u1EA9m tr\u01B0\u1EDBc khi thanh to\u00E1n.";
        
        // Checkout
        public static readonly string EnterRecipientInfo = "Vui l\u00F2ng nh\u1EADp \u0111\u1EA7y \u0111\u1EE7 th\u00F4ng tin ng\u01B0\u1EDDi nh\u1EADn!";
        public static readonly string EnterCardInfo = "Vui l\u00F2ng nh\u1EADp \u0111\u1EA7y \u0111\u1EE7 th\u00F4ng tin th\u1EBB!";
        public static readonly string PaymentFailed = "Thanh to\u00E1n th\u1EA5t b\u1EA1i!\n\n{0}";
        public static readonly string PaymentError = "L\u1ED7i thanh to\u00E1n";
        public static readonly string SaveOrderError = "L\u1ED7i l\u01B0u \u0111\u01A1n h\u00E0ng:\n{0}";
    }
}
