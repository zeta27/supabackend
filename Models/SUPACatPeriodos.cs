using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatPeriodos", Schema = "dbo")]
public partial class SUPACatPeriodos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatPeriodos { get; set; }

    [StringLength(100)]
    public string? DescripcionPeriodo { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaInicio { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaTermino { get; set; }

    [InverseProperty("IdCatPeriodosNavigation")]
    public virtual ICollection<SUPADescargasA> SUPADescargasA { get; set; } = new List<SUPADescargasA>();
}
