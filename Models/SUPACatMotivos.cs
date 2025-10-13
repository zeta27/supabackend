using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatMotivos", Schema = "dbo")]
public partial class SUPACatMotivos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatMotivos { get; set; }

    [Required]
    [StringLength(100)]
    public string DMotivos { get; set; } = null!;

    [InverseProperty("IdCatMotivosNavigation")]
    public virtual ICollection<SUPAAcademicos> SUPAAcademicos { get; set; } = new List<SUPAAcademicos>();

    [InverseProperty("IdCatMotivosNavigation")]
    public virtual ICollection<SUPACuerpoAcademicos> SUPACuerpoAcademicos { get; set; } = new List<SUPACuerpoAcademicos>();

    [InverseProperty("IdCatMotivosNavigation")]
    public virtual ICollection<SUPAMiembrosCA> SUPAMiembrosCA { get; set; } = new List<SUPAMiembrosCA>();

    [InverseProperty("IdCatMotivosNavigation")]
    public virtual ICollection<SUPAPlazas> SUPAPlazas { get; set; } = new List<SUPAPlazas>();
}

