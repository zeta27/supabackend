using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAVigenciaCuerpoViewModel
    {
        public int? IdVigenciaCuerpo { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        public int IdCA { get; set; }

        [Required]
        public DateTime Inicio { get; set; }

        [Required]
        public DateTime Termino { get; set; }

        public bool? UltimaVigencia { get; set; } = false;
    }
}