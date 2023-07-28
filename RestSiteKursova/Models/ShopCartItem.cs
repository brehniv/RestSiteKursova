using System.ComponentModel.DataAnnotations;

namespace RestSiteKursova.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }
        public MenuItem item { get; set; }
        public int price { get; set; }

        public string ShopCartId { get; set; }
    }
}
