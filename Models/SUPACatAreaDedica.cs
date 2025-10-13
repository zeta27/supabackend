using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatAreaDedica", Schema = "dbo")]
public partial class SUPACatAreaDedica
{
    [Required]
    [StringLength(50)]
    public string DAreaDedica { get; set; } = null!;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatAreaDedica { get; set; }

    [InverseProperty("IdCatAreaDedicaNavigation")]
    public virtual ICollection<SUPAAreaDedica> SUPAAreaDedica { get; set; } = new List<SUPAAreaDedica>();
}
