using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Innit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Ico = table.Column<string>(type: "text", nullable: false),
                    Dic = table.Column<string>(type: "text", nullable: false),
                    IcDph = table.Column<string>(type: "text", nullable: false),
                    Bank = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    AddressPostalCode = table.Column<string>(type: "text", nullable: false),
                    AddressCity = table.Column<string>(type: "text", nullable: false),
                    BankAccount = table.Column<string>(type: "text", nullable: false),
                    Swift = table.Column<string>(type: "text", nullable: false),
                    Iban = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferenceSettings_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingOptions_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceCommittees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferenceCommittees_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConferenceEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferenceEntries_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    FoodOptionsType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodOptions_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportantDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    NormalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ImportantDatesStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportantDates_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramDays_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    IeeePdfExpressUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EasyChairUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ConferenceCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FinalPaperDeadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IeeeTemplateUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LatexExample = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MaxPages = table.Column<int>(type: "integer", nullable: false),
                    ExtraPagePrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AbstractMinWords = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionSettings_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommitteeRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceCommitteeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitteeRoles_ConferenceCommittees_ConferenceCommitteeId",
                        column: x => x.ConferenceCommitteeId,
                        principalTable: "ConferenceCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Affiliation = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    ConferenceEntryId = table.Column<int>(type: "integer", nullable: true),
                    IsStudent = table.Column<bool>(type: "boolean", nullable: false),
                    IsPresenting = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    ConferenceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_ConferenceEntries_ConferenceEntryId",
                        column: x => x.ConferenceEntryId,
                        principalTable: "ConferenceEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Participants_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProgramItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramDayId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Speaker = table.Column<string>(type: "text", nullable: true),
                    Chair = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramItems_ProgramDays_ProgramDayId",
                        column: x => x.ProgramDayId,
                        principalTable: "ProgramDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommitteeMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommitteeRoleId = table.Column<int>(type: "integer", nullable: false),
                    FullName = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Position = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Affiliation = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitteeMembers_CommitteeRoles_CommitteeRoleId",
                        column: x => x.CommitteeRoleId,
                        principalTable: "CommitteeRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParticipantId = table.Column<int>(type: "integer", nullable: false),
                    FileType = table.Column<int>(type: "integer", nullable: false),
                    FileStatus = table.Column<int>(type: "integer", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    OriginalFileName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReviewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReviewedByUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileManagers_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParticipantId = table.Column<int>(type: "integer", nullable: false),
                    ConferenceId = table.Column<int>(type: "integer", nullable: false),
                    SubmissionIdentifier = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    IsPresenting = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramItemId = table.Column<int>(type: "integer", nullable: false),
                    SessionName = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Chair = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramSessions_ProgramItems_ProgramItemId",
                        column: x => x.ProgramItemId,
                        principalTable: "ProgramItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SharedCode = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CustomerType = table.Column<int>(type: "integer", nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    BillingAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Ico = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Dic = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    VatId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaidAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FileManagerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_FileManagers_FileManagerId",
                        column: x => x.FileManagerId,
                        principalTable: "FileManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProgramPresentations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramSessionId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Authors = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramPresentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramPresentations_ProgramSessions_ProgramSessionId",
                        column: x => x.ProgramSessionId,
                        principalTable: "ProgramSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    ParticipantId = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SourceId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    ParticipantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceParticipants_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceParticipants_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "AddressCity", "AddressPostalCode", "Bank", "BankAccount", "City", "Country", "Dic", "Iban", "IcDph", "Ico", "Name", "Phone", "PostalCode", "Street", "Swift" },
                values: new object[] { 1, "Letná 40", "Košice", "040 01", "Všeobecná úverová banka, a.s.", "1584034953/0200", "Košice", "Slovanská Republika", "2021681970", "SK55 0200 0000 0015 8403 4953", "SK2021681970", "35541784", "Pobočka SSAKI pri KPI FEI TU v Košiciach", "+421/(0)55/602 4148", "040 01", "Letná 9", "SUBASKBX" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Role" },
                values: new object[] { 9999, new DateTime(2026, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), "admin@admin", "AQAAAAEAACcQAAAAEDwHjYoxtrvuR2EGHqT3BtCqcdNdrZM9ewhYCyC4DlfwJDIL0AnrqRenzXSgcCnVeA==", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_BookingOptions_ConferenceSettingsId",
                table: "BookingOptions",
                column: "ConferenceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeMembers_CommitteeRoleId",
                table: "CommitteeMembers",
                column: "CommitteeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeRoles_ConferenceCommitteeId",
                table: "CommitteeRoles",
                column: "ConferenceCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceCommittees_ConferenceSettingsId",
                table: "ConferenceCommittees",
                column: "ConferenceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceEntries_ConferenceSettingsId",
                table: "ConferenceEntries",
                column: "ConferenceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceSettings_ConferenceId",
                table: "ConferenceSettings",
                column: "ConferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileManagers_ParticipantId",
                table: "FileManagers",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOptions_ConferenceSettingsId",
                table: "FoodOptions",
                column: "ConferenceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportantDates_ConferenceSettingsId",
                table: "ImportantDates",
                column: "ConferenceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ParticipantId",
                table: "InvoiceItems",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceParticipants_InvoiceId_ParticipantId",
                table: "InvoiceParticipants",
                columns: new[] { "InvoiceId", "ParticipantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceParticipants_ParticipantId",
                table: "InvoiceParticipants",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ConferenceId",
                table: "Invoices",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_FileManagerId",
                table: "Invoices",
                column: "FileManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SharedCode",
                table: "Invoices",
                column: "SharedCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ConferenceEntryId",
                table: "Participants",
                column: "ConferenceEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ConferenceId",
                table: "Participants",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId_ConferenceId",
                table: "Participants",
                columns: new[] { "UserId", "ConferenceId" },
                unique: true,
                filter: "\"UserId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDays_ConferenceSettingsId",
                table: "ProgramDays",
                column: "ConferenceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramItems_ProgramDayId",
                table: "ProgramItems",
                column: "ProgramDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPresentations_ProgramSessionId",
                table: "ProgramPresentations",
                column: "ProgramSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSessions_ProgramItemId",
                table: "ProgramSessions",
                column: "ProgramItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ConferenceId",
                table: "Submissions",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ParticipantId",
                table: "Submissions",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionSettings_ConferenceSettingsId",
                table: "SubmissionSettings",
                column: "ConferenceSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingOptions");

            migrationBuilder.DropTable(
                name: "CommitteeMembers");

            migrationBuilder.DropTable(
                name: "FoodOptions");

            migrationBuilder.DropTable(
                name: "ImportantDates");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "InvoiceParticipants");

            migrationBuilder.DropTable(
                name: "ProgramPresentations");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "SubmissionSettings");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "CommitteeRoles");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "ProgramSessions");

            migrationBuilder.DropTable(
                name: "ConferenceCommittees");

            migrationBuilder.DropTable(
                name: "FileManagers");

            migrationBuilder.DropTable(
                name: "ProgramItems");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "ProgramDays");

            migrationBuilder.DropTable(
                name: "ConferenceEntries");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ConferenceSettings");

            migrationBuilder.DropTable(
                name: "Conferences");
        }
    }
}
