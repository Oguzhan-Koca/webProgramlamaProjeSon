using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramlamaYazProje.Models;

namespace ProgramlamaYazProje.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly DatabaseContext _context;

        public ContactController(DatabaseContext context)
        {
            _context = context;
        }

        // POST: api/Contact/AddContact
        [HttpPost("AddContact")]
        public void Create(Contact contact)
        {
            try
            {
                _context.Add(contact);
                _context.SaveChanges();
                Console.WriteLine("İletişim mesajı eklendi");
            }
            catch
            {
                Console.WriteLine("İletişim mesajı eklendi");
            }
        }
    }
}
