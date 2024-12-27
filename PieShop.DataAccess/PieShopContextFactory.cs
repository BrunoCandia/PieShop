using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PieShop.DataAccess
{
    public class PieShopContextFactory : IDesignTimeDbContextFactory<PieShopContext>
    {
        public PieShopContext CreateDbContext(string[] args)
        {
            var configuration = GetConfiguration();

            return CreateDbContext(configuration);
        }

        public static IConfiguration GetConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, true);

            return configurationBuilder.Build();
        }

        public static PieShopContext CreateDbContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PieShopContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new PieShopContext(optionsBuilder.Options);
        }
    }
}
