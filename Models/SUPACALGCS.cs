using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACALGCS", Schema = "dbo")]
public partial class SUPACALGCS
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCALGCS { get; set; }

    [Required]
    public int IdCA { get; set; }

    [Required]
    [StringLength(150)]
    public string LGC1 { get; set; } = null!;

    [StringLength(150)]
    public string? LGC2 { get; set; }

    [StringLength(150)]
    public string? LGC3 { get; set; }

    [StringLength(150)]
    public string? LGC4 { get; set; }

    [StringLength(150)]
    public string? LGC5 { get; set; }

    [StringLength(150)]
    public string? LGC6 { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FechaRegistro { get; set; } = DateTime.Today;

    [Required]
    public bool UltimasLineas { get; set; } = false;

    [ForeignKey("IdCA")]
    [InverseProperty("SUPACALGCS")]
    public virtual SUPACuerpoAcademicos IdCANavigation { get; set; } = null!;
}
