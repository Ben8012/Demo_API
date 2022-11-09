using Microsoft.AspNetCore.Mvc;

namespace Demo1_ASP_MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Admin/new")]
        [Route("NewAdmin")]
        public IActionResult Register()
        {
            return View();
        }

        public string Bienvenu()
        {
            return "Bienvenu administrateur";
        }

        public string codeHtml()
        {
            return @"<h1>Test</h1>";
        }
    }
}
