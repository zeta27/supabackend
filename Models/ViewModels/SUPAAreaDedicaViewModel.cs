using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAAreaDedicaViewModel
    {
        public int? IdAreaDedica { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdSUPA { get; set; }

        [Required]
        public int IdCatAreaDedica { get; set; }

        public DateTime? FechaRegistro { get; set; } // Si es null, usa GETDATE() en el SP

        public bool UltimaAreaDedica { get; set; } = false;
    }
}