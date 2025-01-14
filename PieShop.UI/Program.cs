using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PieShop.BusinessLogic;
using PieShop.DataAccess;
using PieShop.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services related to ASP.NET Core MVC to the container.
builder.Services.AddControllersWithViews();

// To use the pages realted to Identity (Login, Register) we need to add the service related to razor pages because they were done with Razor
builder.Services.AddRazorPages();

builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("PieList", policy => policy.SetVaryByRouteValue("category").Expire(TimeSpan.FromSeconds(60)));
    options.AddPolicy("PieDetail", policy => policy.SetVaryByRouteValue("pieId").Expire(TimeSpan.FromSeconds(60)));
});

builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.Configuration = "localhost:59163"; // Update the value after running Redis with Podman: Container image "docker.io/library/redis:7.4"
});

// Cached responses are stored in Redis
// https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output?view=aspnetcore-9.0
// https://redis.io/docs/latest/commands/command-getkeys/#:~:text=You%20can%20use%20COMMAND%20GETKEYS,how%20Redis%20parses%20the%20commands.
// https://www.atlassian.com/data/admin/how-to-get-all-keys-in-redis

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<PieShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity will use Entity Framework
builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<PieShopContext>();
////builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PieShopContext>();

builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IPieService, PieService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseOutputCache();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller=Home}/{action=Index}/{id?}")
////    .WithStaticAssets();

// The order matters. The middleware to support MVC routing
////app.UseEndpoints(endpoints =>
////{
////    endpoints.MapControllerRoute(
////        name: "pieList",
////        pattern: "pie/{action=List}",
////        defaults: new { controller = "pie" });

////    endpoints.MapControllerRoute(
////        name: "pieDetail",
////        pattern: "pie/{action=Detail}/{pieId}",
////        defaults: new { controller = "pie" });

////    endpoints.MapControllerRoute(
////        name: "orderCheckout",
////        pattern: "order/{action=Checkout}",
////        defaults: new { controller = "order" });

////    endpoints.MapControllerRoute(
////        name: "orderCheckoutCompleted",
////        pattern: "order/{action=CheckoutCompleted}",
////        defaults: new { controller = "order" });

////    endpoints.MapControllerRoute(
////        name: "default",
////        pattern: "{controller=Home}/{action=Index}/{id?}");
////});

app.MapControllerRoute(
        name: "pieList",
        pattern: "pie/{action=List}",
        defaults: new { controller = "pie" });

app.MapControllerRoute(
    name: "pieDetail",
    pattern: "pie/{action=Detail}/{pieId}",
    defaults: new { controller = "pie" });

app.MapControllerRoute(
    name: "orderCheckout",
    pattern: "order/{action=Checkout}",
    defaults: new { controller = "order" });

app.MapControllerRoute(
    name: "orderCheckoutCompleted",
    pattern: "order/{action=CheckoutCompleted}",
    defaults: new { controller = "order" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// To use the pages realted to Identity (Login, Register) we need to add the routing to support Razor routing
app.MapRazorPages();

await app.RunAsync();
