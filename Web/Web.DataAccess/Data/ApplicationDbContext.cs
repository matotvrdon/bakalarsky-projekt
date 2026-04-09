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
    public DbSet<ConferenceSettings> ConferenceSettings => Set<ConferenceSettings>();
    public DbSet<ImportantDates> ImportantDates => Set<ImportantDates>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Conference>()
            .Property(conference => conference.StartDate)
            .HasColumnType("date");

        modelBuilder.Entity<Conference>()
            .Property(conference => conference.EndDate)
            .HasColumnType("date");

        modelBuilder.Entity<Conference>()
            .HasOne(conference => conference.Settings)
            .WithOne(settings => settings.Conference)
            .HasForeignKey<ConferenceSettings>(settings => settings.ConferenceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ConferenceSettings>()
            .HasMany(settings => settings.ImportantDates)
            .WithOne(date => date.ConferenceSettings)
            .HasForeignKey(date => date.ConferenceSettingsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<ImportantDates>()
            .Property(date => date.NormalDate)
            .HasColumnType("date");

        modelBuilder.Entity<ImportantDates>()
            .Property(date => date.UpdatedDate)
            .HasColumnType("date");

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
