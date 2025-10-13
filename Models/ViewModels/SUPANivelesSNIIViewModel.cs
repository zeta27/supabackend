using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPANivelesSNIIViewModel
    {
        public int? IdNivelesSNII { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdSUPA { get; set; }

        public DateTime? FechaObtencion { get; set; }

        public bool UltimoNivel { get; set; } = false;

        [Required]
        public int IdCatNivelSNII { get; set; }
    }
}
