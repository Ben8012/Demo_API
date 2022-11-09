using System.ComponentModel.DataAnnotations;

namespace Demo1_ASP_MVC.Models.ViewModels
{
    public class AuthLoginFormVM
    {
        [Display(Name = "Nom d'utilisateur")]
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire")]
        [MinLength(2, ErrorMessage = "Le nom d'utilisateur n'a pas assez de caractères")]
        [MaxLength(16, ErrorMessage = "Le nom d'utilisateur a trop de caractères")]
        public string LoginName { get; set;}

        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [MinLength(8, ErrorMessage = "Le mot de passe n'a pas assez de caractères")]
        [MaxLength(32, ErrorMessage = "Le mot de passe a trop de caractères")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
