using System.ComponentModel.DataAnnotations;

namespace RestSiteKursova.Models
{
    public class EditModel
    {
        [Display(Name ="Рік народження")]
        public int Year { get; set; }
        [Display(Name = "Пошта")]

        public string Email { get; set; }

    }
}
