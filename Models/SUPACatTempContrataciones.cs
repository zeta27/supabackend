using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatTempContrataciones", Schema = "dbo")]
public partial class SUPACatTempContrataciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatTempContratacion { get; set; }

    [Required]
    [StringLength(100)]
    public string DTempContratacion { get; set; } = null!;

    [InverseProperty("IdCatTempContratacionNavigation")]
    public virtual ICollection<SUPAContrataciones> SUPAContrataciones { get; set; } = new List<SUPAContrataciones>();
}
