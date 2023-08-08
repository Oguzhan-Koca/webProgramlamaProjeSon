using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ProgramlamaYazProje.Models
{
    public class AnimalViewModel
    {
        public Animal Animal { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }

    }
}