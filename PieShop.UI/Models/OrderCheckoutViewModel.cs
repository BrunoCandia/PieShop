using PieShop.Models.Order;

namespace PieShop.UI.Models
{
    public class OrderCheckoutViewModel
    {
        public Order Order { get; set; }

        //TODO: this is neede to avoid exception when validation
        public OrderCheckoutViewModel() { }

        //TODO: review if this is needed
        public OrderCheckoutViewModel(Order order)
        {
            Order = order;
        }
    }
}
