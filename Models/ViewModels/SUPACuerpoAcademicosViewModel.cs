using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACuerpoAcademicosViewModel
    {
        public int? IdCA { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        [StringLength(10)]
        public string Clave { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string NombreCuerpoAcademico { get; set; } = null!;

        public DateTime? FechaRegistro { get; set; } // Si es null, usa GETDATE() en el SP

        public bool UltimoRegistro { get; set; } = false;

        public bool Baja { get; set; } = false;

        public DateTime? FechaBaja { get; set; }

        public int IdCatMotivos { get; set; } = 1;

        [StringLength(255)]
        public string? ObservacionesBaja { get; set; }
    }
}