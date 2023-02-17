using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ProductionDocumentationServer.Data
{
    public class RepositoryBase
    {
        private readonly IConfiguration _config;

        internal IDbConnection Connection
        {
            get
            {
                var cs = _config.GetConnectionString("DefaultConnection");
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public RepositoryBase(IConfiguration config)
        {
            _config = config;
        }
    }
}