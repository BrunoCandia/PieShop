using PieShop.Models.Order;

namespace PieShop.DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
    }
}
