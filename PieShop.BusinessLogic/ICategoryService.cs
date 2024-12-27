using PieShop.Models.Category;

namespace PieShop.BusinessLogic
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
