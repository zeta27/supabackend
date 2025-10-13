using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAVigenciaCuerpo", Schema = "dbo")]
public partial class SUPAVigenciaCuerpo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdVigenciaCuerpo { get; set; }

    [Required]
    public int IdCA { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime Inicio { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime Termino { get; set; }

    public bool? UltimaVigencia { get; set; } = false;

    [ForeignKey("IdCA")]
    [InverseProperty("SUPAVigenciaCuerpo")]
    public virtual SUPACuerpoAcademicos IdCANavigation { get; set; } = null!;
}
