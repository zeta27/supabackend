using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACitasViewModel
    {
        public int? IdCita { get; set; } // Null para INSERT, con valor para UPDATE

        public int? IdSUPA { get; set; }

        [Required]
        public DateTime FechaCita { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraTermino { get; set; }

        [Required]
        [StringLength(100)]
        public string Estado { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Sala { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Lugar { get; set; } = null!;
    }
}
