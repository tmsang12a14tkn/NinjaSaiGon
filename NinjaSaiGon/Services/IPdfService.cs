using System.Collections.Generic;

namespace NinjaSaiGon.Admin.Services
{
    public interface IPdfService
    {
        byte[] GeneratePdfFromHtml(List<string> htmlBody);
    }
}
