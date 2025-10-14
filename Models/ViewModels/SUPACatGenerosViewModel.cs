using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatGenerosViewModel
    {
        public int? IdCatGeneros { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del género es requerida")]
        [StringLength(10, ErrorMessage = "La descripción no puede exceder 10 caracteres")]
        [Display(Name = "Género")]
        public string DGenero { get; set; } = null!;
    }
}