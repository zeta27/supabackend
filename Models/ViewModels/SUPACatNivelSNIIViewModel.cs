using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatNivelSNIIViewModel
    {
        public int? IdCatNivelSNII { get; set; } // Null para INSERT, con valor para UPDATE

        [StringLength(10, ErrorMessage = "El nivel SNII no puede exceder 10 caracteres")]
        public string? DNivelSNII { get; set; } // Campo opcional (nullable)
    }
}