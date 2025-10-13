using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPACatEntidadesViewModel
    {
        public int? IdCatEntidades { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        [StringLength(50)]
        public string Dentidad { get; set; } = null!;

        [Required]
        public int IdCatAreas { get; set; }

        [Required]
        public int IdCatRegion { get; set; }

        [Required]
        [StringLength(100)]
        public string IdentidadUV { get; set; } = null!;
    }
}
