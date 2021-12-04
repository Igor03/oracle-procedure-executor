using System;
using System.IO;
using JIgor.Projects.OracleProcedureExecutor.Services.DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JIgor.Projects.OracleProcedureExecutor.Services
{
    public static class DependencyResolver
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            const string machineUser = "jigor";
            const string secretsJsonId = "ec9b0bc5-9c68-40a5-a210-26514e5ed64b";
            var secretsJson =
                $"C:/Users/{machineUser}/AppData/Roaming/Microsoft/UserSecrets/{secretsJsonId}/secrets.json"; 


            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile(secretsJson, false)
                .Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IConfiguration>(configuration)
                        .AddDataContext(configuration)
                        .AddTransient<OracleExecutor>();

                });
        }
    }
}
