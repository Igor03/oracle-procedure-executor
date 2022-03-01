using System;
using System.Data;
using System.Linq;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Input;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Output;
using JIgor.Projects.OracleProcedureExecutor.Services;
using JIgor.Projects.OracleProcedureExecutor.Services.Support;
using Oracle.ManagedDataAccess.Client;
using static System.Data.ParameterDirection;
using static JIgor.Projects.OracleProcedureExecutor.Services.Support.OracleProcedureExecutorHelper;


namespace JIgor.Projects.OracleProcedureExecutor.Samples.SampleProcedures
{
    public class UnifiedCalculation
    {
        private readonly OracleExecutor _oracleExecutor;
        private readonly InputHeader _header;
        
        public UnifiedCalculation(OracleExecutor oracleExecutor, InputHeader header)
        {
            _oracleExecutor = oracleExecutor;
            _header = header;
        }

        public OutputHeader Run()
        {
            var generalArraySize = this._header.Items.Count();
            var procedure = new ProcedureMetadata("JIGOR_PROJECTS_PKG.apply_unified_taxes", "JOSEIGOR");

            procedure.AddParameter(CreateOracleParameter("inHeaderId", OracleDbType.Decimal, Input, _header.Id));
            procedure.AddParameter(CreateOracleParameter("inTaxRate", OracleDbType.Decimal, Input, _header.TaxRate));
            procedure.AddParameter(CreateArrayOracleParameter("inItemNumber", Input, generalArraySize, value: _header.Items.Select(p => p.Number).ToArray()));
            procedure.AddParameter(CreateArrayOracleParameter("inItemUnitPrice", Input, generalArraySize, value: _header.Items.Select(p => p.UnitPrice).ToArray()));
            procedure.AddParameter(CreateArrayOracleParameter("inItemQuantity", Input, generalArraySize, value: _header.Items.Select(p => p.Quantity).ToArray()));
            procedure.AddParameter(CreateArrayOracleParameter("inItemDescription", Input, generalArraySize, value: _header.Items.Select(p => p.ItemDescription).ToArray()));
            procedure.AddParameter(CreateOracleParameter("outTotalAmount", OracleDbType.Decimal, Output));
            procedure.AddParameter(CreateArrayOracleParameter("outItemTaxAAA", Output, generalArraySize, value: Array.Empty<decimal>()));
            procedure.AddParameter(CreateArrayOracleParameter("outItemTaxBBB", Output, generalArraySize, value: Array.Empty<decimal>()));
            procedure.AddParameter(CreateArrayOracleParameter("outItemTaxCCC", Output, generalArraySize, value: Array.Empty<decimal>()));
            
            // Be careful with the calling order
            _ = _oracleExecutor.ExecuteStoredProcedure(procedure);
            
            foreach (var param in procedure.GetOutputParameters())
            {
                if (param.Value.GetType() == typeof(decimal[]))
                {
                    var x = (decimal[]) param.Value;
                    foreach (var value in x)
                    {
                        Console.WriteLine(value);
                    }
                }
                else
                {
                    Console.WriteLine(param.Value);
                }
            }

            return null;
        }
    }
}