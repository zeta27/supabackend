using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatPeriodosViewModel
    {
        public int? IdCatPeriodos { get; set; } // Null para INSERT, con valor para UPDATE

        [StringLength(100, ErrorMessage = "La descripción no puede exceder 100 caracteres")]
        public string? DescripcionPeriodo { get; set; } // Nullable

        [DataType(DataType.Date)]
        public DateTime? FechaInicio { get; set; } // Nullable

        [DataType(DataType.Date)]
        public DateTime? FechaTermino { get; set; } // Nullable
    }
}