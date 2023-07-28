using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestSiteKursova.Data.Interfaces;
using RestSiteKursova.Models;
using System.Security.Principal;

namespace RestSiteKursova.Data.Repository
{
    public class OrdersRepository:IAllOrders
    {
        private readonly AppDBContent _appDBContent;
        private readonly ShopCart _shopCart;
        private readonly UserManager<User> _userManager;


        public OrdersRepository(AppDBContent appDBContent, ShopCart shopCart,UserManager<User> userManager)
        {
            _appDBContent = appDBContent;
            _shopCart = shopCart;
            _userManager= userManager;
        }



        public async void createOrder(Order order,string email)
        {
            order.email = email;
            order.ordertime = DateTime.Now;
            order.status = "applied to work";
            _appDBContent.Order.Add(order);
            _appDBContent.SaveChanges();
            var items = _shopCart.listShopItems;
            foreach (var element in items)
            {
                var orderDetail = new OrderDetail()
                {
                    ItemID = element.item.Id,
                    orderId = order.id,
                    price = element.item.Price
                    
                };
                _appDBContent.OrderDetails.Add(orderDetail);
            }
            _appDBContent.SaveChanges();
        }
    }
}
