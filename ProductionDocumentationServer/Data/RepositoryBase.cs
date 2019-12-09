using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionDocumentationServer.Data
{
    public class RepositoryBase
    {
        private readonly IConfiguration _config;

        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public RepositoryBase(IConfiguration config)
        {
            _config = config;
        }
    }
}
