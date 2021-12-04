using System;
using System.Data;
using JIgor.Projects.OracleProcedureExecutor.Services;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.SampleProcedures
{
    public class GetNextVal
    {
        private readonly OracleExecutor _oracleExecutor;

        public GetNextVal(OracleExecutor oracleExecutor)
        {
            _oracleExecutor = oracleExecutor;
        }

        private const string ProcedureName = "JIGOR_PROJECTS_PKG.get_next_value";

        public OracleParameter NextValue = new OracleParameter()
        {
            ParameterName = "outNextValue",
            OracleDbType = OracleDbType.Decimal,
            Direction = ParameterDirection.Output
        };

        public void Run()
        {
            _ = _oracleExecutor.ExecuteStoredProcedure(ProcedureName, this.NextValue);
            Console.WriteLine(NextValue.Value);
        }
    }
}
