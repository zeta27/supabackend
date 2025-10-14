using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatTipoApoyoViewModel
    {
        public int? IdCatTipoApoyo { get; set; } // Null para INSERT, con valor para UPDATE

        [Required(ErrorMessage = "La descripción del tipo de apoyo es requerida")]
        [StringLength(255, ErrorMessage = "La descripción no puede exceder 255 caracteres")]
        public string DTipoApoyo { get; set; } = null!;

        [Required(ErrorMessage = "El campo FedInter es requerido")]
        [StringLength(20, ErrorMessage = "El campo FedInter no puede exceder 20 caracteres")]
        public string FedInter { get; set; } = null!;
    }
}