using PieShop.DataAccess.Data.Entitites.Order;
using OrderModel = PieShop.Models.Order;

namespace PieShop.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PieShopContext _pieShopContext;
        private readonly IShoppingCartRepository _shopCartRepository;

        public OrderRepository(PieShopContext pieShopContext, IShoppingCartRepository shopCartRepository)
        {
            _pieShopContext = pieShopContext;
            _shopCartRepository = shopCartRepository;
        }

        public async Task CreateOrderAsync(OrderModel.Order order)
        {
            // TODO: review to the async
            var shoppingCartItems = await _shopCartRepository.GetShoppingCartItemsAsync();
            var shoppingCartTotal = await _shopCartRepository.GetShoppingCartTotalAsync();

            var orderEntity = new Order
            {
                FirstName = order.FirstName,
                LastName = order.LastName,
                AddressLine1 = order.AddressLine1,
                AddressLine2 = order.AddressLine2,
                ZipCode = order.ZipCode,
                City = order.City,
                State = order.State,
                Country = order.Country,
                PhoneNumber = order.PhoneNumber,
                Email = order.Email,
                OrderTotal = shoppingCartTotal,
                OrderPlaced = DateTimeOffset.UtcNow,
                OrderDetails = new List<OrderDetail>()
                ////OrderDetails = order.OrderDetails.Select(x => new OrderDetail
                ////{
                ////    Amount = x.Amount,
                ////    Price = x.Price,
                ////    OrderId = x.OrderId,
                ////    PieId = x.PieId,
                ////}).ToList()
            };

            foreach (var item in shoppingCartItems)
            {
                // TODO: create a list and add the range
                var orderDetail = new OrderDetail
                {
                    Amount = item.Amount,
                    PieId = item.PieId,
                    Price = item.Pie.Price,
                };

                orderEntity.OrderDetails.Add(orderDetail);
            }

            await _pieShopContext.Order.AddAsync(orderEntity);

            await _pieShopContext.SaveChangesAsync();
        }
    }
}
