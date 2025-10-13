using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAAreaDedica", Schema = "dbo")]
public partial class SUPAAreaDedica
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdAreaDedica { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    public int IdCatAreaDedica { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FechaRegistro { get; set; } = DateTime.Today;

    [Required]
    public bool UltimaAreaDedica { get; set; } = false;

    [ForeignKey("IdCatAreaDedica")]
    [InverseProperty("SUPAAreaDedica")]
    public virtual SUPACatAreaDedica IdCatAreaDedicaNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPAAreaDedica")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
