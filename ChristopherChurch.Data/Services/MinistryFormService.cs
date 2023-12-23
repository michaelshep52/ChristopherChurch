using ChristopherChurch.Data.Models;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace ChristopherChurch.Data.Services
{
    public class MinistryFormService : IMinistryFormService
    {
        private readonly IEmailService _emailService;

        public MinistryFormService(IEmailService emailService)
        {
            _emailService = emailService;
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
                using (MemoryStream stream = new MemoryStream())
                {
                    // Create a new PDF document
                    PdfDocument pdf = new PdfDocument();

                    // Add a page to the document
                    PdfPage page = pdf.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Set font and brush for drawing
                    XFont font = new XFont("Verdana", 12, XFontStyleEx.Bold);
                    XBrush brush = XBrushes.Black;

                    // Draw the content on the PDF
                    gfx.DrawString("Ministry Application Form", font, brush, 20, 20);
                    RenderFormAsPdf(gfx, application);

                    // Save the PDF to the memory stream
                    pdf.Save(stream, false);
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating PDF: {ex.Message}");
                throw;
            }
        }

        private void RenderFormAsPdf(XGraphics gfx, MinistryApplicationModel application)
        {
            XFont font = new XFont("Arial", 10);
            float yPosition = 40;

            RenderField(gfx, font, "First Name:", application.FirstName!, ref yPosition);
            RenderField(gfx, font, "Last Name:", application.LastName!, ref yPosition);
            RenderField(gfx, font, "Email:", application.Email!, ref yPosition);
            RenderField(gfx, font, "Address:", application.Address!, ref yPosition);
            RenderField(gfx, font, "Address 2:", application.Address2!, ref yPosition);
            RenderField(gfx, font, "City:", application.City!, ref yPosition);
            RenderField(gfx, font, "State:", application.State!, ref yPosition);
            RenderField(gfx, font, "Zip:", application.Zip!, ref yPosition);
            RenderField(gfx, font, "Authorization:", application.Authorization.ToString(), ref yPosition);
        }

        private void RenderField(XGraphics gfx, XFont font, string label, string value, ref float yPosition)
        {
            gfx.DrawString($"{label} {value}", font, XBrushes.Black, 20, yPosition);
            yPosition += 20;
        }
    }
}
