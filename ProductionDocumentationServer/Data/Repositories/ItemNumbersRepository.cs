using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ProductionDocumentationServer.Data.Repositories
{
    public class ItemNumbersRepository : RepositoryBase, IItemNumbersRepository
    {
        public ItemNumbersRepository(IConfiguration config) : base(config)
        { }

        public async Task<List<string>> Get()
        {
            using (var db = Connection)
            {
                var itemNumbers = await db.QueryAsync<string>("SELECT ItemNumber FROM ItemNumbers").ConfigureAwait(false);

                return itemNumbers.ToList();
            }
        }

        public async Task Post(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName)) return;

            using (var db = Connection)
            {
                await db.ExecuteAsync("INSERT INTO ItemNumbers(ItemNumber) VALUES(@ItemNumber)", new { ItemNumber = itemName }).ConfigureAwait(false);
            }
        }
    }
}