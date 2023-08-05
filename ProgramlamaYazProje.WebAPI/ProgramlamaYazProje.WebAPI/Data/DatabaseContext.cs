using Microsoft.EntityFrameworkCore;
using ProgramlamaYazProje.Models;

namespace ProgramlamaYazProje
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
    }
}
