using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAGradosCA", Schema = "dbo")]
public partial class SUPAGradosCA
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdGradosCA { get; set; }

    [Required]
    public int IdCA { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FechaObtencion { get; set; } = DateTime.Today;

    [Required]
    public bool UltimoGradoCA { get; set; } = false;

    [Required]
    public int IdCatGradoCA { get; set; }

    [ForeignKey("IdCA")]
    [InverseProperty("SUPAGradosCA")]
    public virtual SUPACuerpoAcademicos IdCANavigation { get; set; } = null!;

    [ForeignKey("IdCatGradoCA")]
    [InverseProperty("SUPAGradosCA")]
    public virtual SUPACatGradoCA IdCatGradoCANavigation { get; set; } = null!;
}
