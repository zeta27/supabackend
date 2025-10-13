using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
    public class SUPAAcademicosViewModel
    {
        public int? IdSUPA { get; set; } // Null para INSERT, con valor para UPDATE

        [Required]
        [StringLength(18)]
        public string CURP { get; set; } = null!;

        [Required]
        public int NP { get; set; }

        [StringLength(150)]
        public string? Paterno { get; set; }

        [StringLength(150)]
        public string? Materno { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = null!;

        [Required]
        public int IdCatGeneros { get; set; }

        [Required]
        public int IdCatNacionalidad { get; set; }

        [StringLength(4)]
        public string Institucion { get; set; } = "Universidad Veracruzana";

        [Required]
        public int IdPRODEP { get; set; }

        [StringLength(100)]
        public string? CuentaUV { get; set; }

        public bool Baja { get; set; } = false;

        public DateTime? FechaBaja { get; set; }

        [StringLength(250)]
        public string? Observaciones { get; set; }

        public int IdCatMotivos { get; set; } = 1;
    }
}
