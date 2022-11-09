namespace Demo1_ASP_MVC.Models
{
    public class User
    {
        public User(int id, string email, string userName, string password)
        {
            Id = id;
            Email = email;
            UserName = userName;
            Password = password;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
