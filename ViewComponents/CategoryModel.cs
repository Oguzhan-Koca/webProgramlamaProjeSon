using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProgramlamaYazProje.Models;

namespace ProgramlamaYazProje.ViewComponents
{
    public class CategoryModel : ViewComponent
    {
        public CategoryModel()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7187/api/Category"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
                }
            }
            return View(categories);
        }
    }
}
