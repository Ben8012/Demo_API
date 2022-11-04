﻿using System.ComponentModel.DataAnnotations;

namespace Demo_API.Models.Forms
{
    public class UpdateContactForm
    {
        [Required]
        [EmailAddress]
        [MinLength(1)]
        [MaxLength(384)]
        public string Email { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
    }
}
