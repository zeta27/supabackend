using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAVigenciaPerfilViewModel
    {
        public int? IdVigenciaPerfil { get; set; } // Null para INSERT, con valor para UPDATE

        public int? IdSUPA { get; set; }

        [Required]
        public DateTime Inicio { get; set; }

        [Required]
        public DateTime Termino { get; set; }

        public bool UltimaVigencia { get; set; } = false;
    }
}