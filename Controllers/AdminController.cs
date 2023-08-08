using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using ProgramlamaYazProje.Languages;
using ProgramlamaYazProje.Models;
using System.Data;
using System.Text;

namespace ProgramlamaYazProje.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        // GET: AdminController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Wait()
        {
            List<Animal> animals;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7187/api/Animal"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
                }
            }

            animals = animals.Where(a => a.Status == "SahiplendirmeBeklemede" || a.Status == "BarinagaBagisBeklemede").ToList();
            
            ApplyViewModel applyViewModel = new ()
            {
                Animals = animals
            };
            return View(applyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Wait(string id, string control)
        {
            Animal animal;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7187/api/Animal/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    animal = JsonConvert.DeserializeObject<Animal>(apiResponse);
                }
            }

            if(animal.Status == "SahiplendirmeBeklemede") // Kişiye sahiplendirme
            {
                if(control == "tick")
                {
                    animal.Status = "Sahipli";
                }
                else
                {
                    animal.Status = "Barinakta";
                    animal.UserGuid = null;
                    animal.UserName = null;
                }
            }
            else if(animal.Status == "BarinagaBagisBeklemede") // Barınağa bağış
            {
                if (control == "tick")
                {
                    animal.Status = "Barinakta";
                    animal.UserGuid = null;
                    animal.UserName = null;
                }
                else
                {
                    animal.Status = "Red";
                }
            }

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(animal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:7187/api/Animal/UpdateAnimal", content);
            }

            List<Animal> animals;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7187/api/Animal"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
                }
            }

            animals = animals.Where(a => a.Status == "SahiplendirmeBeklemede" || a.Status == "BarinagaBagisBeklemede").ToList();

            ApplyViewModel applyViewModel = new()
            {
                Animals = animals
            };

            return View(applyViewModel);
        }
    }
}
