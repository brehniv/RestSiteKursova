using Microsoft.EntityFrameworkCore;
using RestSiteKursova.Models;

namespace RestSiteKursova.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {
     
        }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ShopCartItem> ShopCartItems { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }

}
