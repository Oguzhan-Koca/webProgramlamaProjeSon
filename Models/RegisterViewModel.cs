using System.ComponentModel.DataAnnotations;

namespace ProgramlamaYazProje
{
    public class RegisterViewModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "E-posta adresi alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi formatı.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar alanı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
