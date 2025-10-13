using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPAAcademicos", Schema = "dbo")]
public partial class SUPAAcademicos
{
    [Required]
    [StringLength(18)]
    public string CURP { get; set; } = null!;

    [Required]
    public int NP { get; set; }

    [StringLength(150)]
    public string? Paterno { get; set; }

    [StringLength(150)]
    public string? Materno { get; set; }

    [Required]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Required]
    public int IdCatGeneros { get; set; }

    [Required]
    public int IdCatNacionalidad { get; set; }

    [Required]
    [StringLength(4)]
    public string Institucion { get; set; } = "Universidad Veracruzana";

    [Required]
    public int IdPRODEP { get; set; }

    [StringLength(100)]
    public string? CuentaUV { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdSUPA { get; set; }

    [Required]
    public bool Baja { get; set; } = false;

    [Column(TypeName = "date")]
    public DateTime? FechaBaja { get; set; }

    [StringLength(250)]
    public string? Observaciones { get; set; }

    [Required]
    public int IdCatMotivos { get; set; } = 1;

    [ForeignKey("IdCatGeneros")]
    [InverseProperty("SUPAAcademicos")]
    public virtual SUPACatGeneros IdCatGenerosNavigation { get; set; } = null!;

    [ForeignKey("IdCatMotivos")]
    [InverseProperty("SUPAAcademicos")]
    public virtual SUPACatMotivos IdCatMotivosNavigation { get; set; } = null!;

    [ForeignKey("IdCatNacionalidad")]
    [InverseProperty("SUPAAcademicos")]
    public virtual SUPACatNacionalidades IdCatNacionalidadNavigation { get; set; } = null!;

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPAApoyosEco> SUPAApoyosEco { get; set; } = new List<SUPAApoyosEco>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPAAreaDedica> SUPAAreaDedica { get; set; } = new List<SUPAAreaDedica>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPACitas> SUPACitas { get; set; } = new List<SUPACitas>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPAContrataciones> SUPAContrataciones { get; set; } = new List<SUPAContrataciones>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPADescargasA> SUPADescargasA { get; set; } = new List<SUPADescargasA>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPADisciplinas> SUPADisciplinas { get; set; } = new List<SUPADisciplinas>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPAEntidades> SUPAEntidades { get; set; } = new List<SUPAEntidades>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPAEstudios> SUPAEstudios { get; set; } = new List<SUPAEstudios>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPAMiembrosCA> SUPAMiembrosCA { get; set; } = new List<SUPAMiembrosCA>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPANivelesSNII> SUPANivelesSNII { get; set; } = new List<SUPANivelesSNII>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPAPlazas> SUPAPlazas { get; set; } = new List<SUPAPlazas>();

    [InverseProperty("IdSUPANavigation")]
    public virtual ICollection<SUPAVigenciaPerfil> SUPAVigenciaPerfiles { get; set; } = new List<SUPAVigenciaPerfil>();
}
