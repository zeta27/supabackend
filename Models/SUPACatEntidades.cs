using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatEntidades", Schema = "dbo")]
public partial class SUPACatEntidades
{
    [Required]
    [StringLength(50)]
    public string Dentidad { get; set; } = null!;

    [Required]
    public int IdCatAreas { get; set; }

    [Required]
    public int IdCatRegion { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatEntidades { get; set; }

    [Required]
    [StringLength(100)]
    public string IdentidadUV { get; set; } = null!;

    [ForeignKey("IdCatAreas")]
    [InverseProperty("SUPACatEntidades")]
    public virtual SUPACatAreas IdCatAreasNavigation { get; set; } = null!;

    [ForeignKey("IdCatRegion")]
    [InverseProperty("SUPACatEntidades")]
    public virtual SUPACatRegion IdCatRegionNavigation { get; set; } = null!;

    [InverseProperty("IdCatEntidadesNavigation")]
    public virtual ICollection<SUPAEntidades> SUPAEntidades { get; set; } = new List<SUPAEntidades>();
}

