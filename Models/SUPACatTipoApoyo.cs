using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatTipoApoyo", Schema = "dbo")]
public partial class SUPACatTipoApoyo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatTipoApoyo { get; set; }

    [Required]
    [StringLength(255)]
    public string DTipoApoyo { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string FedInter { get; set; } = null!;

    [InverseProperty("IdCatTipoApoyoNavigation")]
    public virtual ICollection<SUPAApoyosEco> SUPAApoyosEco { get; set; } = new List<SUPAApoyosEco>();

    [InverseProperty("IdCatTipoApoyoNavigation")]
    public virtual ICollection<SUPAApoyosEcoCA> SUPAApoyosEcoCA { get; set; } = new List<SUPAApoyosEcoCA>();
}