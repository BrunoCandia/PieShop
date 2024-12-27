using PieShop.Models.Pie;
using PieShop.Models.ShoppingCart;

namespace PieShop.BusinessLogic
{
    public interface IShoppingCartService
    {
        Task AddToCartAsync(Pie pie);
        Task<int> RemoveFromCartAsync(Pie pie);
        Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync();
        Task ClearCartAsync();
        Task<decimal> GetShoppingCartTotalAsync();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
