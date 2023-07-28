using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSiteKursova.Data;
using RestSiteKursova.Models;

namespace RestSiteKursova.Controllers
{
    [Authorize]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AppDBContent _db;

        public AdminController(AppDBContent db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeMenu()
        {
            IEnumerable<MenuItem> menu = _db.MenuItems;
            return View(menu);
        }
        
        public IActionResult Edit(int id)
        {
            var itemfromDB = _db.MenuItems.Find(id);
            return View(itemfromDB);
        }
        [HttpPost]
        public IActionResult Edit(MenuItem item)
        {

            if (ModelState.IsValid)
            {
                _db.MenuItems.Update(item);
                _db.SaveChanges();
                return RedirectToAction("ChangeMenu");
            }
            return View(item);
        }
        public IActionResult AddNewItem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewItem(MenuItem item)
        {

            if (ModelState.IsValid)
            {
                _db.MenuItems.Add(item);
                _db.SaveChanges();
                return RedirectToAction("ChangeMenu");
            }
            return View(item);
        }
        public IActionResult Delete(int id)
        {
            MenuItem itemtodelete=_db.MenuItems.Find(id);
            _db.MenuItems.Remove(itemtodelete);
            _db.SaveChanges();
            return RedirectToAction("ChangeMenu");
        }
        public IActionResult AllOrders()
        {
            IEnumerable<Order> orders = _db.Order;
            return View(orders);
        }

        public IActionResult DeleteOrder(int id)
        {
            Order order=  _db.Order.Find(id);
            if (order!= null)
            {
                _db.Order.Remove(order);
                _db.SaveChanges();
            }
            return RedirectToAction("AllOrders");
        }
    }
}
