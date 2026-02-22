using Microsoft.EntityFrameworkCore;
using Web.Domain.Models;

namespace Web.DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Conference> Conferences => Set<Conference>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<FileManager> FileManagers => Set<FileManager>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();
        
        modelBuilder.Entity<Participant>()
            .HasIndex(p => new { p.UserId, p.ConferenceId })
            .IsUnique();
    }
}
