
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestSiteKursova.Data;
using RestSiteKursova.Models;

namespace RestSiteKursova.Controllers
{
    [Authorize(Roles ="coureir,admin")]
    public class CourierController : Controller
    {
        private readonly AppDBContent _appDBContent;

        public CourierController(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListNotReadyOrders()
        {
            IEnumerable<Order> orders = _appDBContent.Order.Where(c => c.status!="finished" );
            return View(orders);
        }
        public IActionResult Edit(int id)
        {
            Order item= _appDBContent.Order.Find(id);
            NotReadyOrder nor= new NotReadyOrder { id=item.id,status=item.status};
            return View(nor);
        }
        [HttpPost]
        public IActionResult Edit(NotReadyOrder model)
        {
            Order item = _appDBContent.Order.Find(model.id);
            item.status=model.status;
                _appDBContent.Order.Update(item);
                _appDBContent.SaveChanges();
                return RedirectToAction("ListNotReadyOrders");
        }

    }
}
