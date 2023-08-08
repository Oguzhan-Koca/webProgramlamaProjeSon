using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using ProgramlamaYazProje.Languages;
using ProgramlamaYazProje.Models;
using System.Data;
using System.Net.Http;
using System.Text;

namespace ProgramlamaYazProje.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        public CategoryController()
        {
        }
        [AllowAnonymous]
        // GET: CategoriController1
        public async Task<ActionResult> Index(string? key)
        {
            if (key == null)
            {
                List<Animal> animals = new List<Animal>();

                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:7187/api/Animal"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
                        }
                    }
                }
                catch (Exception ex) { }
                animals = animals.Where(a => a.Status == "Barinakta").ToList();
                return View(animals);
            }
            else
            {
                List<Animal> animals = new List<Animal>();
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync($"https://localhost:7187/api/Animal/GetAnimalByKey/{key}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            animals = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
                        }
                    }
                }
                catch (Exception ex) { }
                animals = animals.Where(a => a.Status == "Barinakta").ToList();
                return View(animals);
            }
        }

        public async Task<IActionResult> Update_Insert(int? id)
        {

            Category obj = new();
            if (id == null)
            {
                return View(obj);
            }

            Category category;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7187/api/Category"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(apiResponse);
                }
            }

            obj = category;
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update_Insert(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    //Create (Oluşturma)
                    using (var httpClient = new HttpClient())
                    {
                        var json = JsonConvert.SerializeObject(obj);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync("https://localhost:7187/api/Category/AddCategory", content);
                    }
                }
                else
                {
                    //Update (Güncelleme)
                    using (var httpClient = new HttpClient())
                    {
                        var json = JsonConvert.SerializeObject(obj);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync("https://localhost:7187/api/Category/UpdateCategory", content);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7187/api/Category/DeleteCategory/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
