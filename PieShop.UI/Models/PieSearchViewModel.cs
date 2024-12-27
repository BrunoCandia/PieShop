using PieShop.Models.Pie;

namespace PieShop.UI.Models
{
    public class PieSearchViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }

        public PieSearchViewModel(IEnumerable<Pie> pies)
        {
            Pies = pies;
        }
    }
}
