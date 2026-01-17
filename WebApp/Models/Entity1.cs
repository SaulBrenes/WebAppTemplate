using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApp.Models
{
	// This is the primary or "master" entity (the "1" side)
	public class Entity1
	{
		#region Atributes
		[Key] // Marks this property as the Primary Key (PK)
		public int Entity1Id { get; set; }
		
		[Required(ErrorMessage = "This field is required.")] // Server-side and client-side validation
		[MaxLength(50)]
		[Display(Name = "Descriptive Name")] // This is how it will appear in the view's <label>
		public string Name { get; set; } = String.Empty;

		[Required(ErrorMessage = "The field is required")] // Server-side and client-side validation
		[Display(Name = "DecimalField")] // This is how it will appear in the view's <label>
		[Column(TypeName = "decimal(18, 2)")]
		public decimal DecimalField { get; set; }

		[MaxLength(50)]
		[Display(Name = "Optional Description")]
		public string? OptionalDescription { get; set; } // The '?' allows this to be null in the DB

		[ScaffoldColumn(false)]
		public DateTime FechaCreacion { get; set; }

		[ScaffoldColumn(false)]
		public DateTime? FechaModificacion { get; set; }

		[ScaffoldColumn(false)]
		public bool Activo { get; set; }

		[NotMapped]
		[Display(Name = "ViewProperty")]
		public string InformationView => $"{Name} - {OptionalDescription}";
		#endregion

		#region Relations
		// --- Navigation Property ("MANY" Side) ---

		// An Entity1 has MANY Entity2 (e.g., A Client has MANY Vehicles)

		// 1. [ValidateNever]: CRUCIAL! Tells ASP.NET to NOT validate
		//    this list during a POST. Without this, ModelState.IsValid will fail.

		// 2. new List<>(): The list is initialized to prevent
		//    NullReferenceException errors if you try to .Add() to a null list.
		[ValidateNever]
		public ICollection<Entity2> RelatedEntities2 { get; set; } = new List<Entity2>();
		#endregion
	}
}