using PieShop.Models.Category;

namespace PieShop.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
