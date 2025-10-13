using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatEstadoApoyo", Schema = "dbo")]
public partial class SUPACatEstadoApoyo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatEstadoApoyo { get; set; }

    [Required]
    [StringLength(100)]
    public string DEstadoApoyo { get; set; } = null!;

    [InverseProperty("IdCatEstadoApoyoNavigation")]
    public virtual ICollection<SUPAApoyosEco> SUPAApoyosEco { get; set; } = new List<SUPAApoyosEco>();

    [InverseProperty("IdCatEstadoApoyoNavigation")]
    public virtual ICollection<SUPAApoyosEcoCA> SUPAApoyosEcoCA { get; set; } = new List<SUPAApoyosEcoCA>();
}
