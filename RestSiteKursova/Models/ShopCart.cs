using Microsoft.EntityFrameworkCore;
using RestSiteKursova.Data;

namespace RestSiteKursova.Models
{
    public class ShopCart
    {
        private readonly AppDBContent _db;
        public ShopCart(AppDBContent db)
        {
            _db = db;
        }

        public string ShopCartId { get; set; }
        public List<ShopCartItem> listShopItems { get; set; }

        public static ShopCart GetAllCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var cont = services.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shopCartId);
            return new ShopCart(cont) { ShopCartId = shopCartId };

        }

        public void AddInCart(MenuItem item)
        {
            _db.ShopCartItems.Add(new ShopCartItem { ShopCartId = ShopCartId, item = item, price = item.Price });

            _db.SaveChanges(); 
        }
        
        public List<ShopCartItem> getItem()
        {
            return _db.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).Include(s => s.item).ToList();
        }

        
    }
}
