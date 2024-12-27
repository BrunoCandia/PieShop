using Microsoft.AspNetCore.Mvc;
using PieShop.BusinessLogic;
using PieShop.Models.Pie;
using PieShop.UI.Models;

namespace PieShop.UI.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieService _pieService;
        private readonly ICategoryService _categoryService;

        public PieController(ICategoryService categoryService, IPieService pieService)
        {
            _categoryService = categoryService;
            _pieService = pieService;
        }

        ////public async Task<ActionResult> List()
        ////{
        ////    var pies = await _pieService.GetAllPiesAsync();

        ////    var pieListViewModel = new PieListViewModel(pies, "All Pies");

        ////    return View(pieListViewModel);
        ////}

        public async Task<ViewResult> List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;

            if (string.IsNullOrWhiteSpace(category))
            {
                pies = await _pieService.GetAllPiesAsync();
                currentCategory = "All pies";
            }
            else
            {
                pies = await _pieService.GetAllPiesAsync();
                pies = pies.Where(p => p.Category.Name == category).OrderBy(p => p.PieId); //TODO: create a method to do this!!!

                var categories = await _categoryService.GetAllCategoriesAsync(); //TODO: create a method to do this!!!
                currentCategory = categories.FirstOrDefault(c => c.Name == category)?.Name;
            }

            return View(new PieListViewModel(pies, currentCategory));
        }

        public async Task<ActionResult> Detail(Guid pieId)
        {
            if (pieId == Guid.Empty)
            {
                return BadRequest();
            }

            var pie = await _pieService.GetPieByPieIdAsync(pieId);

            if (pie is null)
            {
                return NotFound();
            }

            var pieDetailViewModel = new PieDetailViewModel(pie);

            return View(pieDetailViewModel);
        }

        public async Task<IActionResult> Search(string searchQuery)
        {
            IEnumerable<Pie> pies = Enumerable.Empty<Pie>();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                ViewData["ErrorMessage"] = "Please enter a search term.";
                ViewData["SearchPerformed"] = false;

                var emptyPieSearchViewModel = new PieSearchViewModel(pies);

                return View(emptyPieSearchViewModel);
            }

            ViewData["CurrentFilter"] = searchQuery;
            ViewData["SearchPerformed"] = true;

            pies = await _pieService.SearchPiesAsync(searchQuery);

            var pieSearchViewModel = new PieSearchViewModel(pies);

            return View(pieSearchViewModel);
        }
    }
}
