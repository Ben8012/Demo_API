using System.ComponentModel.DataAnnotations;

namespace Demo_API.Models.Forms
{
    public class UpdateContactForm
    {
        public int Id { get; set; }


        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Le Champ est requis.")]
        [MinLength(2, ErrorMessage = "Le prénom doit etre minimum de 2 carateres")]
        [MaxLength(24, ErrorMessage = "Le prénom doit etre maximum de 24 carateres")]
        public string FirstName { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le Champ est requis.")]
        [MinLength(2, ErrorMessage = "Le nom doit etre minimum de 2 carateres")]
        [MaxLength(24, ErrorMessage = "Le nom doit etre maximum de 24 carateres")]
        public string LastName { get; set; }

        [Display(Name = "Surnom")]
        //[Required(ErrorMessage = "Le Champ est requis.")]
        //[MinLength(2, ErrorMessage = "Le surnom doit etre minimum de 2 carateres")]
        //[MaxLength(24, ErrorMessage = "Le surnom doit etre maximum de 24 carateres")]
        public string? SurName { get; set; }

        [Display(Name = "Adresse Email")]
        [Required(ErrorMessage = "Le Champ est requis.")]
        [EmailAddress(ErrorMessage = "Adresse Email Invalide")]
        public string Email { get; set; }

        [Display(Name = "télephone")]
        //[Required(ErrorMessage = "Le Champ est requis.")]
        //[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Numero de téléphone non valide.")]
        public string? Phone { get; set; }


        [Display(Name = "Date d'anniversaire")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}
