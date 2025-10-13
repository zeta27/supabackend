using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPADescargasAViewModel
    {
        public int? IdDescargaA { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdSUPA { get; set; }

        [Required]
        public int IdCatPeriodos { get; set; }

        [Required]
        public int IdAreaDescarga { get; set; }

        [Required]
        public int IdRegionDescarga { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreEstudios { get; set; } = null!;

        [Required]
        public DateTime InicioEstudios { get; set; }

        [Required]
        public DateTime FinEstudios { get; set; }

        public bool Entrego { get; set; } = false;

        [Required]
        [StringLength(100)]
        public string InstitucionEstudios { get; set; } = null!;
    }
}
