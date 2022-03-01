using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Services.Support
{
    public class ProcedureMetadata
    {
        public ProcedureMetadata(string name, string schema)
        {
            this.Name = name;
            this.Schema = schema;
            this.Parameters = new List<OracleParameter>();
        }
        
        public string Name { get; set; }
        
        public string Schema { get; set; }

        public List<OracleParameter> Parameters { get; set; }

        public List<OracleParameter> GetOutputParameters()
        {
            var outputParameters = new List<OracleParameter>();

            this.Parameters.ForEach(p =>
            {
                if (p.Direction == ParameterDirection.Output)
                {
                    outputParameters.Add(p);
                }
            });
            
            return outputParameters;
        }

        public OracleParameter GetOutputParameterByName(string name)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            var outputParameters = this.GetOutputParameters();
            return outputParameters.FirstOrDefault(p => p.ParameterName == name);
        }

        public void AddParameter(OracleParameter parameter)
        {
            _ = parameter ?? throw new ArgumentNullException(nameof(parameter));
            this.Parameters.Add(parameter);
        }

        public string ToSqlStatement()
        {
            var sqlStatement = string.Empty;
            this.Parameters.ForEach(p => sqlStatement += $":{p.ParameterName},");
            return $"BEGIN {this.Schema}" + $".{this.Name}({sqlStatement.Remove(sqlStatement.Length - 1, 1)}); END;";
        }
    }
}