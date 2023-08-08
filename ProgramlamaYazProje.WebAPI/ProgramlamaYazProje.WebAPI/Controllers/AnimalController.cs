using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramlamaYazProje.Models;

namespace ProgramlamaYazProje.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AnimalController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Animal
        [HttpGet]
        public IEnumerable<Animal> Details()
        {
            var animals = _context.Animals.Include(a => a.Category).ThenInclude(c => c.Animals).ToList();
            return animals;
        }

        // GET: api/Animal/id
        [HttpGet("{id}")]
        public Animal Details(int id)
        {
            var animals = _context.Animals.Include(a => a.Category).ThenInclude(c => c.Animals).AsQueryable();
            return animals.Where(a => a.Id == id).FirstOrDefault();
        }

        // GET: api/Animal/GetAnimalByKey/category
        [HttpGet("GetAnimalByKey/{key}")]
        public List<Animal> Details(string? key)
        {
            var animals = _context.Animals.Include(a => a.Category).ThenInclude(c => c.Animals).AsQueryable();
            return animals.Where(a => a.Category.Name.ToLower() == key.ToLower()).ToList();
        }

        // POST: api/Animal/AddAnimal
        [HttpPost("AddAnimal")]
        public void Create(Animal animal)
        {
            try
            {
                _context.Add(animal);
                _context.SaveChanges();
                Console.WriteLine("Hayvan eklendi");
            }
            catch
            {
                Console.WriteLine("Hayvan eklenemedi");
            }
        }

        // POST: api/Animal/UpdateAnimal
        [HttpPost("UpdateAnimal")]
        public void Edit(Animal animal)
        {
            try
            {
                animal.Category = null;
                _context.Update(animal);
                _context.SaveChanges();
                Console.WriteLine("Hayvan güncellendi");
            }
            catch
            {
                Console.WriteLine("Hayvan güncellenemedi");
            }
        }

        // POST: api/Animal/DeleteAnimal
        [HttpPost("DeleteAnimal")]
        public void Delete(Animal animal)
        {
            try
            {
                _context.Update(animal);
                _context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Hayvan silinemedi");
            }
        }

        // GET: api/Animal/id
        [HttpGet("DeleteAnimal/{id}")]
        public void Delete(int id)
        {
            Animal animal = _context.Animals.Where(a => a.Id == id).FirstOrDefault();
            _context.Animals.Remove(animal);
        }
    }
}
