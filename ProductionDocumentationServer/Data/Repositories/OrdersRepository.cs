using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ProductionDocumentationServer.Data.Repositories
{
    public class OrdersRepository : RepositoryBase, IOrdersRepository
    {
        public OrdersRepository(IConfiguration config) : base(config)
        { }

        public async Task<IEnumerable<Order>> Get()
        {
            using (var db = Connection)
            {
                var r = await db.QueryAsync<Order>("SELECT * From Orders").ConfigureAwait(false);

                return r.ToList();
            }
        }

        public async Task<Order> GetbyId(int id)
        {
            using (var db = Connection)
            {
                return await db.QueryFirstAsync<Order>("SELECT * FROM Orders WHERE Id = @Id", new { id }).ConfigureAwait(false);
            }
        }

        public async Task<int> GetOrderId(string orderNumber)
        {
            var query = @"
SELECT Id
FROM Orders
WHERE OrderNumber = @OrderNumber;";
            const string insertQuery = @"INSERT INTO Orders(OrderNumber) VALUES(@OrderNumber) SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var db = Connection)
            {
                var id = await db.QueryFirstOrDefaultAsync<int>(query, new { orderNumber }).ConfigureAwait(false);

                if (id == 0)
                {
                    id = await db.QueryFirstOrDefaultAsync<int>(insertQuery, new { orderNumber }).ConfigureAwait(false);
                }

                return id;
            }
        }

        public async Task Post(Order order)
        {
            using (var db = Connection)
            {
                await db.ExecuteAsync("INSERT INTO Orders(OrderNumber) VALUES(@OrderNumber)", new { order.OrderNumber }).ConfigureAwait(false);
            }
        }

        public async Task<string> GetOrderNumber(int orderId)
        {
            using (var db = Connection)
            {
                return await db.QueryFirstAsync<string>("SELECT OrderNumber FROM Orders WHERE Id = @Id", new { Id = orderId }).ConfigureAwait(false);
            }
        }
    }
}