using Demo1_ASP_MVC.Context;
using Demo1_ASP_MVC.Models;
using Demo1_ASP_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo1_ASP_MVC.Controllers
{
    public class ProductController : Controller
    {
        [ViewData]
        public string Title { get => "Shopping"; }

        

        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        public IActionResult List()
        {
            ViewData["Title2"]= ", faites votre choix :";

            ListViewModel listViewModel = new ListViewModel(FakeDB.Products);
           

            return View(listViewModel);
        }

        [HttpPost]
        public IActionResult AddProduct(string product)
        {
            FakeDB.Products.Add(new Product(product));
            return RedirectToAction("List");
        }
    }
}
