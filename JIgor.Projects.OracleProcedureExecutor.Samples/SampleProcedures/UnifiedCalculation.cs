using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Input;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Output;
using JIgor.Projects.OracleProcedureExecutor.Services;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

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
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Number).ToArray().Length, 12), 
            };
            
            var inItemUnitPrice = new OracleParameter
            {
                ParameterName = "inItemUnitPrice",
                Direction = ParameterDirection.Input,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.UnitPrice).ToArray().Length,
                Value = _header.Items.Select(p => p.UnitPrice).ToArray(),
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.UnitPrice).ToArray().Length, 12), 
            };
            
            var inItemQuantity = new OracleParameter
            {
                ParameterName = "inItemQuantity",
                Direction = ParameterDirection.Input,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.Quantity).ToArray().Length,
                Value = _header.Items.Select(p => p.Quantity).ToArray(),
                ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Quantity).ToArray().Length, 12), 
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
                // ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Number).ToArray().Length, 12), 
                // OracleDbType = OracleDbType.Decimal,
                Value = Array.Empty<decimal>()
            };
            
            var outItemTaxBBB = new OracleParameter
            {
                ParameterName = "outItemTaxBBB",
                Direction = ParameterDirection.Output,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.Number).ToArray().Length,
                // ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Number).ToArray().Length, 12), 
                // OracleDbType = OracleDbType.Decimal,
                Value = Array.Empty<decimal>(),
            };
            
            var outItemTaxCCC = new OracleParameter
            {
                ParameterName = "outItemTaxCCC",
                Direction = ParameterDirection.Output,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = _header.Items.Select(p => p.Number).ToArray().Length,
                // ArrayBindSize = GenerateArrayBindSize(_header.Items.Select(p => p.Number).ToArray().Length, 12), 
                // OracleDbType = OracleDbType.Decimal,
                Value = Array.Empty<decimal>()
            };
            _ = _oracleExecutor.ExecuteStoredProcedure(ProcedureName, 
                inHeaderId, 
                inTaxRate, inItemNumber, inItemUnitPrice, inItemQuantity, inItemDescription, outTotalAmount, outItemTaxAAA, outItemTaxBBB, outItemTaxCCC);

            // That's important
            var x = (decimal[]) outItemTaxAAA.Value;
            
            // var @return = new OutputHeader()
            // {   
            //     Id = _header.Id,
            //     TotalAmount = (decimal) outTotalAmount.Value,
            //     TaxRate = _header.TaxRate,
            //     ItemsCalculation = new List<OutputItemCalculation>()
            //     {
            //         new OutputItemCalculation()
            //         {
            //         }
            //     }
            // };
            
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