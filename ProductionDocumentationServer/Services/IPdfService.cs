using System.Threading.Tasks;
using ProductionDocumentationServer.Data;

namespace ProductionDocumentationServer.Services
{
    public interface IPdfService
    {
        Task<byte[]> GeneratePdfReport(ProductionReport productionReport);
    }
}