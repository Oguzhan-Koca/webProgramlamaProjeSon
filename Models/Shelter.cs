using System.ComponentModel.DataAnnotations;

namespace ProgramlamaYazProje.Models
{
    public class Shelter
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Barınak ismi zorunludur")]
        public string Name { get; set; }
        public List<Animal>? Animals { get; set; }
    }
}
