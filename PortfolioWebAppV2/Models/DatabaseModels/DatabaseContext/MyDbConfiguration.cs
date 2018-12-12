using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace PortfolioWebAppV2.Models.DatabaseModels.DatabaseContext
{
    public class MyDbConfiguration : DbConfiguration
    {
        public MyDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());

        }
    }
}