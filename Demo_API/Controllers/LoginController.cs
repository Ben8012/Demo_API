using Demo_API.Models;
using Demo_API.Models.Forms;
using Demo_API.Models.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("Login")]
        public IActionResult Login(LoginForm form)
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate FROM [User] WHERE Email = @Email AND Password = @Password", false);
            command.AddParameter("Email", form.Email);
            command.AddParameter("Password", form.Password);

            try
            {
                return Ok(_connection.ExecuteReader(command, dr => dr.ToUser()).SingleOrDefault());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        

    }
}
