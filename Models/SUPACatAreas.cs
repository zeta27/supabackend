using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace supa.Models
{
    [Table("SUPACatAreas", Schema = "dbo")]
    public class SUPACatAreas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCatAreas { get; set; }

        [Required(ErrorMessage = "El nombre del área es requerido")]
        [StringLength(50, ErrorMessage = "El nombre del área no puede exceder 50 caracteres")]
        [Display(Name = "Nombre del Área")]
        public string Darea { get; set; } = null!;

       
         public virtual ICollection<SUPACatEntidades> SUPACatEntidades { get; set; } = new List<SUPACatEntidades>();
        public virtual ICollection<SUPADescargasA> SUPADescargasA { get; set; } = new List<SUPADescargasA>();
         public virtual ICollection<SUPAPlazas> SUPAPlazas { get; set; } = new List<SUPAPlazas>();
    }
}