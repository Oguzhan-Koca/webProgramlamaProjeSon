using System.ComponentModel.DataAnnotations;

namespace ProgramlamaYazProje.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Hayvan ismi zorunludur")]
        public string Name { get; set; }
        public int Age { get; set; }
        public string? PhotoURL { get; set; }
        public string Status { get; set; }
        public string? UserGuid { get; set; } 
        public string? UserName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? ShelterId { get; set; }
        public Shelter? Shelter { get; set; }
    }
}
