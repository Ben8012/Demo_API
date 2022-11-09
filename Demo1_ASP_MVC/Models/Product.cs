namespace Demo1_ASP_MVC.Models
{
    public class Product
    {

        public Product(string nom )
        {
            Random ran = new Random();
            Prix = ran.Next(1, 10);

            Random ran2 = new Random();
            Stock = ran2.Next(1, 20);

            Nom = nom;
        }
        public string Nom { get; set; }
        public decimal Prix { get; private set; }
        public int Stock { get; private set; }
      
    }
}
