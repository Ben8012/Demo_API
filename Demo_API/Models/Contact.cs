﻿namespace Demo_API.Models
{
#nullable disable
    public class Contact
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
#nullable enable
}
