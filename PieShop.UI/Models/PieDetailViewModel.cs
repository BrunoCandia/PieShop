using PieShop.Models.Pie;

namespace PieShop.UI.Models
{
    public class PieDetailViewModel
    {
        public Pie Pie { get; set; }

        public PieDetailViewModel(Pie pie)
        {
            Pie = pie;
        }
    }
}
