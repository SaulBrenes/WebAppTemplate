using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApp.Models
{
	// This is the primary or "master" entity (the "1" side)
	public class Entity1
	{
		#region Atributes
		[Key]
		public int Entity1Id { get; set; }

		[Required(ErrorMessage = "This field is required.")] 
		[MaxLength(50)]
		[Display(Name = "Descriptive Name")] 
		public required string Name { get; set; }

		[Required(ErrorMessage = "The field is required")] 
		[Display(Name = "DecimalField")] 
		[Column(TypeName = "decimal(18, 2)")]
		public decimal DecimalField { get; set; }

		[MaxLength(50)]
		[Display(Name = "Optional Description")]
		public string? OptionalDescription { get; set; }

		[ScaffoldColumn(false)]
		public bool Eliminado { get; set; }

		[NotMapped]
		[Display(Name = "ViewProperty")]
		public string InformationView => $"{Name} - {OptionalDescription}";
		#endregion

		#region Relations
		[ValidateNever]
		public ICollection<Entity2> RelatedEntities2 { get; set; } = new List<Entity2>();
		#endregion
	}
}