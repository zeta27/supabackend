using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAApoyosEcoCA", Schema = "dbo")]
public partial class SUPAApoyosEcoCA
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdApoyosEcoCA { get; set; }

    [Required]
    public int IdCA { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime InicioApoyo { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FinApoyo { get; set; }

    [Required]
    public int IdCatEstadoApoyo { get; set; }

    [Required]
    public int IdCatTipoApoyo { get; set; }

    [Required]
    public double MontoApoyo { get; set; }

    [StringLength(255)]
    public string? ObservacionesApoyo { get; set; }

    public double? MontoEjercido { get; set; }

    public double? MontoComprobado { get; set; }

    public double? MontoDevuelto { get; set; }

    [StringLength(255)]
    public string? OficioConcFin { get; set; }

    [StringLength(255)]
    public string? OficioConcAcad { get; set; }

    [ForeignKey("IdCA")]
    [InverseProperty("SUPAApoyosEcoCA")]
    public virtual SUPACuerpoAcademicos IdCANavigation { get; set; } = null!;

    [ForeignKey("IdCatEstadoApoyo")]
    [InverseProperty("SUPAApoyosEcoCA")]
    public virtual SUPACatEstadoApoyo IdCatEstadoApoyoNavigation { get; set; } = null!;

    [ForeignKey("IdCatTipoApoyo")]
    [InverseProperty("SUPAApoyosEcoCA")]
    public virtual SUPACatTipoApoyo IdCatTipoApoyoNavigation { get; set; } = null!;
}
