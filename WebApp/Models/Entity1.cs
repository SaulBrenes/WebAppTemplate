using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace WebApp.Models
{
	// This is the primary or "master" entity (the "1" side)
	public class Entity1
	{
		[Key] // Marks this property as the Primary Key (PK)
		public int Entity1Id { get; set; }

		[Display(Name = "Descriptive Name")] // This is how it will appear in the view's <label>
		[Required(ErrorMessage = "This field is required.")] // Server-side and client-side validation
		public string Name { get; set; }

		[Display(Name = "Optional Description")]
		public string? OptionalDescription { get; set; } // The '?' allows this to be null in the DB

		[Display(Name = "Creation Date")]
		public DateTime CreatedAt { get; set; }

		[Display(Name = "Active Status")]
		public bool Status { get; set; }

		// --- Navigation Property ("MANY" Side) ---

		// An Entity1 has MANY Entity2 (e.g., A Client has MANY Vehicles)

		// 1. [ValidateNever]: CRUCIAL! Tells ASP.NET to NOT validate
		//    this list during a POST. Without this, ModelState.IsValid will fail.
		[ValidateNever]

		// 2. new List<>(): The list is initialized to prevent
		//    NullReferenceException errors if you try to .Add() to a null list.
		public ICollection<Entity2> RelatedEntities2 { get; set; } = new List<Entity2>();
	}
}