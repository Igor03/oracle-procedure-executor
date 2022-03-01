using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Input;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Output;
using JIgor.Projects.OracleProcedureExecutor.Samples.support;
using JIgor.Projects.OracleProcedureExecutor.Services;
using Oracle.ManagedDataAccess.Client;
using static System.Data.ParameterDirection;
using static Oracle.ManagedDataAccess.Client.OracleCollectionType;
using static JIgor.Projects.OracleProcedureExecutor.Samples.support.OracleProcedureExecutorHelper;


namespace JIgor.Projects.OracleProcedureExecutor.Samples.SampleProcedures
{
    public class UnifiedCalculation
    {
        private readonly OracleExecutor _oracleExecutor;
        private readonly InputHeader _header;
        private const string ProcedureName = "JIGOR_PROJECTS_PKG.apply_unified_taxes";

        public UnifiedCalculation(OracleExecutor oracleExecutor, InputHeader header)
        {
            _oracleExecutor = oracleExecutor;
            _header = header;
        }

        public OutputHeader Run()
        {
            var generalArraySize = this._header.Items.Count();

            var inHeaderId = CreateOracleParameter("inHeaderId", OracleDbType.Decimal, Input, _header.Id);
            var inTaxRate = CreateOracleParameter("inTaxRate", OracleDbType.Decimal, Input, _header.TaxRate);
            var inItemNumber = CreateArrayOracleParameter("inItemNumber", Input, PLSQLAssociativeArray, generalArraySize, value: _header.Items.Select(p => p.Number).ToArray());
            var inItemUnitPrice = CreateArrayOracleParameter("inItemUnitPrice", Input, PLSQLAssociativeArray, generalArraySize, value: _header.Items.Select(p => p.UnitPrice).ToArray());
            var inItemQuantity = CreateArrayOracleParameter("inItemQuantity", Input, PLSQLAssociativeArray, generalArraySize, value: _header.Items.Select(p => p.Quantity).ToArray());
            var inItemDescription = CreateArrayOracleParameter("inItemDescription", Input, PLSQLAssociativeArray, generalArraySize, value: _header.Items.Select(p => p.ItemDescription).ToArray());
            var outTotalAmount = CreateOracleParameter("outTotalAmount", OracleDbType.Decimal, Output);
            var outItemTaxAAA = CreateArrayOracleParameter("outItemTaxAAA", Output, PLSQLAssociativeArray, generalArraySize, value: Array.Empty<decimal>());
            var outItemTaxBBB = CreateArrayOracleParameter("outItemTaxBBB", Output, PLSQLAssociativeArray, generalArraySize, value: Array.Empty<decimal>());
            var outItemTaxCCC = CreateArrayOracleParameter("outItemTaxCCC", Output, PLSQLAssociativeArray, generalArraySize, value: Array.Empty<decimal>());

            // Be careful with the calling order
            _ = _oracleExecutor.ExecuteStoredProcedure(ProcedureName,
                inHeaderId,
                inTaxRate, inItemNumber, inItemUnitPrice, inItemQuantity, inItemDescription, outTotalAmount,
                outItemTaxAAA, outItemTaxBBB, outItemTaxCCC);

            // That's important
            var x = (decimal[]) outItemTaxAAA.Value;

            Console.WriteLine(x[0]);
            Console.WriteLine(x[1]);
            Console.WriteLine(x[2]);
            Console.WriteLine(outTotalAmount.Value);

            return null;
        }
    }
}