using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ProductionDocumentationServer.Data.Repositories
{
    public class ProductionReportsRepository : RepositoryBase, IProductionReportsRepository
    {
        public ProductionReportsRepository(IConfiguration config) : base(config)
        { }

        public async Task<bool> Post(ProductionReport productionReport)
        {
            if (productionReport?.ReportPictures.Count == 0) return false;

            using (var db = Connection)
            {
                const string sql = @"INSERT INTO ProductionReports(Date, ItemName, ItemNumber, OrderId, TimeCode) VALUES(@Date, @ItemName, @ItemNumber, @OrderId, @TimeCode) SELECT CAST(SCOPE_IDENTITY() as int)";
                const string picturesSql = @"INSERT INTO ReportPictures(SectionName, PictureUrl ,ReportId) VALUES(@SectionName, @PictureUrl, @ReportId)";

                var id = await db.QuerySingleAsync<int>(sql, new { productionReport.Date, ItemName = productionReport.ItemName, ItemNumber = productionReport.ItemNumber, OrderId = productionReport.OrderId, TimeCode = productionReport.TimeCode }).ConfigureAwait(false);

                var results = new List<int>();

                foreach (var item in productionReport.ReportPictures)
                {
                    var result = await db.ExecuteAsync(picturesSql, new { SectionName = item.SectionName, PictureUrl = item.PictureUrl, ReportId = id }).ConfigureAwait(false);
                    results.Add(result);
                }

                return results.All(x => x == 1);
            }
        }

        public async Task<IEnumerable<ProductionReport>> Get()
        {
            using (var db = Connection)
            {
                return await db.QueryAsync<ProductionReport>("SELECT * From ProductionReports").ConfigureAwait(false);
            }
        }

        public async Task<ProductionReport> GetById(int id)
        {
            const string sql = @"
SELECT
       [Id]
      ,[Date]
      ,[ItemName]
      ,[ItemNumber]
      ,[TimeCode]
      ,[OrderId]
  FROM [dbo].[ProductionReports]
  WHERE Id = @Id";

            const string picturesSql = @"
SELECT [Id]
      ,[SectionName]
      ,[PictureUrl]
      ,[ReportId]
  FROM [dbo].[ReportPictures]
  WHERE ReportId = @Id";

            using (var db = Connection)
            {
                var r = await db.QueryFirstAsync<ProductionReport>(sql, new { id }).ConfigureAwait(false);
                var pictures = await db.QueryAsync<ReportPicture>(picturesSql, new { id }).ConfigureAwait(false);
                r.OrderNumber = await db.QueryFirstAsync<string>("SELECT [OrderNumber] FROM [dbo].[Orders] WHERE Id = @Id", new { Id = r.OrderId }).ConfigureAwait(false);

                r.ReportPictures = pictures.ToList();

                return r;
            }
        }

        public async Task<IEnumerable<ProductionReport>> GetByOrder(int orderId)
        {
            const string sql = @"
SELECT
       [Id]
      ,[Date]
      ,[TimeCode]
      ,[ItemName]
      ,[ItemNumber]
  FROM [dbo].[ProductionReports]
  WHERE OrderId = @OrderId";

            using (var db = Connection)
            {
                var r = await db.QueryAsync<ProductionReport>(sql, new { orderId }).ConfigureAwait(false);

                return r.ToList();
            }
        }

        public async Task UpdateReport(ProductionReport report)
        {
            var sql = $@"
UPDATE ProductionReports SET
    {nameof(ProductionReport.Date)} = @{nameof(ProductionReport.Date)},
    {nameof(ProductionReport.ItemName)} = @{nameof(ProductionReport.ItemName)},
    {nameof(ProductionReport.ItemNumber)} = @{nameof(ProductionReport.ItemNumber)},
    {nameof(ProductionReport.TimeCode)} = @{nameof(ProductionReport.TimeCode)}
WHERE {nameof(ProductionReport.Id)} = @{nameof(ProductionReport.Id)}";
            using (var db = Connection)
            {
                await db.ExecuteAsync(sql, new{ Date = report.Date, ItemName = report.ItemName, ItemNumber = report.ItemNumber, TimeCode = report.TimeCode, Id = report.Id}).ConfigureAwait(false);
            }
        }
    }
}