using Microsoft.EntityFrameworkCore;
using ProgramlamaYazProje.Models;

namespace ProgramlamaYazProje
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>() // Navigation
                .HasOne(a => a.Category)
                .WithMany(c => c.Animals)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired(true);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Animals)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .IsRequired(true);

            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Shelter)
                .WithMany(s => s.Animals)
                .HasForeignKey(a => a.ShelterId)
                .IsRequired(false);

            modelBuilder.Entity<Shelter>()
                .HasMany(s => s.Animals)
                .WithOne(a => a.Shelter)
                .HasForeignKey(a => a.ShelterId)
                .IsRequired(false);
        }
        #endregion
    }
}
