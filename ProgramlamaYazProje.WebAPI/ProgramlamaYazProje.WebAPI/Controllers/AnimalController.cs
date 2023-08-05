using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        /*
        // GET: AnimalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnimalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnimalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnimalController/Create
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

        // GET: AnimalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnimalController/Edit/5
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

        // GET: AnimalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnimalController/Delete/5
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
        }*/

        // GET: api/Animals
        [HttpGet]
        public IEnumerable<Animal> GetAnimals()
        {
            var animals = _context.Animals.ToList();

            var animal = new Animal();
            animal.Name = "ccikus0";
            _context.Add(animal);

            return animals;
        }

        // GET animal/5
        [HttpGet("{id}")]
        public IActionResult GetAnimal(int id)
        {
            var animal = _context.Animals.Where(a => a.Id == id).FirstOrDefault();
            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }
    }
}
