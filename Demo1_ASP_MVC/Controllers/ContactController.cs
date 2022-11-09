
using System.Text.Json.Serialization;
using Demo1_ASP_MVC.Models.ContactModels;
using Demo1_ASP_MVC.Models.ViewModels.ContactVM;
using Demo1_ASP_MVC.Service;
using Microsoft.AspNetCore.Mvc;


namespace Demo1_ASP_MVC.Controllers
{
    public class ContactController : Controller
    {

        private readonly string BASEAPI = "http://localhost:5201/api/"; //"https://localhost:7201/api/";

        private readonly SessionManager _sessionManager;
       

        public ContactController(SessionManager sessionManager )
        {
            _sessionManager = sessionManager;
        
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(ListContact));
        }

        public IActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactVM form)
        {

            if (!ModelState.IsValid)
                return View(form);
            
            var contact = new 
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Birthdate = form.Birthdate,
                UserId = _sessionManager.Id
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASEAPI);
            var reponse = await client.PostAsJsonAsync("Contact/Insert", contact);
            if (reponse.IsSuccessStatusCode)
            {
                Contact result = await reponse.Content.ReadFromJsonAsync<Contact>();
            }
            return RedirectToAction(nameof(ListContact));
        }


        public async Task<IActionResult> ListContact()
        {

           List<Contact> contacts = new List<Contact>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASEAPI);
            var reponse = await client.GetAsync("Contact/GetAll");
            if (reponse.IsSuccessStatusCode)
            {
                contacts = await reponse.Content.ReadFromJsonAsync<List<Contact>>();
            }

            ListContactVM listContacts = new ListContactVM(contacts.OrderBy(c => c.Id).ToList());
           
            return View(listContacts);
        }
        

        public async Task<IActionResult> DeleteContact(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASEAPI);
            var reponse = await client.DeleteAsync($"Contact/Delete/{id}");
            if (reponse.IsSuccessStatusCode)
            {
                int nbRows = await reponse.Content.ReadFromJsonAsync<int>();
            }
            
            return RedirectToAction(nameof(ListContact));
        }

        public async Task<IActionResult> ModifyContact(int id)
        {
            AddContactVM contact = new AddContactVM();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASEAPI);
            var reponse = await client.GetAsync($"Contact/GetById/{id}");
            if (reponse.IsSuccessStatusCode)
            {
                contact = await reponse.Content.ReadFromJsonAsync<AddContactVM>();

            }

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyContact(AddContactVM form)
        {
            if(!ModelState.IsValid) return View(form);

            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASEAPI);
            var reponse = await client.PutAsJsonAsync($"Contact/Update/{form.Id}", form);
            if (reponse.IsSuccessStatusCode)
            {
                int result = await reponse.Content.ReadFromJsonAsync<int>();

            }

            return RedirectToAction(nameof(ListContact));
        }

       
       



    }
}
