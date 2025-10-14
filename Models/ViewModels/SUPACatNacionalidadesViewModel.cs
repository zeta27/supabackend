using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatNacionalidadesViewModel
    {
        public int? IdCatNacionalidad { get; set; } // Null para INSERT, con valor para UPDATE

        [StringLength(20, ErrorMessage = "La nacionalidad no puede exceder 20 caracteres")]
        public string? DNacionalidad { get; set; } // Campo opcional (nullable)
    }
}