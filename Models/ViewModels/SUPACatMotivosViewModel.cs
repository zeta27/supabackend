using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatMotivosViewModel
    {
        public int? IdCatMotivos { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del motivo es requerida")]
        [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres")]
        public string DMotivos { get; set; } = null!;
    }
}