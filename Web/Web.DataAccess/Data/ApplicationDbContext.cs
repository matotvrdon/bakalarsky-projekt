using Microsoft.EntityFrameworkCore;
using Web.Domain.Models;

namespace Web.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<Conference> Conferences => Set<Conference>();
    public DbSet<ConferenceSettings> ConferenceSettings => Set<ConferenceSettings>();
    public DbSet<Registration> Registrations => Set<Registration>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<SubmissionCategory> SubmissionCategories => Set<SubmissionCategory>();
    public DbSet<Speaker> Speakers => Set<Speaker>();
    public DbSet<ScheduleItem> ScheduleItems => Set<ScheduleItem>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<InvoiceParticipant> InvoiceParticipants => Set<InvoiceParticipant>();
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<AccommodationOption> AccommodationOptions => Set<AccommodationOption>();
    public DbSet<AccommodationBooking> AccommodationBookings => Set<AccommodationBooking>();
    public DbSet<CateringOption> CateringOptions => Set<CateringOption>();
    public DbSet<CateringOrder> CateringOrders => Set<CateringOrder>();
    public DbSet<CateringOrderItem> CateringOrderItems => Set<CateringOrderItem>();
    public DbSet<InvitationEmail> InvitationEmails => Set<InvitationEmail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<InvoiceParticipant>()
            .HasKey(x => new { x.InvoiceId, x.ParticipantId });

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Coupon>()
            .HasIndex(x => x.Code)
            .IsUnique();

        modelBuilder.Entity<ConferenceSettings>()
            .HasIndex(x => x.ConferenceId)
            .IsUnique();

        modelBuilder.Entity<Invoice>()
            .Property(x => x.Subtotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .Property(x => x.Discount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Invoice>()
            .Property(x => x.Total)
            .HasPrecision(18, 2);

        modelBuilder.Entity<InvoiceItem>()
            .Property(x => x.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<InvoiceItem>()
            .Property(x => x.LineTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Coupon>()
            .Property(x => x.Value)
            .HasPrecision(18, 2);

        modelBuilder.Entity<AccommodationOption>()
            .Property(x => x.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<AccommodationBooking>()
            .Property(x => x.Total)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CateringOption>()
            .Property(x => x.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CateringOrder>()
            .Property(x => x.Total)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CateringOrderItem>()
            .Property(x => x.Price)
            .HasPrecision(18, 2);
    }
}
