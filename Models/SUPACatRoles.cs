using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatRoles", Schema = "dbo")]
public partial class SUPACatRoles
{
    [Required]
    [StringLength(15)]
    public string DRol { get; set; } = null!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatRol { get; set; }

    [InverseProperty("IdCatRolNavigation")]
    public virtual ICollection<SUPARolesMiembros> SUPARolesMiembros { get; set; } = new List<SUPARolesMiembros>();
}
