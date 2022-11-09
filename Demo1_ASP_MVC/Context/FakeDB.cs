using Demo1_ASP_MVC.Models;
using Demo1_ASP_MVC.Models.ContactModels;

namespace Demo1_ASP_MVC.Context
{
    public static class FakeDB
    {
        public static List<User> Users { get; set; } = new List<User>()
        {
            new User(1,"test1@mail.com","toto","Test1234="),
            new User(2,"test2@mail.com","riri","Test1234="),
            new User(3,"test3@mail.com","fifi","Test1234="),
            new User(4,"test4@mail.com","loulou","Test1234="),
            new User(5,"test5@mail.com","lolo","Test1234="),
        };


        public static List<Product> Products { get; set; } = new List<Product>()
        {
            new Product("Pomme"),
            new Product("Poire"),
            new Product("Banane"),
            new Product("Mangue")

        };


        public static List<Contact> Contacts { get; set; } = new List<Contact>()
        {
           // new Contact(1,"Benjamin","Sterckx","Ben","ben@mail.com","0495/123456",new DateTime(1980,12,10))

        };

        public static List<MtmFollower> Followers { get; set; } = new List<MtmFollower>()
        {
            // new Contact(1,"Benjamin","Sterckx","Ben","ben@mail.com","0495/123456",new DateTime(1980,12,10))

        };


    }

    
}
