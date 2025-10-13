using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACuerpoAcademicos", Schema = "dbo")]
public partial class SUPACuerpoAcademicos
{
    [Required]
    [StringLength(10)]
    public string Clave { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string NombreCuerpoAcademico { get; set; } = null!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCA { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FechaRegistro { get; set; } = DateTime.Today;

    [Required]
    public bool UltimoRegistro { get; set; } = false;

    [Required]
    public bool Baja { get; set; } = false;

    [Column(TypeName = "date")]
    public DateTime? FechaBaja { get; set; }

    [Required]
    public int IdCatMotivos { get; set; } = 1;

    [StringLength(255)]
    public string? ObservacionesBaja { get; set; }

    [ForeignKey("IdCatMotivos")]
    [InverseProperty("SUPACuerpoAcademicos")]
    public virtual SUPACatMotivos IdCatMotivosNavigation { get; set; } = null!;

    [InverseProperty("IdCANavigation")]
    public virtual ICollection<SUPAApoyosEcoCA> SUPAApoyosEcoCA { get; set; } = new List<SUPAApoyosEcoCA>();

    [InverseProperty("IdCANavigation")]
    public virtual ICollection<SUPACALGCS> SUPACALGCS { get; set; } = new List<SUPACALGCS>();

    [InverseProperty("IdCANavigation")]
    public virtual ICollection<SUPAGradosCA> SUPAGradosCA { get; set; } = new List<SUPAGradosCA>();

    [InverseProperty("IdCANavigation")]
    public virtual ICollection<SUPAMiembrosCA> SUPAMiembrosCA { get; set; } = new List<SUPAMiembrosCA>();

    [InverseProperty("IdCANavigation")]
    public virtual ICollection<SUPAVigenciaCuerpo> SUPAVigenciaCuerpo { get; set; } = new List<SUPAVigenciaCuerpo>();
}
