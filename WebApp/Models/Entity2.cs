namespace WebApp.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema; // Necesario para [ForeignKey]
	using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

	// This is the "detail" entity (the "N" or "Many" side)
	public class Entity2
	{
		[Key]
		public int Entity2Id { get; set; }

		[Required] // Simple validation (will use the default error message)
		public string AnotherRequiredField { get; set; }

		// --- Foreign Key (FK) ---

		// 1. This property stores the ID for the foreign key.
		[Display(Name = "Main Entity")] // Used for the DropDownList in the view
		public int Entity1Id { get; set; }

		// --- Navigation Property ("ONE" Side) ---

		// An Entity2 belongs to ONE Entity1 (e.g., A Vehicle belongs to ONE Client)

		// 1. [ForeignKey("Entity1Id")]: Tells EF Core that this property
		//    (RelatedEntity1) is "loaded" using the ID from the Entity1Id property.
		[ForeignKey("Entity1Id")]

		// 2. [ValidateNever]: CRUCIAL! Tells ASP.NET to NOT validate
		//    this object during a POST, since it will only receive the Entity1Id.
		[ValidateNever]

		// 3. = null!: (Null-forgiving operator). Tells the compiler:
		//    "Trust me, this property will NEVER be null because
		//    Entity Framework will populate it for me."
		public Entity1 RelatedEntity1 { get; set; } = null!;
	}
}
