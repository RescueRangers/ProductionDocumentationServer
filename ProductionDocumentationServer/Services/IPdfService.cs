using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductionDocumentationServer.Data;

namespace ProductionDocumentationServer.Services
{
    public interface IPdfService
    {
        Task<byte[]> GeneratePdfReport(ProductionReport productionReport);
    }
}