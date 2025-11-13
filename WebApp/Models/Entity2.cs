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
		public string AnotherRequiredField { get; set; } = string.Empty;

		// --- Foreign Key (FK) ---
		[Display(Name = "Main Entity")]
		public int Entity1Id { get; set; }
		#endregion

		#region Relations
		// --- Navigation Property ("ONE" Side) ---

		// An Entity2 belongs to ONE Entity1 (e.g., A Vehicle belongs to ONE Client)

		// 1. [ForeignKey("Entity1Id")]: Tells EF Core that this property
		//    (RelatedEntity1) is "loaded" using the ID from the Entity1Id property.

		// 2. [ValidateNever]: CRUCIAL! Tells ASP.NET to NOT validate
		//    this object during a POST, since it will only receive the Entity1Id.

		// 3. = null!: (Null-forgiving operator). Tells the compiler:
		//    "Trust me, this property will NEVER be null because
		//    Entity Framework will populate it for me."
		[ForeignKey("Entity1Id")]
		[ValidateNever]
		public Entity1 RelatedEntity1 { get; set; } = null!;
		#endregion
	}
}
