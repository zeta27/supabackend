using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAPlazasViewModel
    {
        public int? IdPlaza { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        [StringLength(100)]
        public string ClavePlaza { get; set; } = null!;

        [Required]
        public DateTime InicioPlaza { get; set; }

        [Required]
        public DateTime TerminoPlaza { get; set; }

        [Required]
        public int IdSUPA { get; set; }

        public bool UltimoOcupante { get; set; } = false;

        public bool BajaPlaza { get; set; } = false;

        public DateTime? FechaBajaPlaza { get; set; }

        public int IdCatMotivos { get; set; } = 1;

        [Required]
        public int IdAreaPlaza { get; set; }

        [Required]
        public int IdRegionPlaza { get; set; }
    }
}
