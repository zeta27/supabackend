using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatTipoContratacionesViewModel
    {
        public int? IdCatTipoContratacion { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del tipo de contratación es requerida")]
        [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres")]
        public string DTipoContratacion { get; set; } = null!;
    }
}