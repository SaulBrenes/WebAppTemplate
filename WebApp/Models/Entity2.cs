namespace WebApp.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

	// This is the "detail" entity (the "N" or "Many" side)
	public class Entity2
	{
		#region Atributes
		[Key]
		public int Entity2Id { get; set; }

		[Required]
		[MaxLength(50)]
		public required string AnotherRequiredField { get; set; } 

		// --- Foreign Key (FK) ---
		[Display(Name = "Main Entity")]
		public int Entity1Id { get; set; }
		#endregion

		#region Relations
		[ForeignKey("Entity1Id")]
		[ValidateNever]
		public Entity1 RelatedEntity1 { get; set; } = null!;
		#endregion
	}
}
