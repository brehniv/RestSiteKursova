using Microsoft.AspNetCore.Mvc;
using RestSiteKursova.Data;
using RestSiteKursova.Data.Interfaces;
using RestSiteKursova.Models;

namespace RestSiteKursova.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDBContent _db;

        public MenuController(AppDBContent db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            IEnumerable<MenuItem> menu = _db.MenuItems;
            return View(menu);
        }
    }
}
