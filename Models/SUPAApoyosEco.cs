using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAApoyosEco", Schema = "dbo")]
public partial class SUPAApoyosEco
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdApoyosEco { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime InicioApoyo { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FinApoyo { get; set; }

    [Required]
    public int IdCatTipoApoyo { get; set; }

    [Required]
    public double MontoApoyo { get; set; }

    [StringLength(255)]
    public string? ObservacionesApoyo { get; set; }

    [Required]
    public int IdCatEstadoApoyo { get; set; }

    public double? MontoEjercido { get; set; }

    public double? MontoComprobado { get; set; }

    public double? MontoDevuelto { get; set; }

    [StringLength(255)]
    public string? OficioConcFIn { get; set; }

    [StringLength(255)]
    public string? OficioFinAcad { get; set; }

    [ForeignKey("IdCatEstadoApoyo")]
    [InverseProperty("SUPAApoyosEco")]
    public virtual SUPACatEstadoApoyo IdCatEstadoApoyoNavigation { get; set; } = null!;

    [ForeignKey("IdCatTipoApoyo")]
    [InverseProperty("SUPAApoyosEco")]
    public virtual SUPACatTipoApoyo IdCatTipoApoyoNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPAApoyosEco")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
