using Microsoft.EntityFrameworkCore;
using CategoryEntity = PieShop.DataAccess.Data.Entitites.Category.Category;
using CategoryModel = PieShop.Models.Category.Category;
using PieModel = PieShop.Models.Pie.Pie;

namespace PieShop.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PieShopContext _pieShopContext;

        public CategoryRepository(PieShopContext pieShopContext)
        {
            _pieShopContext = pieShopContext;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _pieShopContext.Category
                    .AsNoTracking()
                    .Select(category => new CategoryModel
                    {
                        CategoryId = category.CategoryId,
                        Name = category.Name,
                        Description = category.Description,
                    })
                    .OrderBy(category => category.Name)
                    .ToListAsync();
        }

        public async Task<int> AddCategoryAsync(CategoryModel category)
        {
            var categoryEntity = new CategoryEntity
            {
                Name = category.Name,
                Description = category.Description
            };

            bool categoryWithSameNameExist = await _pieShopContext.Category.AnyAsync(c => c.Name == category.Name);

            if (categoryWithSameNameExist)
            {
                throw new Exception("A category with the same name already exists");
            }

            await _pieShopContext.AddAsync(categoryEntity);

            return await _pieShopContext.SaveChangesAsync();
        }

        public async Task<CategoryModel?> GetCategoryByCategoryIdAsync(Guid categoryId)
        {
            //TODO: verify if pies are null

            return await _pieShopContext.Category
                .AsNoTracking()
                .Include(c => c.Pies)
                .Where(category => category.CategoryId == categoryId)
                .Select(category => new CategoryModel
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description,
                    Pies = category.Pies.Select(p => new PieModel
                    {
                        PieId = p.PieId,
                        Name = p.Name,
                        ShortDescription = p.ShortDescription,
                        LongDescription = p.LongDescription,
                        AllergyInformation = p.AllergyInformation,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl,
                        ImageThumbnailUrl = p.ImageThumbnailUrl,
                        IsPieOfTheWeek = p.IsPieOfTheWeek,
                        IsInStock = p.IsInStock
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateCategoryAsync(CategoryModel category)
        {
            bool categoryWithSameNameExist = await _pieShopContext.Category.AnyAsync(c => c.Name == category.Name && c.CategoryId != category.CategoryId);

            if (categoryWithSameNameExist)
            {
                throw new Exception("A category with the same name already exists");
            }

            var categoryToUpdate = await _pieShopContext.Category.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;

                _pieShopContext.Category.Update(categoryToUpdate);

                return await _pieShopContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The category to update can't be found.");
            }
        }

        public async Task<int> DeleteCategoryAsync(Guid categoryId)
        {
            var categoryToDelete = await _pieShopContext.Category.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            if (categoryToDelete != null)
            {
                _pieShopContext.Category.Remove(categoryToDelete);

                return await _pieShopContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The category to delete can't be found.");
            }
        }
    }
}
