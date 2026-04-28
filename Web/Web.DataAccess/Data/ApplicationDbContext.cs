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
    
    public DbSet<ConferenceCommittee> ConferenceCommittees { get; set; }
    public DbSet<CommitteeRole> CommitteeRoles { get; set; }
    public DbSet<CommitteeMember> CommitteeMembers { get; set; }
    public DbSet<SubmissionSettings> SubmissionSettings { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<InvoiceParticipant> InvoiceParticipants { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(x => x.Email)
                .IsUnique();

            entity.HasData(new User
            {
                Id = 9999,
                Email = "admin@admin",
                PasswordHash = "AQAAAAEAACcQAAAAEDwHjYoxtrvuR2EGHqT3BtCqcdNdrZM9ewhYCyC4DlfwJDIL0AnrqRenzXSgcCnVeA==",
                Role = 0,
                CreatedAt = new DateTime(2026, 4, 28, 0, 0, 0, DateTimeKind.Utc)
            });
        });

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
        
        modelBuilder.Entity<ConferenceCommittee>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Description)
                .HasMaxLength(1000);

            entity.HasOne(x => x.ConferenceSettings)
                .WithMany(x => x.ConferenceCommittees)
                .HasForeignKey(x => x.ConferenceSettingsId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CommitteeRole>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(x => x.ConferenceCommittee)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.ConferenceCommitteeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CommitteeMember>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(x => x.Position)
                .HasMaxLength(300);

            entity.Property(x => x.Affiliation)
                .HasMaxLength(300);

            entity.Property(x => x.Country)
                .HasMaxLength(50);

            entity.HasOne(x => x.CommitteeRole)
                .WithMany(x => x.Members)
                .HasForeignKey(x => x.CommitteeRoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<SubmissionSettings>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.IeeePdfExpressUrl)
                .HasMaxLength(500);

            entity.Property(x => x.EasyChairUrl)
                .HasMaxLength(500);

            entity.Property(x => x.ConferenceCode)
                .HasMaxLength(100);

            entity.Property(x => x.IeeeTemplateUrl)
                .HasMaxLength(500);

            entity.Property(x => x.LatexExample)
                .HasMaxLength(500);

            entity.Property(x => x.ExtraPagePrice)
                .HasColumnType("decimal(10,2)");

            entity.HasOne(x => x.ConferenceSettings)
                .WithOne(x => x.SubmissionSettings)
                .HasForeignKey<SubmissionSettings>(x => x.ConferenceSettingsId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(invoice => invoice.Id);

            entity.Property(invoice => invoice.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(64);

            entity.Property(invoice => invoice.SharedCode)
                .HasMaxLength(32);

            entity.Property(invoice => invoice.TotalAmount)
                .HasColumnType("numeric(10,2)");

            entity.Property(invoice => invoice.CompanyName)
                .HasMaxLength(255);

            entity.Property(invoice => invoice.Ico)
                .HasMaxLength(32);

            entity.Property(invoice => invoice.Dic)
                .HasMaxLength(32);

            entity.Property(invoice => invoice.CustomerName)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(invoice => invoice.CompanyName)
                .HasMaxLength(255);

            entity.Property(invoice => invoice.BillingAddress)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(invoice => invoice.Ico)
                .HasMaxLength(32);

            entity.Property(invoice => invoice.Dic)
                .HasMaxLength(32);

            entity.Property(invoice => invoice.VatId)
                .HasMaxLength(32);

            entity.HasIndex(invoice => invoice.InvoiceNumber)
                .IsUnique();

            entity.HasIndex(invoice => invoice.SharedCode)
                .IsUnique();
            
            entity.Property(invoice => invoice.CreatedAtUtc)
                .IsRequired();

            entity.Property(invoice => invoice.DueDateUtc)
                .IsRequired();
            
            entity.HasOne(invoice => invoice.FileManager)
                .WithMany()
                .HasForeignKey(invoice => invoice.FileManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(invoice => invoice.Conference)
                .WithMany()
                .HasForeignKey(invoice => invoice.ConferenceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(item => item.Id);

            entity.Property(item => item.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(item => item.UnitPrice)
                .HasColumnType("numeric(10,2)");

            entity.Property(item => item.TotalPrice)
                .HasColumnType("numeric(10,2)");

            entity.HasOne(item => item.Invoice)
                .WithMany(invoice => invoice.Items)
                .HasForeignKey(item => item.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(item => item.Participant)
                .WithMany()
                .HasForeignKey(item => item.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InvoiceParticipant>(entity =>
        {
            entity.HasKey(invoiceParticipant => invoiceParticipant.Id);

            entity.HasIndex(invoiceParticipant => new
            {
                invoiceParticipant.InvoiceId,
                invoiceParticipant.ParticipantId
            }).IsUnique();

            entity.HasOne(invoiceParticipant => invoiceParticipant.Invoice)
                .WithMany(invoice => invoice.Participants)
                .HasForeignKey(invoiceParticipant => invoiceParticipant.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(invoiceParticipant => invoiceParticipant.Participant)
                .WithMany()
                .HasForeignKey(invoiceParticipant => invoiceParticipant.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<Supplier>(e =>
        {
            e.HasData(
                new Supplier {
                    Id = 1,
                    Name = "Pobočka SSAKI pri KPI FEI TU v Košiciach",
                    Street = "Letná 9",
                    City = "Košice",
                    PostalCode = "040 01",
                    Country = "Slovanská Republika",
                    Ico = "35541784",
                    Dic = "2021681970",
                    IcDph = "SK2021681970",
                    Bank = "Všeobecná úverová banka, a.s.",
                    Address = "Letná 40",
                    AddressPostalCode = "040 01",
                    AddressCity = "Košice",
                    BankAccount = "1584034953/0200",
                    Swift = "SUBASKBX",
                    Iban = "SK55 0200 0000 0015 8403 4953",
                    Phone = "+421/(0)55/602 4148"
                }
            );
        });
    }
}
