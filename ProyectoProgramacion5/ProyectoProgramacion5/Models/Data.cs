using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoProgra5.Models
{
    public class Data
    {
        public List<Location>? LocationList { get; set; }
        public List<Rol>? RolList { get; set; }
        public List<Condo>? CondoList { get; set; }

    }
}
