using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PieShop.DataAccess.Data.Entitites.Order
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

        public class OrderDetailEntity : IEntityTypeConfiguration<OrderDetail>
        {
            public void Configure(EntityTypeBuilder<OrderDetail> builder)
            {
                builder.HasKey(p => p.OrderDetailId);
                builder.Property(p => p.OrderDetailId).HasDefaultValueSql("NEWSEQUENTIALID()");
                builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            }
        }
    }
}
