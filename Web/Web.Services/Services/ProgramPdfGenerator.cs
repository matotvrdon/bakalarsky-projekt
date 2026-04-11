using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Web.Domain.Enums;
using Web.Domain.Models;
using Web.Services.Abstractions;

namespace Web.Services.Services;

public class ProgramPdfGenerator : IProgramPdfGenerator
{
    private const string TextPrimary = "#111827";
    private const string TextSecondary = "#4B5563";
    private const string TextMuted = "#6B7280";
    private const string BorderNeutral = "#E5E7EB";
    private const string BackgroundSoft = "#F8FAFC";
    private static readonly CultureInfo SlovakCulture = CultureInfo.GetCultureInfo("sk-SK");

    public byte[] GenerateProgramPdf(ConferenceSettings conferenceSettings, List<ProgramDay> programDays)
    {
        var sortedDays = programDays
            .OrderBy(day => day.Order)
            .ToList();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10.5f));

                page.Content()
                    .Element(container => ComposeContent(container, conferenceSettings, sortedDays));

                page.Footer()
                    .PaddingTop(8)
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Strana ").FontColor(TextMuted);
                        text.CurrentPageNumber().FontColor(TextMuted);
                        text.Span(" / ").FontColor(TextMuted);
                        text.TotalPages().FontColor(TextMuted);
                    });
            });
        });

        return document.GeneratePdf();
    }

    private static void ComposeHeader(IContainer container, ConferenceSettings conferenceSettings)
    {
        var conference = conferenceSettings.Conference;
        var dateRange = FormatConferenceDateRange(conference);

        container
            .Background(BackgroundSoft)
            .Border(1)
            .BorderColor(BorderNeutral)
            .CornerRadius(16)
            .Padding(16)
            .Column(column =>
            {
                column.Spacing(6);

                column.Item()
                    .Text("Program konferencie")
                    .FontSize(24)
                    .Bold()
                    .FontColor(TextPrimary);

                column.Item()
                    .Text(string.IsNullOrWhiteSpace(conference?.Name) ? "Conference.Name" : conference.Name)
                    .FontSize(16)
                    .SemiBold()
                    .FontColor(TextPrimary);

                column.Item()
                    .Text(dateRange)
                    .FontSize(12)
                    .FontColor(TextSecondary);

                if (!string.IsNullOrWhiteSpace(conference?.Location))
                {
                    column.Item()
                        .Text(conference.Location)
                        .FontSize(10.5f)
                        .FontColor(TextMuted);
                }
            });
    }

    private static void ComposeContent(IContainer container, ConferenceSettings conferenceSettings, IReadOnlyList<ProgramDay> sortedDays)
    {
        container.Column(column =>
        {
            column.Spacing(16);

            column.Item().Element(headerContainer => ComposeHeader(headerContainer, conferenceSettings));
            column.Item().Element(ComposeLegend);

            if (sortedDays.Count == 0)
            {
                column.Item()
                    .Background(Colors.White)
                    .Border(1)
                    .BorderColor(BorderNeutral)
                    .CornerRadius(14)
                    .Padding(18)
                    .Text("Program pre aktívnu konferenciu zatiaľ nie je dostupný.")
                    .FontColor(TextSecondary);

                return;
            }

            foreach (var day in sortedDays)
            {
                column.Item().Element(dayContainer => ComposeProgramDay(dayContainer, day));
            }
        });
    }

    private static void ComposeLegend(IContainer container)
    {
        container
            .Border(1)
            .BorderColor(BorderNeutral)
            .CornerRadius(14)
            .Padding(14)
            .Column(column =>
            {
                column.Spacing(8);

                column.Item()
                    .Text("Legenda")
                    .FontSize(12)
                    .SemiBold()
                    .FontColor(TextPrimary);

                column.Item().Row(row =>
                {
                    row.Spacing(10);
                    row.RelativeItem().Element(item => ComposeLegendItem(item, "#3B82F6", "Pozvané prednášky"));
                    row.RelativeItem().Element(item => ComposeLegendItem(item, "#22C55E", "Paralelné sekcie"));
                    row.RelativeItem().Element(item => ComposeLegendItem(item, "#A855F7", "Workshopy"));
                });

                column.Item().Row(row =>
                {
                    row.Spacing(10);
                    row.RelativeItem().Element(item => ComposeLegendItem(item, "#EC4899", "Spoločenské"));
                    row.RelativeItem().Element(item => ComposeLegendItem(item, "#9CA3AF", "Prestávky"));
                    row.RelativeItem();
                });
            });
    }

    private static void ComposeLegendItem(IContainer container, string color, string label)
    {
        container.Row(row =>
        {
            row.Spacing(8);

            row.ConstantItem(12)
                .Height(12)
                .Background(color)
                .CornerRadius(3);

            row.RelativeItem()
                .Text(label)
                .FontSize(9.5f)
                .FontColor(TextSecondary);
        });
    }

    private static void ComposeProgramDay(IContainer container, ProgramDay day)
    {
        var sortedItems = day.ProgramItems
            .OrderBy(item => item.Order)
            .ToList();

        container.Column(column =>
        {
            column.Spacing(10);

            column.Item().Row(row =>
            {
                row.RelativeItem()
                    .Background(TextPrimary)
                    .CornerRadius(999)
                    .PaddingHorizontal(14)
                    .PaddingVertical(8)
                    .Text(day.Label?.Trim() is { Length: > 0 } ? day.Label : FormatDayTab(day.Date))
                    .FontSize(13)
                    .SemiBold()
                    .FontColor(Colors.White);

                row.AutoItem()
                    .AlignRight()
                    .Text(FormatDate(day.Date))
                    .FontSize(11)
                    .FontColor(TextMuted);
            });

            if (sortedItems.Count == 0)
            {
                column.Item()
                    .Border(1)
                    .BorderColor(BorderNeutral)
                    .CornerRadius(12)
                    .Padding(14)
                    .Text("Tento deň zatiaľ nemá priradené žiadne body programu.")
                    .FontColor(TextSecondary);

                return;
            }

            foreach (var item in sortedItems)
            {
                var itemEntry = column.Item();

                if (item.ProgramSessions.Count == 0)
                {
                    itemEntry = itemEntry.ShowEntire();
                }

                itemEntry.Element(itemContainer => ComposeProgramItem(itemContainer, item));
            }
        });
    }

    private static void ComposeProgramItem(IContainer container, ProgramItem item)
    {
        var palette = GetPalette(item.Type);
        var sessions = item.ProgramSessions
            .OrderBy(session => session.Order)
            .ToList();

        container
            .Border(1)
            .BorderColor(BorderNeutral)
            .CornerRadius(16)
            .Row(row =>
            {
                row.ConstantItem(8)
                    .Background(palette.Accent);

                row.RelativeItem()
                    .Background(palette.Background)
            .Padding(14)
                    .Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Row(topRow =>
                        {
                            topRow.Spacing(12);

                            topRow.ConstantItem(96)
                                .Element(timeContainer => ComposeTimeBadge(
                                    timeContainer,
                                    FormatTimeRange(item.StartTime, item.EndTime)));

                            topRow.RelativeItem().Column(contentColumn =>
                            {
                                contentColumn.Spacing(4);

                                contentColumn.Item()
                                    .Text(item.Title)
                                    .FontSize(13)
                                    .SemiBold()
                                    .FontColor(TextPrimary);

                                if (!string.IsNullOrWhiteSpace(item.Speaker))
                                {
                                    contentColumn.Item()
                                        .Text($"Prednášajúci: {item.Speaker}")
                                        .FontSize(10)
                                        .FontColor(TextSecondary);
                                }

                                if (!string.IsNullOrWhiteSpace(item.Chair) && sessions.Count == 0)
                                {
                                    contentColumn.Item()
                                        .Text($"Chair: {item.Chair}")
                                        .FontSize(10)
                                        .FontColor(TextSecondary);
                                }
                            });

                            if (!string.IsNullOrWhiteSpace(item.Location))
                            {
                                topRow.ConstantItem(120)
                                    .AlignRight()
                                    .Text(item.Location)
                                    .FontSize(10)
                                    .FontColor(TextSecondary);
                            }
                        });

                        if (sessions.Count > 0)
                        {
                            column.Item()
                                .Height(1)
                                .Background("#D1D5DB");

                            column.Item().Column(sessionColumn =>
                            {
                                sessionColumn.Spacing(10);

                                sessionColumn.Item()
                                    .Text($"Detail programu ({sessions.Count} sekcií)")
                                    .FontSize(11)
                                    .SemiBold()
                                    .FontColor("#1D4ED8");

                                foreach (var session in sessions)
                                {
                                    sessionColumn.Item().Element(sessionContainer => ComposeProgramSession(sessionContainer, session));
                                }
                            });
                        }
                    });
            });
    }

    private static void ComposeTimeBadge(IContainer container, string value)
    {
        container
            .Border(1)
            .BorderColor("#CBD5E1")
            .Background(Colors.White)
            .CornerRadius(10)
            .PaddingHorizontal(10)
            .PaddingVertical(6)
            .AlignMiddle()
            .AlignCenter()
            .Text(value)
            .FontSize(10)
            .SemiBold()
            .FontColor(TextPrimary);
    }

    private static void ComposeProgramSession(IContainer container, ProgramSession session)
    {
        var presentations = session.ProgramPresentations
            .OrderBy(presentation => presentation.Order)
            .ToList();

        container
            .Background(Colors.White)
            .Border(1)
            .BorderColor(BorderNeutral)
            .CornerRadius(12)
            .Padding(12)
            .Column(column =>
            {
                column.Spacing(8);

                column.Item().Row(row =>
                {
                    row.Spacing(12);

                    row.RelativeItem().Column(headerColumn =>
                    {
                        headerColumn.Spacing(2);

                        headerColumn.Item()
                            .Text(session.SessionName)
                            .FontSize(12)
                            .SemiBold()
                            .FontColor(TextPrimary);

                        headerColumn.Item()
                            .Text(FormatTimeRange(session.StartTime, session.EndTime))
                            .FontSize(10)
                            .FontColor(TextMuted);

                        if (!string.IsNullOrWhiteSpace(session.Chair))
                        {
                            headerColumn.Item()
                                .Text($"Chair: {session.Chair}")
                                .FontSize(10)
                                .FontColor(TextSecondary);
                        }
                    });
                });

                if (presentations.Count == 0)
                {
                    column.Item()
                        .Text("Táto sekcia zatiaľ nemá pridané žiadne príspevky.")
                        .FontSize(9.5f)
                        .FontColor(TextMuted);

                    return;
                }

                column.Item().Element(tableContainer => ComposePresentationTable(tableContainer, presentations));
            });
    }

    private static void ComposePresentationTable(IContainer container, IReadOnlyCollection<ProgramPresentation> presentations)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(88);
                columns.RelativeColumn(2);
                columns.RelativeColumn(3);
            });

            table.Header(header =>
            {
                header.Cell().Element(StyleHeaderCell).Text(text =>
                {
                    text.Span("Čas").SemiBold().FontSize(9.5f);
                });
                header.Cell().Element(StyleHeaderCell).Text(text =>
                {
                    text.Span("Autori").SemiBold().FontSize(9.5f);
                });
                header.Cell().Element(StyleHeaderCell).Text(text =>
                {
                    text.Span("Názov príspevku").SemiBold().FontSize(9.5f);
                });
            });

            foreach (var presentation in presentations)
            {
                table.Cell().Element(StyleBodyCell)
                    .Text(FormatTimeRange(presentation.StartTime, presentation.EndTime))
                    .FontSize(9)
                    .SemiBold()
                    .FontColor(TextPrimary);

                table.Cell().Element(StyleBodyCell)
                    .Text(presentation.Authors)
                    .FontSize(9)
                    .FontColor(TextPrimary);

                table.Cell().Element(StyleBodyCell)
                    .Text(presentation.Title)
                    .FontSize(9)
                    .Italic()
                    .FontColor(TextPrimary);
            }
        });
    }

    private static IContainer StyleHeaderCell(IContainer container)
    {
        return container
            .Background("#F3F4F6")
            .Border(1)
            .BorderColor(BorderNeutral)
            .PaddingHorizontal(6)
            .PaddingVertical(5);
    }

    private static IContainer StyleBodyCell(IContainer container)
    {
        return container
            .BorderLeft(1)
            .BorderRight(1)
            .BorderBottom(1)
            .BorderColor(BorderNeutral)
            .PaddingHorizontal(6)
            .PaddingVertical(5);
    }

    private static string FormatConferenceDateRange(Conference? conference)
    {
        if (conference == null)
        {
            return string.Empty;
        }

        if (conference.EndDate == default || conference.EndDate == conference.StartDate)
        {
            return FormatDate(conference.StartDate);
        }

        return $"{FormatDate(conference.StartDate)} - {FormatDate(conference.EndDate)}";
    }

    private static string FormatDate(DateOnly value)
    {
        return value.ToString("dd.MM.yyyy", SlovakCulture);
    }

    private static string FormatDayTab(DateOnly value)
    {
        return value
            .ToDateTime(TimeOnly.MinValue)
            .ToString("dddd dd.MM.", SlovakCulture);
    }

    private static string FormatTimeRange(TimeOnly startTime, TimeOnly endTime)
    {
        return $"{startTime:HH\\:mm} - {endTime:HH\\:mm}";
    }

    private static ProgramItemPalette GetPalette(ProgramItemType type)
    {
        return type switch
        {
            ProgramItemType.Keynote => new ProgramItemPalette("#3B82F6", "#EFF6FF"),
            ProgramItemType.Parallel or ProgramItemType.Session => new ProgramItemPalette("#22C55E", "#F0FDF4"),
            ProgramItemType.Workshop => new ProgramItemPalette("#A855F7", "#FAF5FF"),
            ProgramItemType.Panel => new ProgramItemPalette("#F97316", "#FFF7ED"),
            ProgramItemType.Break => new ProgramItemPalette("#9CA3AF", "#F9FAFB"),
            ProgramItemType.Social => new ProgramItemPalette("#EC4899", "#FDF2F8"),
            ProgramItemType.Poster => new ProgramItemPalette("#EAB308", "#FEFCE8"),
            ProgramItemType.Registration => new ProgramItemPalette("#6366F1", "#EEF2FF"),
            ProgramItemType.Opening => new ProgramItemPalette("#16A34A", "#DCFCE7"),
            ProgramItemType.Closing => new ProgramItemPalette("#EF4444", "#FEF2F2"),
            _ => new ProgramItemPalette("#D1D5DB", "#F9FAFB")
        };
    }

    private sealed record ProgramItemPalette(string Accent, string Background);
}
