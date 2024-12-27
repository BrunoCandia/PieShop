using Microsoft.EntityFrameworkCore;
using PieShop.Models.Category;
using PieShop.Models.Pie;

namespace PieShop.DataAccess.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly PieShopContext _pieShopContext;

        public PieRepository(PieShopContext pieShopContext)
        {
            _pieShopContext = pieShopContext;
        }

        public async Task<Pie?> GetPieByPieIdAsync(Guid pieId)
        {
            return await _pieShopContext.Pie
                .AsNoTracking()
                .Include(c => c.Category)
                .Where(pie => pie.PieId == pieId)
                .Select(pie => new Pie
                {
                    PieId = pie.PieId,
                    Name = pie.Name,
                    ShortDescription = pie.ShortDescription,
                    LongDescription = pie.LongDescription,
                    AllergyInformation = pie.AllergyInformation,
                    Price = pie.Price,
                    ImageUrl = pie.ImageUrl,
                    ImageThumbnailUrl = pie.ImageThumbnailUrl,
                    IsPieOfTheWeek = pie.IsPieOfTheWeek,
                    IsInStock = pie.IsInStock,
                    CategoryId = pie.CategoryId,
                    Category = new Category
                    {
                        CategoryId = pie.Category.CategoryId,
                        Name = pie.Category.Name
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pie>> GetAllPiesAsync()
        {
            return await _pieShopContext.Pie
                    .AsNoTracking()
                    .Include(c => c.Category)
                    .Select(pie => new Pie
                    {
                        PieId = pie.PieId,
                        Name = pie.Name,
                        ShortDescription = pie.ShortDescription,
                        LongDescription = pie.LongDescription,
                        AllergyInformation = pie.AllergyInformation,
                        Price = pie.Price,
                        ImageUrl = pie.ImageUrl,
                        ImageThumbnailUrl = pie.ImageThumbnailUrl,
                        IsPieOfTheWeek = pie.IsPieOfTheWeek,
                        IsInStock = pie.IsInStock,
                        CategoryId = pie.CategoryId,
                        Category = new Category
                        {
                            CategoryId = pie.Category.CategoryId,
                            Name = pie.Category.Name,
                            Description = pie.Category.Description
                        }
                    })
                    .ToListAsync();
        }

        public async Task<IEnumerable<Pie>> GetPiesOfTheWeekAsync()
        {
            return await _pieShopContext.Pie
                    .AsNoTracking()
                    .Include(c => c.Category)
                    .Where(pie => pie.IsPieOfTheWeek)
                    .Select(pie => new Pie
                    {
                        PieId = pie.PieId,
                        Name = pie.Name,
                        ShortDescription = pie.ShortDescription,
                        LongDescription = pie.LongDescription,
                        AllergyInformation = pie.AllergyInformation,
                        Price = pie.Price,
                        ImageUrl = pie.ImageUrl,
                        ImageThumbnailUrl = pie.ImageThumbnailUrl,
                        IsPieOfTheWeek = pie.IsPieOfTheWeek,
                        IsInStock = pie.IsInStock,
                        CategoryId = pie.CategoryId,
                        Category = new Category
                        {
                            CategoryId = pie.Category.CategoryId,
                            Name = pie.Category.Name,
                            Description = pie.Category.Description
                        }
                    })
                    .ToListAsync();
        }

        public async Task<IEnumerable<Pie>> SearchPiesAsync(string searchQuery)
        {
            return await _pieShopContext.Pie
                .AsNoTracking()
                .Where(p => p.Name.Contains(searchQuery))
                .Select(pie => new Pie
                {
                    PieId = pie.PieId,
                    Name = pie.Name,
                    ShortDescription = pie.ShortDescription,
                    LongDescription = pie.LongDescription,
                    AllergyInformation = pie.AllergyInformation,
                    Price = pie.Price,
                    ImageUrl = pie.ImageUrl,
                    ImageThumbnailUrl = pie.ImageThumbnailUrl,
                    IsPieOfTheWeek = pie.IsPieOfTheWeek,
                    IsInStock = pie.IsInStock,
                    CategoryId = pie.CategoryId,
                    Category = new Category
                    {
                        CategoryId = pie.Category.CategoryId,
                        Name = pie.Category.Name,
                        Description = pie.Category.Description
                    }
                })
                .ToListAsync();
        }
    }
}
