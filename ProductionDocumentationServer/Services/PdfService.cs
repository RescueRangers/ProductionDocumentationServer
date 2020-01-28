using System.IO;
using System.Threading.Tasks;
using jsreport.AspNetCore;
using ProductionDocumentationServer.Data;

namespace ProductionDocumentationServer.Services
{
    public class PdfService : IPdfService
    {
        private readonly IJsReportMVCService _reportingService;

        public PdfService(IJsReportMVCService reportingService)
        {
            _reportingService = reportingService;
        }

        public async Task<byte[]> GeneratePdfReport(ProductionReport productionReport)
        {
            var pdf = await _reportingService.RenderByNameAsync("ProductionReport", productionReport).ConfigureAwait(false);

            using (var ms = new MemoryStream())
            {
                await pdf.Content.CopyToAsync(ms).ConfigureAwait(false);
                return ms.ToArray();
            }
        }
    }
}