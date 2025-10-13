using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAEstudios", Schema = "dbo")]
public partial class SUPAEstudios
{
    [StringLength(15)]
    public string? Abreviatura { get; set; }

    [StringLength(255)]
    public string? EstudiosEn { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Inicio { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Termino { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaObtencion { get; set; }

    [StringLength(255)]
    public string? Titulo { get; set; }

    [StringLength(255)]
    public string? Cedula { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdEstudios { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    public bool UltimoGrado { get; set; } = false;

    [Required]
    public int IdCatNivelEstudios { get; set; }

    [ForeignKey("IdCatNivelEstudios")]
    [InverseProperty("SUPAEstudios")]
    public virtual SUPACatNivelEstudios IdCatNivelEstudiosNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPAEstudios")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
