using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatDisciplinasViewModel
    {
        public int? IdCatDisciplinas { get; set; } // Null para INSERT, con valor para UPDATE

        [StringLength(50, ErrorMessage = "La descripción no puede exceder 50 caracteres")]
        public string? Ddisciplina { get; set; } // Nullable como en la BD
    }
}