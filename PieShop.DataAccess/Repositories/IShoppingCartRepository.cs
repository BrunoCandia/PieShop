using PieShop.Models.Pie;
using PieShop.Models.ShoppingCart;

namespace PieShop.DataAccess.Repositories
{
    public interface IShoppingCartRepository
    {
        Task AddToCartAsync(Pie pie);
        Task<int> RemoveFromCartAsync(Pie pie);
        Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync();
        Task ClearCartAsync();
        Task<decimal> GetShoppingCartTotalAsync();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
