using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSiteKursova.Data;

using RestSiteKursova.Data.Interfaces;
using RestSiteKursova.Data.Repository;
using RestSiteKursova.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDBContent>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.Password.RequiredLength = 8;  
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false; 
    opts.Password.RequireUppercase = false; 
    opts.Password.RequireDigit = true; 
})
    .AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => ShopCart.GetAllCart(sp));
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddTransient<IAllOrders, OrdersRepository>();
//builder.Services.AddScoped<IMenuInterface, MockItemcs>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<AppDBContent>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddScoped(sp => ShopCart.GetAllCart(sp));
    services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
    services.AddControllersWithViews();
    //services.AddTransient<IMenuInterface, MockItemcs>();
    services.AddTransient<IAllOrders, OrdersRepository>();
    services.AddMvc();
    services.AddMemoryCache();
    services.AddSession();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
 app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
