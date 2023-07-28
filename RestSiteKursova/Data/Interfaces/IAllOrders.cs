using RestSiteKursova.Models;
using System.Security.Principal;

namespace RestSiteKursova.Data.Interfaces
{
    public interface IAllOrders
    {
        public void createOrder(Order o,string email); 
    }
}
