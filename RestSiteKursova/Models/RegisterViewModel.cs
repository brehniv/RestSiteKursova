using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RestSiteKursova.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле не має бути порожнім")]

        [Display(Name = "Пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не має бути порожнім")]
        [Display(Name = "Рік народження")]
        [Range(1000, 9999, ErrorMessage = "Введіть рік коректно")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Поле не має бути порожнім")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} має мати мінімум {2} та максимум {1} символів.", MinimumLength = 8)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Поле не має бути порожнім")]
        [Compare("Password", ErrorMessage = "Паролі не однакові")]
        [DataType(DataType.Password)]
        [Display(Name = "Повторіть пароль")]
        public string PasswordConfirm { get; set; }
        [Required]
        public bool  roles { get; set; }

    }
}
