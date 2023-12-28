using ChristopherChurch.Data.Models;
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
                        page.PageColor(Colors.White);
                        page.Margin(40);
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
            var titleStyle = TextStyle.Default.FontSize(35).Bold().FontColor(Colors.Black);

            container.Padding(40).Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Ministry Application").Style(titleStyle);
                });

                row.ConstantItem(80).Height(80).Image("Images/DarkCName.jpg");
            });
        }

        private void RenderFormAsPdf(IContainer container)
        {
            var bodyStyle = TextStyle.Default.FontSize(15).SemiBold().LineHeight(2).FontColor(Colors.Black);

            container.PaddingLeft(25).Column(column =>
            {
                try
                {
                    column.Item().Text($"First Name: {Application.FirstName}").Style(bodyStyle);
                    column.Item().Text($"Last Name: {Application.LastName}").Style(bodyStyle);
                    column.Item().Text($"Email: {Application.Email}").Style(bodyStyle);
                    column.Item().Text($"Address: {Application.Address}").Style(bodyStyle);
                    column.Item().Text($"Address 2: {Application.Address2}").Style(bodyStyle);
                    column.Item().Text($"City: {Application.City}").Style(bodyStyle);
                    column.Item().Text($"State: {Application.State}").Style(bodyStyle);
                    column.Item().Text($"Zip: {Application.Zip}").Style(bodyStyle);
                    column.Item().Text($"Authorization: {Application.Authorization}").Style(bodyStyle);
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
