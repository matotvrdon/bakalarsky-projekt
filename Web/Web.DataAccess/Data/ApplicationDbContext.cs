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
    public DbSet<Submission> Submissions => Set<Submission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Participant>()
            .HasIndex(p => new { p.UserId, p.ConferenceId })
            .IsUnique()
            .HasFilter("\"UserId\" IS NOT NULL");

        modelBuilder.Entity<Participant>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Submission>()
            .HasIndex(s => s.ParticipantId)
            .IsUnique();

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Participant)
            .WithOne()
            .HasForeignKey<Submission>(s => s.ParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Submission>()
            .HasOne(s => s.Conference)
            .WithMany()
            .HasForeignKey(s => s.ConferenceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
