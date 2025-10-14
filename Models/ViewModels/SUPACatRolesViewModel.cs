using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatRolesViewModel
    {
        public int? IdCatRol { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del rol es requerida")]
        [StringLength(15, ErrorMessage = "La descripción no puede exceder 15 caracteres")]
        public string DRol { get; set; } = null!;
    }
}