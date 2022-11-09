using Microsoft.AspNetCore.Mvc;

namespace Demo1_ASP_MVC.Controllers
{
    public class MathController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Math/TableCroise")]
        public IActionResult TableauCroise()
        {
            return View();
        }

        [Route("Math/Carre/{nombre}")]
        [Route("Math/{nombre}/Carre")]
        public string Carre(int nombre)
        {
            return $"{nombre}² = {nombre * nombre}";
        }

        [Route("Math/Multiple/{nombre}")]
        [Route("Math/Table/{nombre}")]
        public string[] Multiplication(int nombre)
        {
            string [] results = new string [10];
            for (int i = 0; i < results.Length; i++)
            {
                int result = (i+1)*nombre;
                results[i] = $" {i+1} * {nombre} = {result}";
            }
            return results;
        }

        [Route("Math/Divison/{nombre1}/{nombre2}")]
        [Route("Math/Divison")]
        [Route("Math/{nombre1}/diviséPar/{nombre2}")]
        public string Division(int nombre1, int nombre2)
        {
            if (nombre2 == 0)
            {
                return "Vers l'infini";
            }
            else
            {
                return $"{nombre1} / {nombre2} = {(double)nombre1 / nombre2}";
            }
        }
    }
}
