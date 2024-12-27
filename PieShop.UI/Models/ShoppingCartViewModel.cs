using PieShop.Models.ShoppingCart;

namespace PieShop.UI.Models
{
    public class ShoppingCartViewModel
    {
        public decimal ShoppingCartTotal { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCartViewModel(List<ShoppingCartItem> shoppingCartItems, decimal shoppingCartTotal)
        {
            ShoppingCartTotal = shoppingCartTotal;
            ShoppingCartItems = shoppingCartItems;
        }
    }
}
