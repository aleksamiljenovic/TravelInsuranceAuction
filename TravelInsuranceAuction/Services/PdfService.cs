using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TravelInsuranceAuction.ViewModels;

namespace TravelInsuranceAuction.Services
{
    public class PdfService
    {
        public byte[] GeneratePolicy(PaymentVM model)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("PUTNO OSIGURANJE — POLISA")
                            .FontSize(20).Bold().FontColor(Colors.Blue.Medium);
                        col.Item().Text($"Broj polise: POL-{model.OfferId:D6}")
                            .FontSize(11).FontColor(Colors.Grey.Medium);
                        col.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Blue.Medium);
                    });

                    page.Content().PaddingTop(20).Column(col =>
                    {
                        col.Item().Text("Podaci o putovanju").FontSize(14).Bold();
                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                                c.RelativeColumn();
                            });

                            void Row(string label, string value)
                            {
                                table.Cell().Padding(5).Background(Colors.Grey.Lighten3).Text(label).Bold();
                                table.Cell().Padding(5).Text(value);
                            }

                            Row("Destinacija", model.Destination);
                            Row("Datum polaska", model.StartDate?.ToString("dd.MM.yyyy") ?? "N/A");
                            Row("Datum povratka", model.EndDate?.ToString("dd.MM.yyyy") ?? "N/A");
                            Row("Agencija", model.AgencyName);
                            Row("Cena polise", $"{model.Price:F2} €");
                        });

                        col.Item().PaddingTop(30).Text("RAČUN").FontSize(14).Bold();
                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3);
                                c.RelativeColumn();
                            });

                            table.Cell().Padding(5).Background(Colors.Blue.Lighten3).Text("Opis").Bold();
                            table.Cell().Padding(5).Background(Colors.Blue.Lighten3).Text("Iznos").Bold();

                            table.Cell().Padding(5).Text($"Putno osiguranje — {model.Destination}");
                            table.Cell().Padding(5).Text($"{model.Price:F2} €");

                            table.Cell().Padding(5).Background(Colors.Grey.Lighten3).Text("Provizija platforme (10%)").Bold();
                            table.Cell().Padding(5).Background(Colors.Grey.Lighten3).Text($"{model.Price * 0.10:F2} €").Bold();

                            table.Cell().Padding(5).Background(Colors.Blue.Lighten2).Text("UKUPNO").Bold();
                            table.Cell().Padding(5).Background(Colors.Blue.Lighten2).Text($"{model.Price:F2} €").Bold();
                        });

                        col.Item().PaddingTop(40).Text($"Datum izdavanja: {DateTime.Now:dd.MM.yyyy}")
                            .FontColor(Colors.Grey.Medium);
                    });

                    page.Footer().AlignCenter()
                        .Text("TravelInsuranceAuction © 2026")
                        .FontSize(10).FontColor(Colors.Grey.Medium);
                });
            }).GeneratePdf();
        }
    }
}
