using Demo_API.Models;
using Demo_API.Models.Forms;
using Demo_API.Models.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace Demo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Connection _connection;
        private readonly ILogger _logger;

        public LoginController(ILogger<ContactController> logger, Connection connection)
        {
            _connection = connection;
            _logger = logger;
        }


        /// <summary>
        /// pour se connecter
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginForm form)
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate FROM [User] WHERE Email = @Email AND Password = @Password", false);
            command.AddParameter("Email", form.Email);
            command.AddParameter("Password", form.Password);

            try
            {
                User? user = _connection.ExecuteReader(command, dr => dr.ToUser()).SingleOrDefault();

                if(user == null)
                {
                   return BadRequest(new { Message = "Email ou mot de passe incorrect "});
                   // return Problem("L'email incorrect", statusCode: (int)HttpStatusCode.BadRequest);
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

    }
}
