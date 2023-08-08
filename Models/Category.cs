using System.ComponentModel.DataAnnotations;

namespace ProgramlamaYazProje.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Çeşit ismi zorunludur")]
        public string Name { get; set; }
        public List<Animal>? Animals { get; set; }
    }
}