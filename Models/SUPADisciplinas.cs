using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPADisciplinas", Schema = "dbo")]
public partial class SUPADisciplinas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdDisciplinas { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    public int IdCatDisciplinas { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FechaRegistro { get; set; } = DateTime.Today;

    [Required]
    public bool UltimaDisciplina { get; set; } = false;

    [ForeignKey("IdCatDisciplinas")]
    [InverseProperty("SUPADisciplinas")]
    public virtual SUPACatDisciplinas IdCatDisciplinasNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPADisciplinas")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
