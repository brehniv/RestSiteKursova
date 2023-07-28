using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSiteKursova.Data;
using RestSiteKursova.Data.Interfaces;
using RestSiteKursova.Models;

namespace RestSiteKursova.Controllers
{
    [Authorize]

    public class OrderController : Controller
    {
        private readonly AppDBContent _appDBContent;
        private readonly IAllOrders _allorder;
        private readonly ShopCart _shopCart;
        private readonly UserManager<User> _userManager;
        public OrderController(IAllOrders allOrders, ShopCart shopCart, UserManager<User> userManager, AppDBContent appDBContent)
        {
            _allorder = allOrders;
            _shopCart = shopCart;
            _userManager = userManager;
            _appDBContent = appDBContent;   
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Order order)
        {
            ModelState.Remove("email");
            ModelState.Remove("status");
            ModelState.Remove("ordersDetail");
                var user = await _userManager.GetUserAsync(User);
                string email = user.Id;
                _shopCart.listShopItems = _shopCart.getItem();
                if (_shopCart.listShopItems.Count == 0)
                {
                    ModelState.AddModelError("", "У вас порожня корзина");
                }
            if (ModelState.IsValid)
            {
                foreach(var item in _shopCart.listShopItems)
                {
                    if (item != null)
                    {
                        _appDBContent.Remove(item);
                    }
                }
                _appDBContent.SaveChanges();
                _allorder.createOrder(order, email);
                return View("Succesfull");
            }
            
            return View(order);
        }
        public void DeleteOnCart(int id)
        {
           
        }
        public IActionResult Succesfull()
        {
            return View();
        }
        
    }
}
