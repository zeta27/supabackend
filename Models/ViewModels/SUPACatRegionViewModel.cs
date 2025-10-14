using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatRegionViewModel
    {
        public int? IdCatRegion { get; set; } // Null para INSERT, con valor para UPDATE

        [StringLength(50, ErrorMessage = "La región no puede exceder 50 caracteres")]
        public string? Dregion { get; set; } // Campo opcional (nullable)
    }
}