using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionDocumentationServer.Data;

namespace ProductionDocumentationServer.Services
{
    public interface IReportsService
    {
        Task<List<string>> GetItemNames();
        Task<List<string>> GetItemNumbers();
        Task<int> GetOrderId(string orderNumber);
        Task<List<Order>> GetOrders();
        Task<ReportSections> GetReportSections();
        Task PostItemName(string itemName);
        Task PostItemNumber(string itemNumber);
        Task<bool> PostReport(ProductionReport productionReport);
        Task PostSections(List<string> sections);
    }
}