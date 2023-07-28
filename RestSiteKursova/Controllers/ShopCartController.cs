using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSiteKursova.Data;
using RestSiteKursova.Data.Interfaces;
using RestSiteKursova.Models;
using System.ComponentModel.Design;
namespace RestSiteKursova.Controllers
{
    [Authorize]

    public class ShopCartController : Controller
    {
        private readonly AppDBContent _db;
        private readonly ShopCart _shopcart;

        public ShopCartController(AppDBContent db,ShopCart shopCart)
        {
            _db = db;
            _shopcart = shopCart;
        }
        public ViewResult Index()
        {
            var items = _shopcart.getItem();
            _shopcart.listShopItems = items;

            var obj= new ShopCartViewModel
            {
                shopCart = _shopcart
            };

            return View(obj);
        }
        public RedirectToActionResult AddedToCart(int id)
        {
            var item = _db.MenuItems.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                _shopcart.AddInCart(item);
            }
            return RedirectToAction("Index");

        }
        public RedirectToActionResult DeleteOnCart(int id)
        {
            var item=_db.ShopCartItems.Find(id);
            if (item!= null)
            {
                _db.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public RedirectToRouteResult ReadyToPayment(int id)
        {
            var item = _db.MenuItems.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                _shopcart.AddInCart(item);
            }
            return RedirectToRoute(new { controller = "Order", action = "Index" });
        }
    }
}
