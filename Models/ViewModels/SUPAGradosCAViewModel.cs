using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAGradosCAViewModel
    {
        public int? IdGradosCA { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdCA { get; set; }

        public DateTime? FechaObtencion { get; set; } // Si es null, usa GETDATE() en el SP

        public bool UltimoGradoCA { get; set; } = false;

        [Required]
        public int IdCatGradoCA { get; set; }
    }
}