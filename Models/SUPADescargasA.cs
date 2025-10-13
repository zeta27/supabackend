using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPADescargasA", Schema = "dbo")]
public partial class SUPADescargasA
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdDescargaA { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    public int IdCatPeriodos { get; set; }

    [Required]
    public int IdAreaDescarga { get; set; }

    [Required]
    public int IdRegionDescarga { get; set; }

    [Required]
    [StringLength(100)]
    public string NombreEstudios { get; set; } = null!;

    [Required]
    [Column(TypeName = "date")]
    public DateTime InicioEstudios { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FinEstudios { get; set; }

    [Required]
    public bool Entrego { get; set; } = false;

    [Required]
    [StringLength(100)]
    public string InstitucionEstudios { get; set; } = null!;

    [ForeignKey("IdAreaDescarga")]
    [InverseProperty("SUPADescargasA")]
    public virtual SUPACatAreas IdAreaDescargaNavigation { get; set; } = null!;

    [ForeignKey("IdCatPeriodos")]
    [InverseProperty("SUPADescargasA")]
    public virtual SUPACatPeriodos IdCatPeriodosNavigation { get; set; } = null!;

    [ForeignKey("IdRegionDescarga")]
    [InverseProperty("SUPADescargasA")]
    public virtual SUPACatRegion IdRegionDescargaNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPADescargasA")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
