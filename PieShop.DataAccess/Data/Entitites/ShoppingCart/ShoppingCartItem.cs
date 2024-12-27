using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PieShop.DataAccess.Data.Entitites.ShoppingCart
{
    public class ShoppingCartItem
    {
        public Guid ShoppingCartItemId { get; set; }
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; } //Get the value from the session.
        public Guid PieId { get; set; }
        ////public required Pie.Pie Pie { get; set; }
        public Pie.Pie Pie { get; set; } = default!;

        public class ShoppingCartItemEntity : IEntityTypeConfiguration<ShoppingCartItem>
        {
            public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
            {
                builder.HasKey(p => p.ShoppingCartItemId);
                builder.Property(p => p.ShoppingCartItemId).HasDefaultValueSql("NEWSEQUENTIALID()");
                builder.Property(p => p.ShoppingCartId).HasMaxLength(128);
            }
        }
    }
}
