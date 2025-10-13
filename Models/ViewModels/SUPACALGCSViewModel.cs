using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACALGCSViewModel
    {
        public int? IdCALGCS { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdCA { get; set; }

        [Required]
        [StringLength(150)]
        public string LGC1 { get; set; } = null!;

        [StringLength(150)]
        public string? LGC2 { get; set; }

        [StringLength(150)]
        public string? LGC3 { get; set; }

        [StringLength(150)]
        public string? LGC4 { get; set; }

        [StringLength(150)]
        public string? LGC5 { get; set; }

        [StringLength(150)]
        public string? LGC6 { get; set; }

        public DateTime? FechaRegistro { get; set; } // Si es null, usa GETDATE() en el SP

        public bool UltimasLineas { get; set; } = false;
    }
}
