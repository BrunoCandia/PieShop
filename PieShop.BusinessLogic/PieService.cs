using PieShop.DataAccess.Repositories;
using PieShop.Models.Pie;

namespace PieShop.BusinessLogic
{
    public class PieService : IPieService
    {
        private readonly IPieRepository _pieRepository;

        public PieService(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public async Task<Pie?> GetPieByPieIdAsync(Guid pieId)
        {
            return await _pieRepository.GetPieByPieIdAsync(pieId);
        }

        public async Task<IEnumerable<Pie>> GetAllPiesAsync()
        {
            return await _pieRepository.GetAllPiesAsync();
        }

        public async Task<IEnumerable<Pie>> GetPiesOfTheWeekAsync()
        {
            return await _pieRepository.GetPiesOfTheWeekAsync();
        }

        public async Task<IEnumerable<Pie>> SearchPiesAsync(string searchQuery)
        {
            return await _pieRepository.SearchPiesAsync(searchQuery);
        }
    }
}
