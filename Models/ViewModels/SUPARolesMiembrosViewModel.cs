using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPARolesMiembrosViewModel
    {
        public int? IdRolesMiembros { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public DateTime FechaAsignacion { get; set; }

        public bool UltimoRol { get; set; } = false;

        [Required]
        public int IdCatRol { get; set; }

        [Required]
        public int IdMiembrosCA { get; set; }
    }
}