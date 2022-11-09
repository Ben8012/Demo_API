using Demo_API.Models;
using Demo_API.Models.Forms;
using Demo_API.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Tools;

namespace Demo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly Connection _connection;
        private readonly ILogger _logger;

        public UserController(ILogger<ContactController> logger, Connection connection)
        {
            _connection = connection;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate FROM [User];", false);

            try
            {
                return Ok(_connection.ExecuteReader(command, dr => dr.ToUser()).ToList());
            }
            catch (DbException ex)
            {
#if DEBUG
                return BadRequest(ex.Message);
#else
                _logger.LogError(ex,ex.Message);
                return BadRequest( new { Message = "Un probleme est survenu avec la base de donnée"});
#endif
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { Message = "Un probleme est survenu lors de la recuperation des données, contactez l'admin" });
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate FROM [User] WHERE Id = @Id;", false);
            command.AddParameter("Id", id);
            try
            {
                return Ok(_connection.ExecuteReader(command, dr => dr.ToUser()).SingleOrDefault());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Insert")]
        public IActionResult Insert(AddUserForm form)
        {
            Command command = new Command("INSERT INTO [User](LastName, FirstName, Email, Birthdate, Password) OUTPUT inserted.id VALUES(@LastName, @FirstName, @Email, @Birthdate, @Password)", false);
            command.AddParameter("LastName", form.LastName);
            command.AddParameter("FirstName", form.FirstName);
            command.AddParameter("Email", form.Email);
            command.AddParameter("Birthdate", form.Birthdate);
            command.AddParameter("Password", form.Password);

            int? id = (int?)_connection.ExecuteScalar(command); // recuperer l'id du contact inseré

            if (id.HasValue)
            {
                User user = new User()
                {
                    Id = id.Value,
                    FirstName = form.FirstName,
                    Email = form.Email,
                    Birthdate = form.Birthdate,
                    LastName = form.LastName,

                };
                return Ok(user);
            }
            else
            {
                return BadRequest(new { Message = "l'insertion a échoué" });
            }
        }


        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, UpdateUserForm form)
        {
            string sqlcommand = "";
            bool isFirstName = false;
            bool isLastName = false;
            bool isEmail = false;
            bool isBrithdate = false;

            User user = GetUserById(id);

            if (form.FirstName != user.FirstName)
            {
                sqlcommand += " FirstName = @FirstName ";
                isFirstName = true;
            }

            if (form.LastName != user.LastName)
            {
                if (isFirstName) sqlcommand += ",";
                sqlcommand += " LastName = @LastName ";
                isLastName = true;
            }

            if (form.Email != user.Email)
            {
                if (isLastName || isFirstName) sqlcommand += ",";
                sqlcommand += " Email = @Email ";
                isEmail = true;
            }

            if (form.Birthdate != user.Birthdate)
            {
                if (isEmail || isLastName || isFirstName) sqlcommand += ",";
                sqlcommand += " Birthdate = @Birthdate ";
                isBrithdate = true;
            }

            if (isLastName || isFirstName || isEmail || isBrithdate)
            {
                Command command = new Command($"UPDATE [User] SET {sqlcommand} OUTPUT inserted.id WHERE Id = @Id", false);
                command.AddParameter("Id", id);
                if (isFirstName) command.AddParameter("FirstName", form.FirstName);
                if (isEmail) command.AddParameter("Email", form.Email);
                if (isBrithdate) command.AddParameter("Birthdate", form.Birthdate);
                if (isLastName) command.AddParameter("LastName", form.LastName);

                int? resultid = (int?)_connection.ExecuteScalar(command);

                if (resultid.HasValue)
                {
                    User newUser = new User()
                    {
                        Id = resultid.Value,
                        FirstName = form.FirstName,
                        Email = form.Email,
                        Birthdate = form.Birthdate,
                        LastName = form.LastName,

                    };

                    return Ok(newUser);
                }
                else
                {
                    return BadRequest(new { Message = "l'insertion a échoué" });
                }
            }
            else
            {
                return BadRequest(new { Message = "rien n'a ete modifié" });
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Command command = new Command("DELETE FROM [User] WHERE Id=@Id", false);
            command.AddParameter("Id", id);
            try
            {
                return Ok(_connection.ExecuteNonQuery(command)); // renvois le nombre de ligne affectés

            }
            catch (DbException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { Message = "Un probleme est survenu lors de la suppression, contactez l'admin" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { Message = "Un probleme est survenu, contactez l'admin" });
            }
        }


        private User GetUserById(int id)
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate FROM [User] WHERE Id = @Id;", false);
            command.AddParameter("Id", id);
            try
            {
                return _connection.ExecuteReader(command, dr => dr.ToUser()).SingleOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }


    }
}
