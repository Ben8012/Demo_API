namespace Demo1_ASP_MVC.Models.ViewModels
{
    public class ListViewModel
    {
        public ListViewModel(List<Product> products)
        {
            _products = products;
        }

        private List<Product> _products;

        public Product[] Products { get => _products?.ToArray() ?? new Product[0]; }

        public int NbProduit { get => _products.Count(); }

        public void AddProduct(Product newProduct)
        {
            if (newProduct == null) throw new ArgumentNullException(nameof(newProduct));
            if (_products == null) _products = new List<Product>();
            if (!_products.Contains(newProduct)) _products.Add(newProduct);
           
        }
    }
}
