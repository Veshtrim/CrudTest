using System.ComponentModel.DataAnnotations;

namespace Crud.Models
{
    public class Puntori
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string? Description { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Departament { get; set;}
    }
}
