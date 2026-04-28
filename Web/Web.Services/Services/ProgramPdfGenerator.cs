using Microsoft.Extensions.Hosting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Web.Services.Abstractions;
using Web.Services.DTOs;

namespace Web.Services.Services;

public class ProgramPdfGenerator : IProgramPdfGenerator
{
    private readonly IHostEnvironment _hostEnvironment;

    public ProgramPdfGenerator(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public byte[] GenerateProgramPdf(ConferenceDto conference)
    {
        var programDays = conference.Settings?.ProgramDays?
            .OrderBy(day => day.Order)
            .ThenBy(day => day.Date)
            .ToList() ?? [];

        using var ms = new MemoryStream();

        var document = Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header().Row(row =>
                {
                    row.RelativeItem()
                        .Column(column =>
                        {
                            column.Item()
                                .Text(conference.Name)
                                .FontSize(16)
                                .SemiBold();

                            column.Item()
                                .Text("Conference Program")
                                .FontSize(10)
                                .FontColor(Colors.Grey.Darken1);
                        });

                    var logoPath = Path.Combine(
                        _hostEnvironment.ContentRootPath,
                        "Assets",
                        "logo.png"
                    );

                    if (File.Exists(logoPath))
                    {
                        row.ConstantItem(60).Image(logoPath);
                    }
                });

                page.Content()
                    .Padding(20)
                    .Column(dayColumn =>
                    {
                        dayColumn.Spacing(35);

                        if (programDays.Count == 0)
                        {
                            dayColumn.Item()
                                .Text("Program zatiaľ nie je dostupný.")
                                .FontSize(14)
                                .SemiBold();

                            return;
                        }

                        foreach (var day in programDays)
                        {
                            dayColumn.Item()
                                .Text(GetProgramDayTitle(day.Label, day.Date))
                                .FontSize(22)
                                .Bold();

                            var programItems = day.ProgramItems?
                                .OrderBy(item => item.Order)
                                .ThenBy(item => item.StartTime)
                                .ToList() ?? [];

                            if (programItems.Count == 0)
                            {
                                dayColumn.Item()
                                    .Text("Tento deň zatiaľ nemá žiadne položky programu.")
                                    .FontSize(11)
                                    .FontColor(Colors.Grey.Darken1);

                                continue;
                            }

                            dayColumn.Item().Column(itemColumn =>
                            {
                                itemColumn.Spacing(25);

                                foreach (var item in programItems)
                                {
                                    var sessions = item.Sessions?
                                        .OrderBy(session => session.Order)
                                        .ThenBy(session => session.StartTime)
                                        .ToList() ?? [];

                                    itemColumn.Item().Row(row =>
                                    {
                                        row.ConstantItem(110)
                                            .Text(FormatTimeRange(item.StartTime, item.EndTime))
                                            .FontSize(11)
                                            .Bold();

                                        row.RelativeItem().Column(contentColumn =>
                                        {
                                            contentColumn.Spacing(2);

                                            contentColumn.Item()
                                                .Text(item.Title)
                                                .FontSize(16)
                                                .Bold();

                                            if (!string.IsNullOrWhiteSpace(item.Location))
                                            {
                                                contentColumn.Item()
                                                    .Text(item.Location)
                                                    .FontSize(11)
                                                    .FontColor(Colors.Grey.Darken1);
                                            }

                                            if (!string.IsNullOrWhiteSpace(item.Speaker))
                                            {
                                                contentColumn.Item()
                                                    .Text($"Speaker: {item.Speaker}")
                                                    .FontSize(11);
                                            }

                                            if (!string.IsNullOrWhiteSpace(item.Chair) && sessions.Count == 0)
                                            {
                                                contentColumn.Item()
                                                    .Text($"Chair: {item.Chair}")
                                                    .FontSize(11);
                                            }
                                        });
                                    });

                                    if (sessions.Count > 0)
                                    {
                                        itemColumn.Item().Column(sessionColumn =>
                                        {
                                            sessionColumn.Spacing(15);

                                            foreach (var session in sessions)
                                            {
                                                sessionColumn.Item().Row(row =>
                                                {
                                                    row.ConstantItem(110)
                                                        .Text(FormatTimeRange(session.StartTime, session.EndTime))
                                                        .FontSize(11)
                                                        .Bold();

                                                    row.RelativeItem().Column(sessionContentColumn =>
                                                    {
                                                        sessionContentColumn.Spacing(2);

                                                        sessionContentColumn.Item()
                                                            .Text(session.SessionName)
                                                            .FontSize(14)
                                                            .SemiBold();

                                                        if (!string.IsNullOrWhiteSpace(session.Chair))
                                                        {
                                                            sessionContentColumn.Item()
                                                                .Text($"Chair: {session.Chair}")
                                                                .FontSize(11);
                                                        }
                                                    });
                                                });

                                                var presentations = session.Presentations?
                                                    .OrderBy(presentation => presentation.Order)
                                                    .ThenBy(presentation => presentation.StartTime)
                                                    .ToList() ?? [];

                                                if (presentations.Count == 0)
                                                {
                                                    continue;
                                                }

                                                sessionColumn.Item().Column(presentationColumn =>
                                                {
                                                    presentationColumn.Spacing(8);

                                                    foreach (var presentation in presentations)
                                                    {
                                                        presentationColumn.Item().Row(presentationRow =>
                                                        {
                                                            presentationRow.ConstantItem(110)
                                                                .Text(FormatTimeRange(
                                                                    presentation.StartTime,
                                                                    presentation.EndTime
                                                                ))
                                                                .FontSize(11);

                                                            presentationRow.RelativeItem()
                                                                .Column(presentationContentColumn =>
                                                                {
                                                                    presentationContentColumn.Spacing(2);

                                                                    presentationContentColumn.Item()
                                                                        .Text(presentation.Title)
                                                                        .FontSize(12)
                                                                        .SemiBold();

                                                                    if (!string.IsNullOrWhiteSpace(presentation.Authors))
                                                                    {
                                                                        presentationContentColumn.Item()
                                                                            .Text(presentation.Authors)
                                                                            .FontSize(11);
                                                                    }
                                                                });
                                                        });
                                                    }
                                                });
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.CurrentPageNumber();
                    });
            });
        });

        document.GeneratePdf(ms);

        return ms.ToArray();
    }

    private static string GetProgramDayTitle(string? label, DateOnly date)
    {
        var dateText = date
            .ToDateTime(TimeOnly.MinValue)
            .ToString("dddd, MMMM dd, yyyy");

        if (!string.IsNullOrWhiteSpace(label))
        {
            return $"{label} - {dateText}";
        }

        return dateText;
    }

    private static string FormatTimeRange(TimeOnly startTime, TimeOnly endTime)
    {
        return $"{startTime:HH\\:mm}–{endTime:HH\\:mm}";
    }
}