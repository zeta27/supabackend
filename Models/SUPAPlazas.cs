using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAPlazas", Schema = "dbo")]
public partial class SUPAPlazas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdPlaza { get; set; }

    [Required]
    [StringLength(100)]
    public string ClavePlaza { get; set; } = null!;

    [Required]
    [Column(TypeName = "date")]
    public DateTime InicioPlaza { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime TerminoPlaza { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    public bool UltimoOcupante { get; set; } = false;

    [Required]
    public bool BajaPlaza { get; set; } = false;

    [Column(TypeName = "date")]
    public DateTime? FechaBajaPlaza { get; set; }

    [Required]
    public int IdCatMotivos { get; set; } = 1;

    [Required]
    public int IdAreaPlaza { get; set; }

    [Required]
    public int IdRegionPlaza { get; set; }

    [ForeignKey("IdAreaPlaza")]
    [InverseProperty("SUPAPlazas")]
    public virtual SUPACatAreas IdAreaPlazaNavigation { get; set; } = null!;

    [ForeignKey("IdCatMotivos")]
    [InverseProperty("SUPAPlazas")]
    public virtual SUPACatMotivos IdCatMotivosNavigation { get; set; } = null!;

    [ForeignKey("IdRegionPlaza")]
    [InverseProperty("SUPAPlazas")]
    public virtual SUPACatRegion IdRegionPlazaNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPAPlazas")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
