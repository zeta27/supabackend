using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatRegion", Schema = "dbo")]
public partial class SUPACatRegion
{
    [Key]
    public int IdCatRegion { get; set; }

    [StringLength(50)]
    public string? Dregion { get; set; }

    [InverseProperty("IdCatRegionNavigation")]
    public virtual ICollection<SUPACatEntidades> SUPACatEntidades { get; set; } = new List<SUPACatEntidades>();

    [InverseProperty("IdRegionDescargaNavigation")]
    public virtual ICollection<SUPADescargasA> SUPADescargasA { get; set; } = new List<SUPADescargasA>();

    [InverseProperty("IdRegionPlazaNavigation")]
    public virtual ICollection<SUPAPlazas> SUPAPlazas { get; set; } = new List<SUPAPlazas>();
}
