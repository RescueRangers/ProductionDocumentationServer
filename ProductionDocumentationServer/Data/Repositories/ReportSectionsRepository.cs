using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ProductionDocumentationServer.Data.Repositories
{
    public class ReportSectionsRepository : RepositoryBase, IReportSectionsRepository
    {
        public ReportSectionsRepository(IConfiguration config) : base(config)
        { }

        public async Task<ReportSections> Get()
        {
            using (var db = Connection)
            {
                var sections = await db.QueryAsync<string>("SELECT SectionName FROM ReportSections").ConfigureAwait(false);

                return new ReportSections { Sections = sections.ToList() };
            }
        }

        public async Task Post(IEnumerable<string> sections)
        {
            if (sections == null) return;

            using (var db = Connection)
            {
                foreach (var item in sections)
                {
                    await db.ExecuteAsync("INSERT INTO ReportSections(SectionName) VALUES(@SectionName)", new { SectionName = item }).ConfigureAwait(false);
                }
            }
        }
    }
}