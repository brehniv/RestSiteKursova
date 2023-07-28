using System.ComponentModel.DataAnnotations;

namespace RestSiteKursova.Models
{
    public class NotReadyOrder
    {
        public int id { get; set; }
        [Display(Name ="Статус замовлення")]
        public string status { get; set; }
    }
}
