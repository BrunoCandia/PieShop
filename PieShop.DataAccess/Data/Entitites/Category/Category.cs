using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PieShop.DataAccess.Data.Entitites.Category
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        ////public required string Name { get; set; }
        public string Name { get; set; } = string.Empty; //// this is translated as: Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
        public string? Description { get; set; }
        public List<Pie.Pie>? Pies { get; set; }

        public class CategoryEntity : IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.HasKey(p => p.CategoryId);
                builder.Property(p => p.CategoryId).HasDefaultValueSql("NEWSEQUENTIALID()");
                builder.Property(p => p.Name).HasMaxLength(100);
                builder.Property(p => p.Description).HasMaxLength(255);
            }
        }
    }
}
