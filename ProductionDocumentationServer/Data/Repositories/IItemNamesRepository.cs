using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data.Repositories
{
    public interface IItemNamesRepository
    {
        Task<List<string>> Get();
        Task Post(string itemNumber);
    }
}