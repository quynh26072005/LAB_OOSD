using eShoppingPrototype.Models;
using eShoppingPrototype.Data;

namespace eShoppingPrototype.ServiceAdapter
{
    public class OrderService : IOrderService
    {
        private readonly DonHangDAO _orderDAO;

        public OrderService()
        {
            _orderDAO = new DonHangDAO();
        }

        public int SaveOrder(DonHang donHang)
        {
            return _orderDAO.InsertDonHang(donHang);
        }
    }
}
