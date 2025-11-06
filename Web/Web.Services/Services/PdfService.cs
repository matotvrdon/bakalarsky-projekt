using System.Diagnostics;
using System.Globalization;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Web.Services.Abstractions;
using Web.Services.DTOs.Invoice;

namespace Web.Services.Services
{
    public class PdfService : IPdfService
    {
        private readonly IHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ISupplierService _supplierService;
        private readonly ICustomerService _customerService;

        public PdfService(IHostEnvironment env, IMapper mapper, ISupplierService supplierService,
            ICustomerService customerService)
        {
            _env = env;
            _mapper = mapper;
            _supplierService = supplierService;
            _customerService = customerService;
        }

        public async Task<byte[]> GeneratePdf(CreateInvoiceDto createInvoiceDto)
        {
            ArgumentNullException.ThrowIfNull(createInvoiceDto);

            var invoiceDto = _mapper.Map<InvoiceDto>(createInvoiceDto);

            // invoiceDto.SupplierDto = await _supplierService.GetByIdAsync(createInvoiceDto.SupplierDtoId)
            //     ?? throw new InvalidOperationException($"Supplier with ID {createInvoiceDto.SupplierDtoId} not found.");
            // invoiceDto.CustomerDto = await _customerService.GetByIdAsync(createInvoiceDto.CustomerDtoId)
            //     ?? throw new InvalidOperationException($"Customer with ID {createInvoiceDto.CustomerDtoId} not found.");
            invoiceDto.SupplierDto = await _supplierService.GetByIdAsync(1) ??
                                     throw new InvalidOperationException(
                                         $"Supplier with ID {createInvoiceDto.SupplierDtoId} not found.");

            invoiceDto.CustomerDto = await _customerService.GetByIdAsync(1) ??
                                     throw new InvalidOperationException(
                                         $"Customer with ID {createInvoiceDto.CustomerDtoId} not found.");

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
                        .Text("Invoice: 10/2025").FontSize(20);


                    page.Content()
                        .Border(1)
                        .Column(column =>
                        {
                            column.Spacing(10);

                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Border(1)
                                    .Height(300)
                                    .PaddingLeft(10)
                                    .PaddingTop(10)
                                    .Column(column =>
                                    {
                                        column.Spacing(5);

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Dodávateľ: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Name);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Ulica: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Street);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Mesto: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.PostalCode);
                                            t.Span(" ");
                                            t.Span(invoiceDto.SupplierDto.City);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Štát: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Country);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("IČO: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Ico);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("DIČ: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Dic);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("IČDPH: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.IcDph);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Banka: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Bank);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Adresa: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Address);
                                            t.Span("\n");
                                            t.Span(invoiceDto.SupplierDto.AddressPostalCode);
                                            t.Span(" ");
                                            t.Span(invoiceDto.SupplierDto.AddressCity);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Adresa: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Bank);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Číslo účtu: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.BankAccount);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("SWIFT: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Swift);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("IBAN: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Iban);
                                        });

                                        column.Item().Text(t =>
                                        {
                                            t.Span("Telefon: ").Bold();
                                            t.Span(invoiceDto.SupplierDto.Phone);
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
                                            column.Spacing(10);

                                            column.Item().Text(t =>
                                            {
                                                t.Span("Odberateľ: ").Bold();
                                                t.Span(invoiceDto.CustomerDto.Name);
                                            });

                                            column.Item().Text(t =>
                                            {
                                                t.Span("Ulica: ").Bold();
                                                t.Span(invoiceDto.CustomerDto.Street);
                                            });
                                        });
                                    
                                    column.Item()
                                        .Border(1)
                                        .Height(100)
                                        .Table(table =>
                                        {
                                            table.ColumnsDefinition(columns =>
                                            {
                                                columns.RelativeColumn(1); // labels
                                                columns.RelativeColumn(1); // values
                                            });

                                            // row 1
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(4).PaddingHorizontal(4))
                                                .Text("Dátum vyhotovenia:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(4).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text(invoiceDto.IssueDate.ToString("dd.MM.yyyy"));

                                            // row 2
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(4).PaddingHorizontal(4))
                                                .Text("Dátum splatnosti:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(4).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text(invoiceDto.DueDate.ToString("dd.MM.yyyy"));

                                            // row 3
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .Text("Variabilný symbol:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text("1");

                                            // row 4
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .Text("Forma úhrady:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text("1");

                                            // row 5
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .Text("Konštantný symbol:");
                                            table.Cell().Border(1).Element(c => c.PaddingVertical(3).PaddingHorizontal(4))
                                                .AlignRight()
                                                .Text("1");
                                        });

                                });
                            });
                        });

                    page.Footer()
                        .Border(1)
                        .Background(Colors.Grey.Lighten1)
                        .Height(75)
                        .AlignCenter()
                        .AlignMiddle()
                        .Text("Footer");
                });
            });

            document.GeneratePdf(ms);
            await document.ShowInCompanionAsync();

            return ms.ToArray();
        }

        private static Task OpenPdfInCompanionAsync(byte[] pdfBytes, string fileName)
        {
            return Task.Run(() =>
            {
                try {
                    var path = Path.Combine(Path.GetTempPath(), fileName);
                    File.WriteAllBytes(path, pdfBytes);

                    var psi = new ProcessStartInfo {
                        FileName = "open",
                        Arguments = $"-a \"Quest PDF Companion\" \"{path}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    Process.Start(psi);
                }
                catch {
                    // ignore if app is not installed or launch fails
                }
            });
        }
    }
}