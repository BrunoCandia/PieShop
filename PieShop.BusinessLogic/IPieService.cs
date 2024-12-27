using PieShop.Models.Pie;

namespace PieShop.BusinessLogic
{
    public interface IPieService
    {
        Task<Pie?> GetPieByPieIdAsync(Guid pieId);
        Task<IEnumerable<Pie>> GetAllPiesAsync();
        Task<IEnumerable<Pie>> GetPiesOfTheWeekAsync();
        Task<IEnumerable<Pie>> SearchPiesAsync(string searchQuery);
    }
}
