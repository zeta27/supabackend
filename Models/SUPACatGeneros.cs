using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatGeneros", Schema = "dbo")]
public partial class SUPACatGeneros
{
    [Required]
    [StringLength(10)]
    public string DGenero { get; set; } = null!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatGeneros { get; set; }

    [InverseProperty("IdCatGenerosNavigation")]
    public virtual ICollection<SUPAAcademicos> SUPAAcademicos { get; set; } = new List<SUPAAcademicos>();
}
