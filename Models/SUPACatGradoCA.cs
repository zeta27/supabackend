using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatGradoCA", Schema = "dbo")]
public partial class SUPACatGradoCA
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatGradoCA { get; set; }

    [Required]
    [StringLength(255)]
    public string DescripcionGrado { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Abreviatura { get; set; } = null!;

    [InverseProperty("IdCatGradoCANavigation")]
    public virtual ICollection<SUPAGradosCA> SUPAGradosCA { get; set; } = new List<SUPAGradosCA>();
}
