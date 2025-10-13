using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACitas", Schema = "dbo")]
public partial class SUPACitas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCita { get; set; }

    public int? IdSUPA { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime FechaCita { get; set; }

    [Required]
    [Precision(0)]
    public TimeSpan HoraInicio { get; set; }

    [Required]
    [Precision(0)]
    public TimeSpan HoraTermino { get; set; }

    [Required]
    [StringLength(100)]
    public string Estado { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Sala { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Lugar { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPACitas")]
    public virtual SUPAAcademicos? IdSUPANavigation { get; set; }
}
