using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAContratacionesViewModel
    {
        public int? IdContrataciones { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdCatTipoContratacion { get; set; }

        [Required]
        public int IdSUPA { get; set; }

        [Required]
        public int IdCatTempContratacion { get; set; }

        [Required]
        public DateTime InicioContratacion { get; set; }

        public DateTime? TerminoContratacion { get; set; }

        [StringLength(255)]
        public string? DocSoporte { get; set; }
    }
}