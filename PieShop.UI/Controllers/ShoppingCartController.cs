using Microsoft.AspNetCore.Mvc;
using PieShop.BusinessLogic;
using PieShop.UI.Models;

namespace PieShop.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieService _pieService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IPieService pieService, IShoppingCartService shoppingCartService)
        {
            _pieService = pieService;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: review https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
            var items = await _shoppingCartService.GetShoppingCartItemsAsync();
            var total = await _shoppingCartService.GetShoppingCartTotalAsync();

            var shoppingCartViewModel = new ShoppingCartViewModel(items, total);

            return View(shoppingCartViewModel);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(Guid pieId)
        {
            var selectedPie = await _pieService.GetPieByPieIdAsync(pieId);

            if (selectedPie != null)
            {
                await _shoppingCartService.AddToCartAsync(selectedPie);
            }

            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(Guid pieId)
        {
            var selectedPie = await _pieService.GetPieByPieIdAsync(pieId);

            if (selectedPie != null)
            {
                await _shoppingCartService.RemoveFromCartAsync(selectedPie);
            }

            return RedirectToAction("Index");
        }
    }
}
