using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatTipoContrataciones", Schema = "dbo")]
public partial class SUPACatTipoContrataciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatTipoContratacion { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string DTipoContratacion { get; set; } = null!;

    [InverseProperty("IdCatTipoContratacionNavigation")]
    public virtual ICollection<SUPAContrataciones> SUPAContrataciones { get; set; } = new List<SUPAContrataciones>();
}
