using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAVigenciaPerfil", Schema = "dbo")]
public partial class SUPAVigenciaPerfil
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdVigenciaPerfil { get; set; }

    public int? IdSUPA { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime Inicio { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime Termino { get; set; }

    [Required]
    public bool UltimaVigencia { get; set; } = false;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPAVigenciaPerfiles")]
    public virtual SUPAAcademicos? IdSUPANavigation { get; set; }
}
