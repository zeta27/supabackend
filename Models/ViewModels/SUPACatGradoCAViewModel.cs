using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatGradoCAViewModel
    {
        public int? IdCatGradoCA { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del grado es requerida")]
        [StringLength(255, ErrorMessage = "La descripción no puede exceder 255 caracteres")]
        public string DescripcionGrado { get; set; } = null!;

        [Required(ErrorMessage = "La abreviatura del grado es requerida")]
        [StringLength(100, ErrorMessage = "La abreviatura no puede exceder 100 caracteres")]
        public string Abreviatura { get; set; } = null!;
    }
}