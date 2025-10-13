using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatAreas", Schema = "dbo")]
public partial class SUPACatAreas
{
    [Required]
    [StringLength(50)]
    public string Darea { get; set; } = null!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatAreas { get; set; }

    [InverseProperty("IdCatAreasNavigation")]
    public virtual ICollection<SUPACatEntidades> SUPACatEntidades { get; set; } = new List<SUPACatEntidades>();

    [InverseProperty("IdAreaDescargaNavigation")]
    public virtual ICollection<SUPADescargasA> SUPADescargasA { get; set; } = new List<SUPADescargasA>();

    [InverseProperty("IdAreaPlazaNavigation")]
    public virtual ICollection<SUPAPlazas> SUPAPlazas { get; set; } = new List<SUPAPlazas>();
}
