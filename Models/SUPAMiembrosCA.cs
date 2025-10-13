using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAMiembrosCA", Schema = "dbo")]
public partial class SUPAMiembrosCA
{
    [Key]
    public int IdMiembrosCA { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    public int IdCA { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaAlta { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaBaja { get; set; }

    [Required]
    public bool UltimoRegistro { get; set; } = false;

    [Required]
    public bool Baja { get; set; } = false;

    [Required]
    public int IdCatMotivos { get; set; } = 1;

    [StringLength(255)]
    public string? ObservacionesBaja { get; set; }

    [ForeignKey("IdCA")]
    [InverseProperty("SUPAMiembrosCA")]
    public virtual SUPACuerpoAcademicos IdCANavigation { get; set; } = null!;

    [ForeignKey("IdCatMotivos")]
    [InverseProperty("SUPAMiembrosCA")]
    public virtual SUPACatMotivos IdCatMotivosNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPAMiembrosCA")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;

    [InverseProperty("IdMiembrosCANavigation")]
    public virtual ICollection<SUPARolesMiembros> SUPARolesMiembros { get; set; } = new List<SUPARolesMiembros>();
}
