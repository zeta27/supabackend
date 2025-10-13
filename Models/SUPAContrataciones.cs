using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAContrataciones", Schema = "dbo")]
public partial class SUPAContrataciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdContrataciones { get; set; }

    [Required]
    public int IdCatTipoContratacion { get; set; }

    [Required]
    public int IdSUPA { get; set; }

    [Required]
    public int IdCatTempContratacion { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime InicioContratacion { get; set; }

    [Column(TypeName = "date")]
    public DateTime? TerminoContratacion { get; set; }

    [StringLength(255)]
    public string? DocSoporte { get; set; }

    [ForeignKey("IdCatTempContratacion")]
    [InverseProperty("SUPAContrataciones")]
    public virtual SUPACatTempContrataciones IdCatTempContratacionNavigation { get; set; } = null!;

    [ForeignKey("IdCatTipoContratacion")]
    [InverseProperty("SUPAContrataciones")]
    public virtual SUPACatTipoContrataciones IdCatTipoContratacionNavigation { get; set; } = null!;

    [ForeignKey("IdSUPA")]
    [InverseProperty("SUPAContrataciones")]
    public virtual SUPAAcademicos IdSUPANavigation { get; set; } = null!;
}
