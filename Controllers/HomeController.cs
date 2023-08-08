using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
<<<<<<< HEAD
using Microsoft.Extensions.Localization;
=======
>>>>>>> 4a39f5128a35ba87ebd43a6cc7154495540f2e91
=======
>>>>>>> 4a39f5128a35ba87ebd43a6cc7154495540f2e91
using Newtonsoft.Json;
using ProgramlamaYazProje.Languages;
using ProgramlamaYazProje.Models;
using ProgramlamaYazProje.Services;
using System.Diagnostics;

namespace ProgramlamaYazProje.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
<<<<<<< HEAD
        public HomeController()
        {
=======
=======
>>>>>>> 4a39f5128a35ba87ebd43a6cc7154495540f2e91
        //localization
        //private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _db;
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;

        public HomeController(ILogger<HomeController> logger, LanguageService localization , DatabaseContext db)
        {
            _db = db;
            _logger = logger;
            _localization = localization;
<<<<<<< HEAD
>>>>>>> 4a39f5128a35ba87ebd43a6cc7154495540f2e91
=======
>>>>>>> 4a39f5128a35ba87ebd43a6cc7154495540f2e91
        }
        //public HomeController(DatabaseContext db)
        //{
        //    _db = db;
        //}

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = _localization.Getkey("Hayvan").Value;
            var currentCulture= Thread.CurrentThread.CurrentCulture.Name;    
            List<Animal> animalList = new List<Animal>();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync("https://localhost:7187/api/Animal"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        animalList = JsonConvert.DeserializeObject<List<Animal>>(apiResponse);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            
            animalList = animalList.Where(a => a.Status == "Barinakta").ToList();
            return View(animalList);
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}