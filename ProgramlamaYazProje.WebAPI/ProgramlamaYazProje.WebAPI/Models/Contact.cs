using System.ComponentModel.DataAnnotations;

namespace ProgramlamaYazProje.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Subject { get; set; }
        [MaxLength(500)]
        public string? Message { get; set; }
    }
}
