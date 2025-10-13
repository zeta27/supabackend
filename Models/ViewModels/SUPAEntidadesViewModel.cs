using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAEntidadesViewModel
    {
        public int? IdEntidades { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdCatEntidades { get; set; }

        [Required]
        public int IdSUPA { get; set; }

        public DateTime? FechaRegistro { get; set; } // Si es null, usa GETDATE() en el SP

        public bool UltimaEntidad { get; set; } = false;
    }
}