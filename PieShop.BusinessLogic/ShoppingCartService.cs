using PieShop.DataAccess.Repositories;
using PieShop.Models.Pie;
using PieShop.Models.ShoppingCart;

namespace PieShop.BusinessLogic
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        //TODO: add bussiness logic

        public async Task AddToCartAsync(Pie pie)
        {
            await _shoppingCartRepository.AddToCartAsync(pie);
        }

        public async Task<int> RemoveFromCartAsync(Pie pie)
        {
            return await _shoppingCartRepository.RemoveFromCartAsync(pie);
        }

        public async Task ClearCartAsync()
        {
            await _shoppingCartRepository.ClearCartAsync();
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            return await _shoppingCartRepository.GetShoppingCartItemsAsync();
        }

        public async Task<decimal> GetShoppingCartTotalAsync()
        {
            return await _shoppingCartRepository.GetShoppingCartTotalAsync();
        }
    }
}
