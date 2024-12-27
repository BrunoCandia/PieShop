namespace PieShop.Models.Category
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<Pie.Pie>? Pies { get; set; }
    }
}
