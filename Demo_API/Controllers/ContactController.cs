using Demo_API.Models;
using Demo_API.Models.Forms;
using Demo_API.Models.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Net;
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

        [HttpGet("GetAll/{Userid}")]
        public IActionResult GetAll(int id)
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate, SurName, Phone FROM Contact Where @UserId = UserId;", false);
            command.AddParameter("UserId", id);
            try
            {
                return Ok(_connection.ExecuteReader(command, dr => dr.ToContact()).ToList());
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
                return BadRequest(new { Message = "Un probleme est survenu, contactez l'admin" });
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(200, Type= typeof(Contact))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate , SurName, Phone FROM Contact WHERE Id = @Id;", false);
            command.AddParameter("Id", id);
            try
            {
                Contact? result = _connection.ExecuteReader(command, dr => dr.ToContact()).SingleOrDefault();
                
                return (result != null) ? Ok(result) : NotFound(new {Message = "le contact n'a pas été trouvé"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Insert")]
        public IActionResult Insert(AddContactForm form)
        {
            Command command = new Command("INSERT INTO Contact(LastName, FirstName, Email, Birthdate,UserId) OUTPUT inserted.id VALUES(@LastName, @FirstName, @Email, @Birthdate, @UserId)", false);
            command.AddParameter("LastName", form.LastName);
            command.AddParameter("FirstName", form.FirstName);
            command.AddParameter("Email", form.Email);
            command.AddParameter("Birthdate", form.Birthdate);
            command.AddParameter("UserId", form.UserId);

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
                return BadRequest(new {Message = "Un probleme est survenu, l'insertion dans la base de donnée a échouée" });
            }
        }

        private Contact GetContactById(int id)
        {
            Command command = new Command("SELECT Id, LastName, FirstName, Email, Birthdate , SurName, Phone FROM Contact WHERE Id = @Id;", false);
            command.AddParameter("Id", id);
            try
            {
                return _connection.ExecuteReader(command, dr => dr.ToContact()).SingleOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, UpdateContactForm form)
        {

            Contact contact = GetContactById(id);

            string sqlcommand = "";
            bool isFirstName = false;
            bool isLastName = false;
            bool isEmail = false;
            bool isBrithdate = false;
            bool isSurname = false;
            bool isPhone = false;

            if (form.FirstName != contact.FirstName)
            {
                sqlcommand += " FirstName = @FirstName ";
                isFirstName = true;
            }

            if (form.LastName != contact.LastName)
            {
                if (isFirstName) sqlcommand += ",";
                sqlcommand += " LastName = @LastName ";
                isLastName = true;
            }

            if (form.Email != contact.Email)
            {
                if (isLastName || isFirstName) sqlcommand += ",";
                sqlcommand += " Email = @Email ";
                isEmail = true;
            }

            if (form.Birthdate != contact.Birthdate)
            {
                if (isEmail || isLastName || isFirstName) sqlcommand += ",";
                sqlcommand += " Birthdate = @Birthdate ";
                isBrithdate = true;
            }


            if (form.SurName != contact.SurName)
            {
                if (isEmail || isLastName || isFirstName ||isBrithdate) sqlcommand += ",";
                sqlcommand += " SurName = @SurName ";
                isSurname = true;
            }

            if (form.Phone != contact.Phone)
            {
                if (isEmail || isLastName || isFirstName || isBrithdate || isSurname) sqlcommand += ",";
                sqlcommand += " Phone = @Phone ";
                isPhone = true;
            }


            if (isLastName || isFirstName || isEmail || isBrithdate || isPhone || isSurname)
            {

                Command command = new Command($"UPDATE Contact SET  {sqlcommand} WHERE Id = @Id", false);
                command.AddParameter("Id", id);
                if (isFirstName) command.AddParameter("FirstName", form.FirstName);
                if (isEmail) command.AddParameter("Email", form.Email);
                if (isBrithdate) command.AddParameter("Birthdate", form.Birthdate);
                if (isLastName) command.AddParameter("LastName", form.LastName);
                if (isSurname) command.AddParameter("SurName", form.SurName);
                if (isPhone) command.AddParameter("Phone", form.Phone);
                try
                {
                    return Ok(_connection.ExecuteNonQuery(command)); // renvois le nombre de ligne affectés
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
            else
            {
                return BadRequest(new { Message = " Il n y a rien a modifier " });
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
