// Add a reference to the PdfSharp nuget package
// Install-Package PdfSharp

using ChristopherChurch.Data.Models;

namespace ChristopherChurch.Data.Services
{
    public interface IMinistryFormService
    {
        byte[] GeneratePdf(MinistryApplicationModel application);
        Task SubmitApplicationForm(MinistryApplicationModel application);
    }
}