using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPARolesMiembros", Schema = "dbo")]
public partial class SUPARolesMiembros
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdRolesMiembros { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FechaAsignacion { get; set; }

    [Required]
    public bool UltimoRol { get; set; } = false;

    [Required]
    public int IdCatRol { get; set; }

    [Required]
    public int IdMiembrosCA { get; set; }

    [ForeignKey("IdCatRol")]
    [InverseProperty("SUPARolesMiembros")]
    public virtual SUPACatRoles IdCatRolNavigation { get; set; } = null!;

    [ForeignKey("IdMiembrosCA")]
    [InverseProperty("SUPARolesMiembros")]
    public virtual SUPAMiembrosCA IdMiembrosCANavigation { get; set; } = null!;
}

