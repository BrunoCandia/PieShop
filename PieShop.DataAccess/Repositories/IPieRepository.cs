using PieShop.Models.Pie;

namespace PieShop.DataAccess.Repositories
{
    public interface IPieRepository
    {
        Task<Pie?> GetPieByPieIdAsync(Guid pieId);
        Task<IEnumerable<Pie>> GetAllPiesAsync();
        Task<IEnumerable<Pie>> GetPiesOfTheWeekAsync();
        Task<IEnumerable<Pie>> SearchPiesAsync(string searchQuery);
    }
}
