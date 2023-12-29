using Microsoft.AspNetCore.Mvc.Rendering;

namespace Crud.Models
{
    public class PuntoriDepartamentiViewModel
    {
        public List<Puntori> Puntoret { get; set; }
        public SelectList? Departamenti { get; set; }
        public string? DepartamentiFilter { get; set; }
        public string? SearchString { get; set; }
    }
}
