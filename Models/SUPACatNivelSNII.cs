using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatNivelSNII", Schema = "dbo")]
public partial class SUPACatNivelSNII
{
    [Key]
    public int IdCatNivelSNII { get; set; }

    [StringLength(10)]
    public string? DNivelSNII { get; set; }

    [InverseProperty("IdCatNivelSNIINavigation")]
    public virtual ICollection<SUPANivelesSNII> SUPANivelesSNII { get; set; } = new List<SUPANivelesSNII>();
}
