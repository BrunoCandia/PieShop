using Microsoft.AspNetCore.Mvc;
using PieShop.BusinessLogic;
using PieShop.UI.Models;

namespace PieShop.UI.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartSummary(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // TODO: review to the async
            var items = await _shoppingCartService.GetShoppingCartItemsAsync();
            var total = await _shoppingCartService.GetShoppingCartTotalAsync();

            var shoppingCartViewModel = new ShoppingCartViewModel(items, total);

            return View(shoppingCartViewModel);
        }
    }
}
