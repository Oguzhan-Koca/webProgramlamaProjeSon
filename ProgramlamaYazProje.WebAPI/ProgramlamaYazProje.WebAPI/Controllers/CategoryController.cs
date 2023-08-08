using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramlamaYazProje.Models;

namespace ProgramlamaYazProje.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoryController(DatabaseContext context)
        {
            _context = context;
        }
        
        // GET: api/Category
        [HttpGet]
        public IEnumerable<Category> Details()
        {
            return _context.Categories.ToList();
        }

        // GET: api/Category/id
        [HttpGet("{id}")]
        public Category Details(int id)
        {
            return _context.Categories.Where(c => c.Id == id).Include(c => c.Animals).ThenInclude(a => a.Category).FirstOrDefault();
        }

        // POST: api/Category/AddCategory
        [HttpPost("AddCategory")]
        public void Create(Category category)
        {
            try
            {
                _context.Add(category);
                _context.SaveChanges();
                Console.WriteLine("Kategori eklendi");
            }
            catch
            {
                Console.WriteLine("Kategori eklenemedi");
            }
        }

        // POST: api/Category/UpdateCategory
        [HttpPost("UpdateCategory")]
        public void Edit(Category category)
        {
            try
            {
                _context.Update(category);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Kategori güncellenemedi");
            }
        }

        // POST: api/Category/DeleteCategory
        [HttpPost("DeleteCategory")]
        public void Delete(Category category)
        {
            try
            {
                _context.Update(category);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Kategori silinemedi");
            }
        }

        // GET: api/Category/id
        [HttpGet("DeleteCategory/{id}")]
        public void Delete(int id)
        {
            Category category = _context.Categories.Where(a => a.Id == id).FirstOrDefault();
            _context.Categories.Remove(category);
        }
    }
}
