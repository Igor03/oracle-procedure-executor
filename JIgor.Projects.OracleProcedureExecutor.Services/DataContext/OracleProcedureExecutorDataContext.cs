using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JIgor.Projects.OracleProcedureExecutor.Services.DataContext
{
    public class OracleProcedureExecutorDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public OracleProcedureExecutorDataContext(DbContextOptions<OracleProcedureExecutorDataContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            _ = modelBuilder.HasDefaultSchema(_configuration.GetSection("OracleDatabaseInformation:DefaultSchema").Value)
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        
    }
}
