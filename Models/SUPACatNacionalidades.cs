using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatNacionalidades", Schema = "dbo")]
public partial class SUPACatNacionalidades
{
    [Key]
    public int IdCatNacionalidad { get; set; }

    [StringLength(20)]
    public string? DNacionalidad { get; set; }

    [InverseProperty("IdCatNacionalidadNavigation")]
    public virtual ICollection<SUPAAcademicos> SUPAAcademicos { get; set; } = new List<SUPAAcademicos>();
}
