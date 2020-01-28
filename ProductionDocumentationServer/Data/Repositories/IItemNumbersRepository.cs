using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data.Repositories
{
    public interface IItemNumbersRepository
    {
        Task<List<string>> Get();

        Task Post(string itemName);
    }
}