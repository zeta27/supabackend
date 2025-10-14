using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatAreaDedicaViewModel
    {
        public int? IdCatAreaDedica { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del área dedicada es requerida")]
        [StringLength(50, ErrorMessage = "La descripción no puede exceder 50 caracteres")]
        public string DAreaDedica { get; set; } = null!;
    }
}