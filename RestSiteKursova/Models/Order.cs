using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RestSiteKursova.Models
{
    public class Order
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Поле не має бути порожнім")]
        [Display(Name = "Ім'я")]
        public string name { get; set; }
        [Required(ErrorMessage = "Поле не має бути порожнім")]
        [Display(Name = "Прізвище")]
        public string surname { get; set; }
        [Required(ErrorMessage ="Поле не має бути порожнім")]
        [Display(Name = "Адреса")]
        public string adres { get; set; }
        [Required(ErrorMessage = "Поле не має бути порожнім")]
        [Display(Name = "Номер телефону")]
        [StringLength(13, ErrorMessage = "Поле {0} має мати мінімум {2} символів.", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber)]
        public string phone{ get; set; }
        public DateTime ordertime { get; set; }

        public List<OrderDetail> ordersDetail { get; set; }

        [Display(Name = "Статус")]
        public string status { get; set; }

        [Display(Name ="Пошта")]
        public string email { get; set; }
    }
}
