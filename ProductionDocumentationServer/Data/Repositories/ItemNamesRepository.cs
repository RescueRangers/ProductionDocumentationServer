using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ProductionDocumentationServer.Data.Repositories
{
    public class ItemNamesRepository : RepositoryBase, IItemNamesRepository
    {
        public ItemNamesRepository(IConfiguration config) : base(config)
        { }

        public async Task<List<string>> Get()
        {
            using (var db = Connection)
            {
                var itemNumbers = await db.QueryAsync<string>("SELECT ItemName FROM ItemNames").ConfigureAwait(false);

                return itemNumbers.ToList();
            }
        }

        public async Task Post(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName)) return;

            using (var db = Connection)
            {
                await db.ExecuteAsync("INSERT INTO ItemNames(ItemName) VALUES(@ItemName)", new { ItemName = itemName }).ConfigureAwait(false);
            }
        }
    }
}