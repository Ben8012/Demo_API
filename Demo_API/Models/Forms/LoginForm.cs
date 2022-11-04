using System.ComponentModel.DataAnnotations;

namespace Demo_API.Models.Forms
{
    public class LoginForm
    {
        [Required]
        [EmailAddress]
        [MinLength(1)]
        [MaxLength(384)]
        public string Email { get; set; }
      

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
