using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAApoyosEcoCAViewModel
    {
        public int? IdApoyosEcoCA { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdCA { get; set; }

        [Required]
        public DateTime InicioApoyo { get; set; }

        [Required]
        public DateTime FinApoyo { get; set; }

        [Required]
        public int IdCatEstadoApoyo { get; set; }

        [Required]
        public int IdCatTipoApoyo { get; set; }

        [Required]
        public double MontoApoyo { get; set; }

        [StringLength(255)]
        public string? ObservacionesApoyo { get; set; }

        public double? MontoEjercido { get; set; }

        public double? MontoComprobado { get; set; }

        public double? MontoDevuelto { get; set; }

        [StringLength(255)]
        public string? OficioConcFin { get; set; }

        [StringLength(255)]
        public string? OficioConcAcad { get; set; }
    }
}
