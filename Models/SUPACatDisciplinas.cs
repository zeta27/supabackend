using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatDisciplinas", Schema = "dbo")]
public partial class SUPACatDisciplinas
{
    [StringLength(50)]
    public string? Ddisciplina { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatDisciplinas { get; set; }

    [InverseProperty("IdCatDisciplinasNavigation")]
    public virtual ICollection<SUPADisciplinas> SUPADisciplinas { get; set; } = new List<SUPADisciplinas>();
}
