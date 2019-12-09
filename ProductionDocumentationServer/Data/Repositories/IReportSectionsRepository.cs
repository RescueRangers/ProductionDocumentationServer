using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data.Repositories
{
    public interface IReportSectionsRepository
    {
        Task<ReportSections> Get();
        Task Post(IEnumerable<string> sections);
    }
}