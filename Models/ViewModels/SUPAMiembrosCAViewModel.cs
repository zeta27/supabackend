using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAMiembrosCAViewModel
    {
        [Required]
        public int IdMiembrosCA { get; set; } // En esta tabla el ID no es IDENTITY

        [Required]
        public int IdSUPA { get; set; }

        [Required]
        public int IdCA { get; set; }

        public DateTime? FechaAlta { get; set; }

        public DateTime? FechaBaja { get; set; }

        public bool UltimoRegistro { get; set; } = false;

        public bool Baja { get; set; } = false;

        public int IdCatMotivos { get; set; } = 1;

        [StringLength(255)]
        public string? ObservacionesBaja { get; set; }
    }
}