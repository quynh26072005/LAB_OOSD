using eShoppingPrototype.Models;

namespace eShoppingPrototype.ServiceAdapter
{
    public interface IOrderService
    {
        int SaveOrder(DonHang donHang);
    }
}
