using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using ProgramlamaYazProje.Languages;
using ProgramlamaYazProje.Models;
using System.Text;

namespace ProgramlamaYazProje.Controllers
{
    public class ContactController : Controller
    {
        public ContactController()
        {
        }

        // GET: ContactController1
        public ActionResult Index()
        {
            return View();
        }

        // POST: ContactController1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Contact contact)
        {
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(contact);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:7187/api/Contact/AddContact", content);
            }
            return View();
        }
    }
}
