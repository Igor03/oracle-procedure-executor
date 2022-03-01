using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.support
{
    public class ProcedureMetadata
    {
        public ProcedureMetadata()
        {
        }
        public string Name { get; set; } 
        
        public string Schema { get; set; }
        
        public List<OracleParameter> InputParameters { get; set; }
        
        public List<OracleParameter> OutputParameters { get; set; }

        public string BuildProcedureCall()
        {
            var call = string.Empty;
            var parameters = new List<OracleParameter>();
            
            parameters.AddRange(InputParameters);
            parameters.AddRange(OutputParameters);

            parameters.ForEach(p => call += $":{p.ParameterName},");
              
            return $"BEGIN {this.Schema}" + $".{this.Name}({call.Remove(call.Length - 1, 1)}); END;";
        }
    }
}