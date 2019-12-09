using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

                var sql = @"INSERT INTO ProductionReports(Date) VALUES(@Date) SELECT CAST(SCOPE_IDENTITY() as int)";
                var picturesSql = @"INSERT INTO ReportPictures(SectionName, PictureUrl ,ReportId) VALUES(@SectionName, @PictureUrl, @ReportId)";

                var id = await db.QuerySingleAsync<int>(sql, new { productionReport.Date }).ConfigureAwait(false);

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
            var sql = @"SELECT A.[Id]
      ,A.[Date]
      ,A.[PicturesId]
	  ,B.TipPicture
	  ,B.RootPicture
	  ,B.SectionPicture
  FROM [dbo].[ProductionReports] A
  INNER JOIN ReportPictures B on A.[PicturesId] = B.Id
  WHERE A.Id = @Id";
            using (var db = Connection)
            {
                var r = await db.QueryFirstAsync<ProductionReport>(sql, new { id }).ConfigureAwait(false);
                return r;
            }
        }
    }
}
