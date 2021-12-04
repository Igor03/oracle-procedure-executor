using System;
using System.Collections.Generic;
using System.Linq;
using JIgor.Projects.OracleProcedureExecutor.Services.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Services
{
    public class OracleExecutor
    {
        private readonly IConfiguration _configuration;
        private readonly OracleProcedureExecutorDataContext _oracleProcedureExecutorDataContext;

        public OracleExecutor(IConfiguration configuration, 
            OracleProcedureExecutorDataContext oracleProcedureExecutorDataContext)
        {
            _configuration = configuration;
            _oracleProcedureExecutorDataContext = oracleProcedureExecutorDataContext;
        }

        public int ExecuteStoredProcedure(string procedureName,
            params OracleParameter[] oracleParameters)
        {
            _ = procedureName ?? throw new ArgumentNullException(nameof(procedureName));
            _ = oracleParameters ?? throw new ArgumentNullException(nameof(oracleParameters));

            return this._oracleProcedureExecutorDataContext
                .Database.ExecuteSqlRaw(BuildProcedureCall(procedureName, oracleParameters), oracleParameters.ToList());
        }

        private string BuildProcedureCall(string procedureName, IEnumerable<OracleParameter> oracleParameters)
        {
            _ = procedureName ?? throw new ArgumentNullException(nameof(procedureName));
            _ = oracleParameters ?? throw new ArgumentNullException(nameof(oracleParameters));

            var procedureCallParameters = string.Empty;
            var databaseSchema = _configuration.GetSection("OracleDatabaseInformation:DefaultSchema").Value;

            oracleParameters.ToList()
                .ForEach(p => procedureCallParameters += $":{p.ParameterName},");

            var stringProcedureCall = $"BEGIN {databaseSchema}" + 
                                      $".{procedureName}({procedureCallParameters.Remove(procedureCallParameters.Length - 1, 1)}); END;";

            return stringProcedureCall;

        }
    }
}
