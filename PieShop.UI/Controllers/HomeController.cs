using Microsoft.AspNetCore.Mvc;
using PieShop.BusinessLogic;
using PieShop.UI.Models;
using System.Diagnostics;

namespace PieShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieService _pieService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IPieService pieService, ILogger<HomeController> logger)
        {
            _pieService = pieService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var piesOfTheWeek = await _pieService.GetPiesOfTheWeekAsync();

            var homeViewModel = new HomeViewModel(piesOfTheWeek);

            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
