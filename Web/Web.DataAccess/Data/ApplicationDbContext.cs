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
    public DbSet<ConferenceEntry> ConferenceEntries => Set<ConferenceEntry>();
    public DbSet<ImportantDates> ImportantDates => Set<ImportantDates>();
    public DbSet<FoodOptions> FoodOptions => Set<FoodOptions>();
    public DbSet<BookingOptions> BookingOptions => Set<BookingOptions>();
    public DbSet<ProgramDay> ProgramDays => Set<ProgramDay>();
    public DbSet<ProgramItem> ProgramItems => Set<ProgramItem>();
    public DbSet<ProgramSession> ProgramSessions => Set<ProgramSession>();
    public DbSet<ProgramPresentation> ProgramPresentations => Set<ProgramPresentation>();

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

        modelBuilder.Entity<ConferenceSettings>()
            .HasMany(settings => settings.ConferenceEntries)
            .WithOne(entry => entry.ConferenceSettings)
            .HasForeignKey(entry => entry.ConferenceSettingsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<ImportantDates>()
            .Property(date => date.NormalDate)
            .HasColumnType("date");

        modelBuilder.Entity<ImportantDates>()
            .Property(date => date.UpdatedDate)
            .HasColumnType("date");

        modelBuilder.Entity<ConferenceSettings>()
            .HasMany(settings => settings.FoodOptions)
            .WithOne(option => option.ConferenceSettings)
            .HasForeignKey(option => option.ConferenceSettingsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<ConferenceSettings>()
            .HasMany(settings => settings.BookingOptions)
            .WithOne(option => option.ConferenceSettings)
            .HasForeignKey(option => option.ConferenceSettingsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<FoodOptions>()
            .Property(option => option.Date)
            .HasColumnType("date");

        modelBuilder.Entity<BookingOptions>()
            .Property(option => option.StartDate)
            .HasColumnType("date");

        modelBuilder.Entity<BookingOptions>()
            .Property(option => option.EndDate)
            .HasColumnType("date");

        modelBuilder.Entity<ConferenceSettings>()
            .HasMany(settings => settings.ProgramDays)
            .WithOne(day => day.ConferenceSettings)
            .HasForeignKey(day => day.ConferenceSettingsId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<ProgramDay>()
            .Property(day => day.Date)
            .HasColumnType("date");

        modelBuilder.Entity<ProgramDay>()
            .HasMany(day => day.ProgramItems)
            .WithOne(item => item.ProgramDay)
            .HasForeignKey(item => item.ProgramDayId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<ProgramItem>()
            .HasMany(item => item.ProgramSessions)
            .WithOne(session => session.ProgramItem)
            .HasForeignKey(session => session.ProgramItemId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<ProgramSession>()
            .HasMany(session => session.ProgramPresentations)
            .WithOne(presentation => presentation.ProgramSession)
            .HasForeignKey(presentation => presentation.ProgramSessionId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

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

        modelBuilder.Entity<Participant>()
            .HasOne(p => p.ConferenceEntry)
            .WithMany()
            .HasForeignKey(p => p.ConferenceEntryId)
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
