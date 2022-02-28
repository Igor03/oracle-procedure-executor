using System.Collections.Generic;
using System.Data;
using System.Linq;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Input;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Output;
using JIgor.Projects.OracleProcedureExecutor.Services;
using Oracle.ManagedDataAccess.Client;

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
            #region Input Parameters

            var inHeaderId = new OracleParameter()
            {
                ParameterName = "inHeaderId",
                OracleDbType = OracleDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = _header.Id,
            };
            
            var inTaxRate = new OracleParameter()
            {
                ParameterName = "inTaxRate",
                OracleDbType = OracleDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = _header.TaxRate,
            };
            
            var inItemNumber = new OracleParameter
            {
                ParameterName = "inItemNumber",
                Direction = ParameterDirection.Input,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.Number).ToArray().Length,
                Value = _header.Items.Select(p => p.Number).ToArray(),
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Number).ToArray().Length, 16), 
            };
            
            var inItemUnitPrice = new OracleParameter
            {
                ParameterName = "inItemUnitPrice",
                Direction = ParameterDirection.Input,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.UnitPrice).ToArray().Length,
                Value = _header.Items.Select(p => p.UnitPrice).ToArray(),
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.UnitPrice).ToArray().Length, 16), 
            };
            
            var inItemQuantity = new OracleParameter
            {
                ParameterName = "inItemQuantity",
                Direction = ParameterDirection.Input,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.Quantity).ToArray().Length,
                Value = _header.Items.Select(p => p.Quantity).ToArray(),
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Quantity).ToArray().Length, 16), 
            };
            
            var inItemDescription = new OracleParameter
            {
                ParameterName = "inItemDescription",
                Direction = ParameterDirection.Input,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.ItemDescription).ToArray().Length,
                Value = _header.Items.Select(p => p.ItemDescription).ToArray(),
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.ItemDescription).ToArray().Length, 2000), 
            };

            #endregion

            var outTotalAmount = new OracleParameter()
            {
                ParameterName = "outTotalAmount",
                OracleDbType = OracleDbType.Decimal,
                Direction = ParameterDirection.Output,
            };
            
            var outItemTaxAAA = new OracleParameter
            {
                ParameterName = "outItemTaxAAA",
                Direction = ParameterDirection.Output,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.Number).ToArray().Length,
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Number).ToArray().Length, 16), 
                OracleDbType = OracleDbType.Decimal,
            };
            
            var outItemTaxBBB = new OracleParameter
            {
                ParameterName = "outItemTaxBBB",
                Direction = ParameterDirection.Output,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.Number).ToArray().Length,
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Number).ToArray().Length, 16), 
                OracleDbType = OracleDbType.Decimal,
            };
            
            var outItemTaxCCC = new OracleParameter
            {
                ParameterName = "outItemTaxCCC",
                Direction = ParameterDirection.Output,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.Number).ToArray().Length,
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Number).ToArray().Length, 16), 
                OracleDbType = OracleDbType.Decimal,
            };
            _ = _oracleExecutor.ExecuteStoredProcedure(ProcedureName, 
                inHeaderId, 
                inTaxRate, inItemNumber, inItemUnitPrice, inItemQuantity, inItemDescription, outTotalAmount, outItemTaxAAA, outItemTaxBBB, outItemTaxCCC);

            return null;
        }
        
        private static int[] GenerateArrayBindSize(int itemsCount, int bindSize)
        {
            var arrayBindSize = new int[itemsCount];

            for (var i = 0; i < itemsCount; i++)
            {
                arrayBindSize[i] = bindSize;
            }
            return arrayBindSize;
        }
    }
}