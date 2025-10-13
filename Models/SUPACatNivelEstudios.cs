using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace supa.Models;

[Table("SUPACatNivelEstudios", Schema = "dbo")]
public partial class SUPACatNivelEstudios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCatNivelEstudios { get; set; }

    [Required]
    [StringLength(100)]
    public string DescripcionNivelEstudios { get; set; } = null!;

    [InverseProperty("IdCatNivelEstudiosNavigation")]
    public virtual ICollection<SUPAEstudios> SUPAEstudios { get; set; } = new List<SUPAEstudios>();
}
