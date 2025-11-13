using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		// --- 1. DbSets ---
		// Define tables that EF Core management.
		public DbSet<Entity1> Entities1 { get; set; }
		public DbSet<Entity2> Entities2 { get; set; }


		// --- 2. Fluent API Configuration ---
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder); // Reading annotations first

			// Configuration Entity1
			modelBuilder.Entity<Entity1>(entity =>
			{
				entity.ToTable("Entity1"); // Mapping the table name

				// additional configurations for properties
				entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
				entity.Property(e => e.Status).HasDefaultValue(true);
			});

			// Configuration Entity2 ---
			modelBuilder.Entity<Entity2>(entity =>
			{
				entity.ToTable("Entity2"); // Mapping the table name
				
				/* NOTE: The foreign key (FK) relationship is already configured by [ForeignKey] in the Entity2 class. 
				 *You would only come here if you wanted to change the deletion behavior (OnDelete)*/

				// Example of configuring the relationship with Fluent API
				entity.HasOne(e2 => e2.RelatedEntity1) // Side "One"
					  .WithMany(e1 => e1.RelatedEntities2) // Side "Many"
					  .HasForeignKey(e2 => e2.Entity1Id) // The FK property
					  .OnDelete(DeleteBehavior.ClientSetNull); // Example: When deleting Entity1, it sets Entity1Id to null
			});

			//3. Data Seeding
			modelBuilder.Entity<Entity1>().HasData(
				new Entity1
				{
					Entity1Id = 1,
					Name = "Sample Item 1 (Active)",
					CreatedAt = new DateTime(2025,11,28),
					Status = true
				},
				new Entity1
				{
					Entity1Id = 2,
					Name = "Sample Item 2 (Inactive)",
					CreatedAt = new DateTime(2025, 11, 28),
					Status = false
				}
			);
		}
	}
}
