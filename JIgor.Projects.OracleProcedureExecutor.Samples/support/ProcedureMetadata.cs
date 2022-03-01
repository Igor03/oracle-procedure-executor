using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.support
{
    public class ProcedureMetadata
    {
        public string Name { get; set; }
        
        public string Schema { get; set; }

        public List<OracleParameter> Parameters { get; set; }

        public OracleParameter[] GetOutputParameters()
        {
            var outputParameters = new List<OracleParameter>();

            this.Parameters.ForEach(p =>
            {
                if (p.Direction == ParameterDirection.Output)
                {
                    outputParameters.Add(p);
                }
            });
            
            return outputParameters.ToArray();
        }

        public string ToSqlStatement()
        {
            var sqlStatement = string.Empty;
            this.Parameters.ForEach(p => sqlStatement += $":{p.ParameterName},");
            return $"BEGIN {this.Schema}" + $".{this.Name}({sqlStatement.Remove(sqlStatement.Length - 1, 1)}); END;";
        }
    }
}