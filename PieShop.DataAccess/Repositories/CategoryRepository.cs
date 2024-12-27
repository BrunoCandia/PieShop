using Microsoft.EntityFrameworkCore;
using PieShop.Models.Category;

namespace PieShop.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PieShopContext _pieShopContext;

        public CategoryRepository(PieShopContext pieShopContext)
        {
            _pieShopContext = pieShopContext;
        }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _pieShopContext.Category
                    .Select(category => new Category
                    {
                        CategoryId = category.CategoryId,
                        Name = category.Name,
                        Description = category.Description,
                    })
                    .OrderBy(category => category.Name);
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _pieShopContext.Category
                    .AsNoTracking()
                    .Select(category => new Category
                    {
                        CategoryId = category.CategoryId,
                        Name = category.Name,
                        Description = category.Description,
                    })
                    .OrderBy(category => category.Name)
                    .ToListAsync();
        }
    }
}
