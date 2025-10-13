using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAEntidades", Schema = "dbo")]
public partial class SUPAEntidades
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdEntidades { get; set; }

    [Required]
    public int IdCatEntidades { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FechaRegistro { get; set; } = DateTime.Today;

    [Required]
    public bool UltimaEntidad { get; set; } = false;

    [ForeignKey("IdCatEntidades")]
    [InverseProperty("SUPAEntidades")]
    public virtual SUPACatEntidades IdCatEntidadesNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPAEntidades")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
