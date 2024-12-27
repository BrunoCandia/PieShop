namespace PieShop.Models.ShoppingCart
{
    public class ShoppingCartItem
    {
        public Guid ShoppingCartItemId { get; set; }
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; }
        public Guid PieId { get; set; }
        public Pie.Pie? Pie { get; set; }
    }
}
