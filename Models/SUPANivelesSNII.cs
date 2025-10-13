using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPANivelesSNII", Schema = "dbo")]
public partial class SUPANivelesSNII
{
    [Required]
    public int IdSUPA { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdNivelesSNII { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaObtencion { get; set; }

    [Required]
    public bool UltimoNivel { get; set; } = false;

    [Required]
    public int IdCatNivelSNII { get; set; }

    [ForeignKey("IdCatNivelSNII")]
    [InverseProperty("SUPANivelesSNII")]
    public virtual SUPACatNivelSNII IdCatNivelSNIINavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPANivelesSNII")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
