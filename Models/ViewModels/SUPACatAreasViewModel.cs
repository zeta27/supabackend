using System.ComponentModel.DataAnnotations;

namespace supa.Models.ViewModels
{
	public class SUPACatAreasViewModel
	{
		public int? IdCatAreas { get; set; } // Null para INSERT, con valor para UPDATE

		[Required(ErrorMessage = "El nombre del área es requerido")]
		[StringLength(50, ErrorMessage = "El nombre del área no puede exceder 50 caracteres")]
		[Display(Name = "Nombre del Área")]
		public string Darea { get; set; } = null!;
	}
}