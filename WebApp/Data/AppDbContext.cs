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

		// Define your DbSets here
		// public DbSet<YourEntity> YourEntities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure your entity mappings here
			modelBuilder.Entity<YourEntity>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).UseIdentityColumn().ValueGeneratedOnAdd();
				entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
				entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
				entity.Property(e => e.Status).HasDefaultValue(1);
				// Additional configurations...
			});

			// Example: Map to specific table name
			modelBuilder.Entity<YourEntity>().ToTable("YourEntity");

			// Seed initial data if necessary
			modelBuilder.Entity<YourEntity>().HasData(
				new YourEntity { Id = 1, Name = "Sample1", CreatedAt = DateTime.Now, Status = 1 },
				new YourEntity { Id = 2, Name = "Sample2", CreatedAt = DateTime.Now, Status = 1 }
				);
		}
	}
}
