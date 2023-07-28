namespace RestSiteKursova.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int orderId { get; set; }

        public int ItemID { get; set; }
        public int price { get; set; }
        public virtual MenuItem item { get; set; }
        public virtual Order order{ get; set; }
    }
}
