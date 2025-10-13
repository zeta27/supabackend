using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAEstudiosViewModel
    {
        public int? IdEstudios { get; set; } // Null para INSERT, con valor para UPDATE

        [StringLength(15)]
        public string? Abreviatura { get; set; }

        [StringLength(255)]
        public string? EstudiosEn { get; set; }

        public DateTime? Inicio { get; set; }

        public DateTime? Termino { get; set; }

        public DateTime? FechaObtencion { get; set; }

        [StringLength(255)]
        public string? Titulo { get; set; }

        [StringLength(255)]
        public string? Cedula { get; set; }

        [Required]
        public int IdSUPA { get; set; }

        public bool UltimoGrado { get; set; } = false;

        [Required]
        public int IdCatNivelEstudios { get; set; }
    }
}
