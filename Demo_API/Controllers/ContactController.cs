using Demo_API.Models;
using Demo_API.Models.Forms;
using Demo_API.Models.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Tools;

namespace Demo_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly Connection _connection;
        private readonly ILogger _logger;

        public ContactController(ILogger<ContactController> logger ,Connection connection)
        {
            _connection = connection;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate FROM Contact;", false);

            try
            {
                return Ok(_connection.ExecuteReader(command, dr => dr.ToContact()).ToList());
            }
            catch (DbException ex)
            {
#if DEBUG
                return BadRequest(ex);
#else
                _logger.LogError(ex,ex.Message);
                return BadRequest( new { Message = "Un probleme est survenu avec la base de donnée"});
#endif
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { Message = "Un probleme est survenu, contactez l'admin" });
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate FROM Contact WHERE Id = @Id;", false);
            command.AddParameter("Id", id);
            try
            {
                return Ok(_connection.ExecuteReader(command, dr => dr.ToContact()).SingleOrDefault());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Insert")]
        public IActionResult Insert(AddContactForm form)
        {
            Command command = new Command("INSERT INTO Contact(LastName, FirstName, Email, Birthdate) OUTPUT inserted.id VALUES(@LastName, @FirstName, @Email, @Birthdate)", false);
            command.AddParameter("LastName", form.LastName);
            command.AddParameter("FirstName", form.FirstName);
            command.AddParameter("Email", form.Email);
            command.AddParameter("Birthdate", form.Birthdate);

            int? id = (int?)_connection.ExecuteScalar(command); // recuperer l'id du contact inseré

            if (id.HasValue)
            {
                Contact contact = new Contact()
                {
                    Id = id.Value,
                    FirstName = form.FirstName,
                    Email = form.Email,
                    Birthdate = form.Birthdate,
                    LastName = form.LastName,

                };
                return Ok(contact);
            }
            else
            {
                return BadRequest(new {Message = " no data insert, something wrong with database"});
            }
        }

        
        [HttpPut("Update")]
        public IActionResult Update(int id, UpdateContactForm form)
        {
            Command command = new Command("UPDATE Contact SET  Email = @Email, Birthdate = @Birthdate WHERE Id = @Id", false);
            command.AddParameter("Id", id);
            command.AddParameter("Email", form.Email);
            command.AddParameter("Birthdate", form.Birthdate);
            try
            {
                return Ok( _connection.ExecuteNonQuery(command)); // renvois le nombre de ligne affectés
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { Message = "Un probleme est survenu lors de l'insertion, contactez l'admin" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { Message = "Un probleme est survenu, contactez l'admin" });
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Command command = new Command("DELETE FROM Contact WHERE Id=@Id", false);
            command.AddParameter("Id", id);
            try
            {
                return Ok( _connection.ExecuteNonQuery(command)); // renvois le nombre de ligne affectés

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

    
    }

}
