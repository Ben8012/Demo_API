using Demo1_ASP_MVC.Context;
using Demo1_ASP_MVC.Models;
using Demo1_ASP_MVC.Models.ViewModels;
using Demo1_ASP_MVC.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Demo1_ASP_MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly SessionManager _sessionManager;

        public LoginController(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
           
        }

        [HttpPost]
        public IActionResult Login(AuthLoginFormVM form)
        {
            User? user = FakeDB.Users.SingleOrDefault(u => u.UserName == form.LoginName,null);

            if (user == null)
                ModelState.AddModelError(nameof(form.LoginName), "l'utilisateur n'existe pas");
            else{
                if (user.Password != form.Password)
                {
                    ModelState.AddModelError(nameof(form.Password), "le mot de passe ne correspond pas !!!");
                }
                
                ValidatePassword(form);
                            
            }
            
            if (!ModelState.IsValid)
                return View();

            // ici remplir la session
            _sessionManager.Id = user.Id;
            _sessionManager.Email = user.Email;
            _sessionManager.UserName = user.UserName;
            return RedirectToAction("Index", "Home");
            
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            FakeDB.Users.Add(new User(
                FakeDB.Users.Last().Id + 1,
                registerViewModel.Email,
                registerViewModel.UserName,
                registerViewModel.Password));
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            _sessionManager.Clear();
            return RedirectToAction("Index", "Home");
        }




        private void ValidatePassword(AuthLoginFormVM form)
        {
            string password = form.Password;
            string min_pattern = @"[a-z]+";
            string maj_pattern = @"[A-Z]+";
            string numb_pattern = @"[0-9]+";
            string symb_pattern = @"[@\-+=#_]+";
            if (!Regex.IsMatch(password, min_pattern, RegexOptions.None))
                ModelState.AddModelError(nameof(form.Password), "Le mot de passe ne contient pas de minuscule.");
            if (!Regex.IsMatch(password, maj_pattern, RegexOptions.None))
                ModelState.AddModelError(nameof(form.Password), "Le mot de passe ne contient pas de majuscule.");
            if (!Regex.IsMatch(password, numb_pattern, RegexOptions.None))
                ModelState.AddModelError(nameof(form.Password), "Le mot de passe ne contient pas de chiffre.");
            if (!Regex.IsMatch(password, symb_pattern, RegexOptions.None))
                ModelState.AddModelError(nameof(form.Password), "Le mot de passe ne contient pas de symbole.");
        }
    }
}
