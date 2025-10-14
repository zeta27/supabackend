using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatNivelEstudiosViewModel
    {
        public int? IdCatNivelEstudios { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del nivel de estudios es requerida")]
        [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres")]
        public string DescripcionNivelEstudios { get; set; } = null!;
    }
}