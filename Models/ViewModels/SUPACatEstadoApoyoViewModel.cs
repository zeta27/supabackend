using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatEstadoApoyoViewModel
    {
        public int? IdCatEstadoApoyo { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del estado de apoyo es requerida")]
        [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres")]
        public string DEstadoApoyo { get; set; } = null!;
    }
}