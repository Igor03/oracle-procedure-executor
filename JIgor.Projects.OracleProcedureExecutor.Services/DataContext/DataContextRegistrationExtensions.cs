using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JIgor.Projects.OracleProcedureExecutor.Services.DataContext
{
    internal static class DataContextRegistrationExtensions
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<OracleProcedureExecutorDataContext>(options =>
            {
                options.UseOracle(configuration.GetConnectionString("Oracle"));
            });
        }
    }
}
