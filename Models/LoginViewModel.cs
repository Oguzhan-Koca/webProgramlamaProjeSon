using ProgramlamaYazProje.Models;
using System.ComponentModel.DataAnnotations;

namespace ProgramlamaYazProje
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-posta adresi veya kullanıcı adı alanı zorunludur.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
