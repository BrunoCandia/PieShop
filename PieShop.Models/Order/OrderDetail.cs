namespace PieShop.Models.Order
{
    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public Guid PieId { get; set; }
        public Pie.Pie Pie { get; set; } = default!;
    }
}
