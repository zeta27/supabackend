using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAApoyosEcoViewModel
    {
        public int? IdApoyosEco { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdSUPA { get; set; }

        [Required]
        public DateTime InicioApoyo { get; set; }

        [Required]
        public DateTime FinApoyo { get; set; }

        [Required]
        public int IdCatTipoApoyo { get; set; }

        [Required]
        public double MontoApoyo { get; set; }

        [StringLength(255)]
        public string? ObservacionesApoyo { get; set; }

        [Required]
        public int IdCatEstadoApoyo { get; set; }

        public double? MontoEjercido { get; set; }

        public double? MontoComprobado { get; set; }

        public double? MontoDevuelto { get; set; }

        [StringLength(255)]
        public string? OficioConcFIn { get; set; }

        [StringLength(255)]
        public string? OficioFinAcad { get; set; }
    }
}
