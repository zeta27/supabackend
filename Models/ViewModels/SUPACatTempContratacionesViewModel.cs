using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatTempContratacionesViewModel
    {
        public int? IdCatTempContratacion { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción de la temporalidad de contratación es requerida")]
        [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres")]
        public string DTempContratacion { get; set; } = null!;
    }
}