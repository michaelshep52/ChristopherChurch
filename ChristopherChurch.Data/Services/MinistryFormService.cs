using ChristopherChurch.Data.Models;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ChristopherChurch.Data.Services
{
    public class MinistryFormService : IMinistryFormService
    {
        private readonly IEmailService _emailService;
        public MinistryApplicationModel Application { get; private set; }

        public MinistryFormService(IEmailService emailService)
        {
            _emailService = emailService;
            Application = new MinistryApplicationModel(); 

        }

        public async Task SubmitApplicationForm(MinistryApplicationModel application)
        {
            byte[] pdfBytes = GeneratePdf(application);

            await _emailService.SendEmailWithAttachment(pdfBytes);
        }

        public byte[] GeneratePdf(MinistryApplicationModel application)
        {
            try
            {
                Application = application;

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(50);

                        page.Header().Element(ComposeHeader);
                        page.Content().Element(RenderFormAsPdf);

                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                    });
                });

                using (MemoryStream stream = new MemoryStream())
                {
                    document.GeneratePdf(stream);
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating PDF: {ex.Message}");
                throw;
            }
        }
        private void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(40).SemiBold().FontColor(Colors.Black);

            container.Row(row =>
            {
               /* row.RelativeItem()
                       .Width(100).Padding(10).AlignLeft()
                      .Image("https://raw.githubusercontent.com/michaelshep52/CCOGPictures/main/DarkLogo.jpg");
               */
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Ministry Application Form").Style(titleStyle);
                });

                row.ConstantItem(100).Height(50).Placeholder();
            });
        }

        private void RenderFormAsPdf(IContainer container)
        {
            container.Column(column =>
            {
                try
                {
                    column.Item().Text($"First Name: {Application.FirstName}").FontSize(12);
                    column.Item().Text($"Last Name: {Application.LastName}").FontSize(12);
                    column.Item().Text($"Email: {Application.Email}").FontSize(12);
                    column.Item().Text($"Address: {Application.Address}").FontSize(12);
                    column.Item().Text($"Address 2: {Application.Address2}").FontSize(12);
                    column.Item().Text($"City: {Application.City}").FontSize(12);
                    column.Item().Text($"State: {Application.State}").FontSize(12);
                    column.Item().Text($"Zip: {Application.Zip}").FontSize(12);
                    column.Item().Text($"Authorization: {Application.Authorization}").FontSize(12);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating PDF: {ex.Message}");
                    throw;
                }
            });
        }
    }
}
