using System.Diagnostics;
using System.Globalization;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.DTOs.Invoice;
using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.Services
{
    public class PdfService : IPdfService
    {
        
        private readonly IInvoiceService _invoiceService;
        private readonly IConferenceService _conferenceService;

        public PdfService(IInvoiceService invoiceService, IConferenceService conferenceService)
        {
            _invoiceService = invoiceService;
            _conferenceService = conferenceService;
        }


        public async Task<byte[]> GeneratePdf(int invoiceId)
        {
            
            var invoiceDto = await _invoiceService.GetByIdAsync(invoiceId);

            if (invoiceDto == null)
            { 
                throw new Exception($"Invoice with id {invoiceId} not found.");
            }
            
            using var ms = new MemoryStream();

            var document = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));


                    page.Header()
                        .Height(30)
                        .Border(1)
                        .AlignRight()
                        .PaddingTop(5)
                        .PaddingRight(20)
                        .Text($"Faktúra: {invoiceDto.InvoiceNumber}").FontSize(20);


                    page.Content()
                        .Border(1)
                        .Column(column =>
                        {

                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Border(1)
                                    .Height(300)
                                    .PaddingLeft(10)
                                    .PaddingTop(10)
                                    .Column(column =>
                                    {
                                        column.Spacing(4);
                                        
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Name)) {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Dodávateľ: ").Bold();
                                                t.Span(invoiceDto.Supplier.Name);
                                            });
                                        }
                                        
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Street)) {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Ulica: ").Bold();
                                                t.Span(invoiceDto.Supplier.Street);
                                            });
                                        }
                                        
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.PostalCode)
                                           && !string.IsNullOrWhiteSpace(invoiceDto.Supplier.City)) 
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Mesto: ").Bold();
                                                t.Span(invoiceDto.Supplier.PostalCode);
                                                t.Span(" ");
                                                t.Span(invoiceDto.Supplier.City);
                                            });
                                        }
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Country))
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Štát: ").Bold();
                                                t.Span(invoiceDto.Supplier.Country);
                                            });
                                        }
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Ico))
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("IČO: ").Bold();
                                                t.Span(invoiceDto.Supplier.Ico);
                                            });
                                        }

                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Dic)) 
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("DIČ: ").Bold();
                                                t.Span(invoiceDto.Supplier.Dic);
                                            });
                                        }

                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.IcDph))
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("IČDPH: ").Bold();
                                                t.Span(invoiceDto.Supplier.IcDph);
                                            });
                                        }

                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Bank))
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Banka: ").Bold();
                                                t.Span(invoiceDto.Supplier.Bank);
                                            });
                                        }
                                        
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Address)
                                           && !string.IsNullOrWhiteSpace(invoiceDto.Supplier.AddressCity)
                                           && !string.IsNullOrWhiteSpace(invoiceDto.Supplier.AddressPostalCode))
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Adresa: ").Bold();
                                                t.Span(invoiceDto.Supplier.Address);
                                                t.Span("\n");
                                                t.Span(invoiceDto.Supplier.AddressPostalCode);
                                                t.Span(" ");
                                                t.Span(invoiceDto.Supplier.AddressCity);
                                            });
                                        }

                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Bank)) 
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Banka: ").Bold();
                                                t.Span(invoiceDto.Supplier.Bank);
                                            });
                                        }

                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.BankAccount))
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Číslo účtu: ").Bold();
                                                t.Span(invoiceDto.Supplier.BankAccount);
                                            });
                                        }
                                        
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Swift))
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("SWIFT: ").Bold();
                                                t.Span(invoiceDto.Supplier.Swift);
                                            });
                                        }
                                        
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Iban))
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("IBAN: ").Bold();
                                                t.Span(invoiceDto.Supplier.Iban);
                                            });
                                        }
                                        
                                        if(!string.IsNullOrWhiteSpace(invoiceDto.Supplier.Phone)) 
                                        {
                                            column.Item().Text(t =>
                                            {
                                                t.Span("Telefon: ").Bold();
                                                t.Span(invoiceDto.Supplier.Phone);
                                            });
                                        }

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Vystavil: ").Bold();
                                            // TODO: replace with actual user name
                                            t.Span(Environment.MachineName);
                                        });
                                    });

                                row.RelativeItem().Column(column =>
                                {
                                    column.Spacing(0);

                                    column.Item()
                                        .Border(1)
                                        .Height(200)
                                        .PaddingLeft(10)
                                        .PaddingTop(10)
                                        .Column(column =>
                                        {
                                            column.Spacing(4);
                                            
                                            if(!string.IsNullOrWhiteSpace(invoiceDto.Customer.Name))
                                            {
                                                column.Item().Text(t =>
                                                {
                                                    t.Span("Odberateľ: ").Bold();
                                                    t.Span(invoiceDto.Customer.Name);
                                                });
                                            }

                                            if(!string.IsNullOrWhiteSpace(invoiceDto.Customer.Street)
                                               && !string.IsNullOrWhiteSpace(invoiceDto.Customer.PostalCode)
                                               && !string.IsNullOrWhiteSpace(invoiceDto.Customer.City))
                                            {
                                                column.Item().Text(t =>
                                                {
                                                    t.Span("Adresa: ").Bold();
                                                    t.Span(invoiceDto.Customer.Street);
                                                    t.Span("\n");
                                                    t.Span(invoiceDto.Customer.PostalCode);
                                                    t.Span(" ");
                                                    t.Span(invoiceDto.Customer.City);
                                                });
                                            }

                                            if(!string.IsNullOrWhiteSpace(invoiceDto.Customer.Ico))
                                            {
                                                column.Item().Text(t =>
                                                {
                                                    t.Span("IČO: ").Bold();
                                                    t.Span(invoiceDto.Customer.Ico);
                                                });
                                            }
                                            if(!string.IsNullOrWhiteSpace(invoiceDto.Customer.Dic)) 
                                            {
                                                column.Item().Text(t =>
                                                {
                                                    t.Span("DIČ: ").Bold();
                                                    t.Span(invoiceDto.Customer.Dic);
                                                });
                                            }
                                            
                                            if(!string.IsNullOrWhiteSpace(invoiceDto.Customer.IcDph)) 
                                            {
                                                column.Item().Text(t =>
                                                {
                                                    t.Span("IČDPH: ").Bold();
                                                    t.Span(invoiceDto.Customer.IcDph);
                                                });
                                            }
                                        });
                                    
                                    column.Item()
                                        .Border(1)
                                        .Height(100)
                                        .Table(table =>
                                        {
                                            table.ColumnsDefinition(columns =>
                                            {
                                                columns.RelativeColumn(1);
                                                columns.RelativeColumn(1);
                                            });
                                            
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(4).PaddingHorizontal(4))
                                                .Text("Dátum vyhotovenia:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(4).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text(invoiceDto.IssueDate.ToString("dd.MM.yyyy"));

                                            table.Cell().Border(1).Element(c => c.PaddingVertical(4).PaddingHorizontal(4))
                                                .Text("Dátum splatnosti:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(4).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text(invoiceDto.DueDate.ToString("dd.MM.yyyy"));

                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .Text("Variabilný symbol:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text("1");

                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .Text("Forma úhrady:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text("1");

                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .Text("Konštantný symbol:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text("1");
                                        });

                                });
                            });

                            column.Item().Column(column =>
                            {
                                column.Spacing(10);
                                
                                column.Item().Border(1).Element(c => c.PaddingVertical(10).PaddingHorizontal(10))
                                    .Text("Na základe Vašej objednávky Vám fakturujeme: ").Bold();
                            });

                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Border(1)
                                    .Padding(10)
                                    .Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(2);
                                            columns.RelativeColumn(12);
                                            columns.RelativeColumn(2.5f);
                                            columns.RelativeColumn(3);
                                            columns.RelativeColumn(3);
                                            columns.RelativeColumn(3);
                                        });
                                        
                                        table.Header(header =>
                                        {
                                            header.Cell().Border(1).AlignCenter().Element(c => c.Padding(2)).Text("P. č");
                                            header.Cell().Border(1).AlignCenter().Element(c => c.Padding(2)).Text("Popis");
                                            header.Cell().Border(1).AlignCenter().Element(c => c.Padding(2)).Text("MJ");
                                            header.Cell().Border(1).AlignCenter().Element(c => c.Padding(2)).Text("Cena za j.");
                                            header.Cell().Border(1).AlignCenter().Element(c => c.Padding(2)).Text("Množstvo");
                                            header.Cell().Border(1).AlignCenter().Element(c => c.Padding(2)).Text("Spolu");
                                        });
                                        
                                        int itemNumber = 1;

                                        foreach (var attendee in invoiceDto.Customer.Attendee) 
                                        {
                                            foreach (var attendeeItems in attendee.InvoiceItem) 
                                            {
                                                table.Cell().Border(1).Element(c => c.Padding(2))
                                                    .AlignCenter()
                                                    .Text(itemNumber++.ToString());
                                                table.Cell().Border(1).Element(c => c.Padding(2))
                                                    .Text($"{attendee.FirstName} {attendee.LastName} - {attendeeItems.Name}");
                                                table.Cell().Border(1).Element(c => c.Padding(2))
                                                    .AlignCenter()
                                                    .Text(attendeeItems.Unit);
                                                table.Cell().Border(1).Element(c => c.Padding(2))
                                                    .AlignRight()
                                                    .Text(attendeeItems.UnitPrice.ToString("F2", CultureInfo.InvariantCulture));
                                                table.Cell().Border(1).Element(c => c.Padding(2))
                                                    .AlignRight()
                                                    .Text(attendeeItems.Quantity.ToString("F2", CultureInfo.InvariantCulture));
                                                table.Cell().Border(1).Element(c => c.Padding(2))
                                                    .AlignRight()
                                                    .Text(attendeeItems.Price.ToString("F2", CultureInfo.InvariantCulture));
                                            }
                                        }
                                    });
                                
                            });

                            column.Item().Padding(10).Text($"Spolu = {invoiceDto.TotalPrice.ToString("F2", CultureInfo.InvariantCulture)} EUR").AlignRight().Bold();
                            
                        });
                    
                });
            });

            document.GeneratePdf(ms);
            await document.ShowInCompanionAsync();

            return ms.ToArray();
        }

        public async Task<byte[]> GenerateProgramPdf(int conferenceId)
        {
            var conferenceDto =  await _conferenceService.GetByIdAsync(conferenceId);

            if (conferenceDto == null)
            {
                throw new Exception($"Conference with id {conferenceId} not found.");
            }

            using var ms = new MemoryStream();

            var document = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1.5f, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Height(70)
                        .Padding(10)
                        .Column(column =>
                        {
                            column.Item().Text("Tu Bude Logo");
                        });

                    page.Content()
                        .Padding(10)
                        .Column(column =>
                        {
                            column.Spacing(12);

                            foreach (var day in conferenceDto.Day)
                            {
                                // Day block
                                column.Item().Border(1).Padding(8).Column(dayCol =>
                                {
                                    dayCol.Spacing(8);
                                    dayCol.Item().Text(day.Date.ToString("dddd, MMMM dd, yyyy")).FontSize(18).Bold();

                                    // Sessions for the day
                                    foreach (var session in day.Session)
                                    {
                                        dayCol.Item().Border(1).Padding(6).Column(sessionCol =>
                                        {
                                            sessionCol.Spacing(6);
                                            sessionCol.Item().Text((string)session.Title).FontSize(16).Bold();

                                            foreach (var theme in session.Theme)
                                            {
                                                sessionCol.Item().PaddingLeft(6).Column(themeCol =>
                                                {
                                                    themeCol.Spacing(4);

                                                    // theme header: time range + title + chair
                                                    themeCol.Item().Row(row =>
                                                    {
                                                        row.ConstantItem(90).Text($"{theme.StartTime:hh.mm} — {theme.EndTime:hh.mm}").FontSize(12).SemiBold();
                                                        row.RelativeItem().Column(c =>
                                                        {
                                                            c.Item().Text((string)theme.Title).FontSize(14).Bold();
                                                            if (!string.IsNullOrWhiteSpace((string)theme.Chair))
                                                            {
                                                                c.Item().Text($"Chair: {(string)theme.Chair}").FontSize(11).Italic().FontColor(Colors.Grey.Darken2);
                                                            }
                                                        });
                                                    });

                                                    // Talks under theme
                                                    foreach (var talk in theme.Talk)
                                                    {
                                                        themeCol.Item().PaddingLeft(12).Row(tRow =>
                                                        {
                                                            tRow.ConstantItem(70).Text($"{talk.StrartTime:hh.mm} — {talk.EndTime:hh.mm}").FontSize(11);
                                                            tRow.RelativeItem().Column(tc =>
                                                            {
                                                                tc.Item().Text((string)talk.Title).FontSize(12).SemiBold();
                                                                if (!string.IsNullOrWhiteSpace((string)talk.Content))
                                                                    tc.Item().Text((string)talk.Content).FontSize(11).FontColor(Colors.Grey.Darken1);
                                                            });
                                                        });
                                                    }
                                                });
                                            }
                                        });
                                    }
                                });
                            }

                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(t =>
                        {
                            t.CurrentPageNumber();
                        });
                });
            });


            document.GeneratePdf(ms);
            await document.ShowInCompanionAsync();

            return ms.ToArray();
        }
    }
}
