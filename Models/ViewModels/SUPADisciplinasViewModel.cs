using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPADisciplinasViewModel
    {
        public int? IdDisciplinas { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdSUPA { get; set; }

        [Required]
        public int IdCatDisciplinas { get; set; }

        public DateTime? FechaRegistro { get; set; } // Si es null, usa GETDATE() en el SP

        public bool UltimaDisciplina { get; set; } = false;
    }
}