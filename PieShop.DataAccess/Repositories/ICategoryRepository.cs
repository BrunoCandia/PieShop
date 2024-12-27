using PieShop.Models.Category;

namespace PieShop.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
