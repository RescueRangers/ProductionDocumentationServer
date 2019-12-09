using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data.Repositories
{
    public interface IProductionReportsRepository
    {
        Task<bool> Post(ProductionReport productionReport);
        Task<IEnumerable<ProductionReport>> Get();
        Task<ProductionReport> GetById(int id);
    }
}