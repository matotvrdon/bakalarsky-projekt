using Microsoft.EntityFrameworkCore;
using Web.Domain.Models;

namespace Web.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Conference> Conference { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Supplier>(e =>
        {
            e.HasData(
                new Supplier {
                    Id = 1,
                    Name = "Pobočka SSAKI pri KPI FEI TU v Košiciach",
                    Street = "Letná 9",
                    City = "Košice",
                    PostalCode = "040 01",
                    Country = "Slovanská Republika"
                }
            );
        });
    }
}