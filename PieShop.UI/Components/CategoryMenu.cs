using Microsoft.AspNetCore.Mvc;
using PieShop.BusinessLogic;

namespace PieShop.UI.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryMenu(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return View(categories);
        }
    }
}
