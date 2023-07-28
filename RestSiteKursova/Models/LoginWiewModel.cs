using System.ComponentModel.DataAnnotations;

namespace RestSiteKursova.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле не має бути порожнім")]
        [Display(Name = "Пошта")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле не має бути порожнім")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
