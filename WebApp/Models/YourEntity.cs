using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class YourEntity
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }
		[Display(Name = "Created At")]
		public DateTime CreatedAt { get;  set; }
		public int Status { get; set; }
		[ValidateNever]
		public string Description { get; set; }

	}
}
