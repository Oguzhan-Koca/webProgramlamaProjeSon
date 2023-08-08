using Microsoft.AspNetCore.Mvc;
using ProgramlamaYazProje.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Localization;
using ProgramlamaYazProje.Languages;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ProgramlamaYazProje.Controllers
{
    [Authorize(Roles = "member, admin")]
    public class AnimalController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnimalController(UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
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
            return View(animals);
        }

        public async Task<IActionResult> Update_Insert(int? id)
        {
            AnimalViewModel obj = new();

            List<Category> categories;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7187/api/Category"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }

            obj.CategoryList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            if (id == null)
            {
                return View(obj);
            }
            //düzenleme

            Animal animal;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7187/api/Animal/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    animal = JsonConvert.DeserializeObject<Animal>(apiResponse);
                }
            }

            obj.Animal = animal;

            if (obj.Animal == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update_Insert(AnimalViewModel obj, IFormFile image)
        {
            if (obj.Animal.Id == 0)
            {
                //Create (Oluşturma)
                obj.Animal.Status = "BarinagaBagisBeklemede";
                var user = await _userManager.GetUserAsync(User);
                obj.Animal.UserGuid = user.Id;
                obj.Animal.UserName = user.UserName;

                if (image != null)
                {
                    //Save image to wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    string extension = Path.GetExtension(image.FileName);
                    string newName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/img/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    using (Image img = Image.Load(path))
                    {
                        int width = 750;
                        int height = 500;
                        img.Mutate(x => x.Resize(width, height));

                        img.Save(path);
                    }

                    string sqlPath = Path.Combine("/img/", newName);
                    obj.Animal.PhotoURL = sqlPath;
                }

                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(obj.Animal);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("https://localhost:7187/api/Animal/AddAnimal", content);
                }
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(obj.Animal);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("https://localhost:7187/api/Animal/UpdateAnimal", content);
                }
            }

            AnimalViewModel newObj = new();

            List<Category> categories;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7187/api/Category"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }

            newObj.CategoryList = categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(newObj);
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7187/api/Animal/DeleteAnimal/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
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
            return View(animal);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(Animal data)
        {
            int id = data.Id;
            Animal animal;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7187/api/Animal/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    animal = JsonConvert.DeserializeObject<Animal>(apiResponse);
                }
            }

            var user = await _userManager.GetUserAsync(User);
            animal.UserGuid = user.Id;
            animal.UserName = user.UserName;
            animal.Status = "SahiplendirmeBeklemede";

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(animal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:7187/api/Animal/UpdateAnimal", content);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

