using System;
using System.Collections.Generic;
using System.Linq;
using JIgor.Projects.OracleProcedureExecutor.Services;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.SampleProcedures
{
    public class ApplyTaxes
    {
        private readonly OracleExecutor _oracleExecutor;

        public ApplyTaxes(OracleExecutor oracleExecutor)
        {
            _oracleExecutor = oracleExecutor;
        }

        private const string ProcedureName = "JIGOR_PROJECTS_PKG.apply_taxes";

        public OracleParameter InProductOriginalPriceArray = new OracleParameter
        {
            ParameterName = "inProductOriginalPriceArray",
            Direction = System.Data.ParameterDirection.InputOutput,
            CollectionType = OracleCollectionType.PLSQLAssociativeArray
        };

        public OracleParameter InProductTypeArray = new OracleParameter
        {
            ParameterName = "inProductTypeArray",
            Direction = System.Data.ParameterDirection.InputOutput,
            CollectionType = OracleCollectionType.PLSQLAssociativeArray
        };

        public void Run(IEnumerable<decimal> productsPrices, IEnumerable<string> productsTypes)
        {
            InProductOriginalPriceArray.Value = productsPrices.ToArray();
            InProductOriginalPriceArray.Size = productsPrices.Count();
            InProductOriginalPriceArray.ArrayBindSize = GenerateIntegerArrayBindSize(productsPrices.Count());

            InProductTypeArray.Value = productsTypes.ToArray();
            InProductTypeArray.Size = productsTypes.Count();
            InProductTypeArray.ArrayBindSize = GenerateStringArrayBindSize(productsTypes.Count());

            _ = _oracleExecutor.ExecuteStoredProcedure(ProcedureName, 
                InProductOriginalPriceArray, 
                InProductTypeArray);

            var newPrices = (decimal[])InProductOriginalPriceArray.Value;

            foreach (var newPrice in newPrices)
            {
                Console.WriteLine($"New price is: {newPrice}");
            }
        }

        private static int[] GenerateStringArrayBindSize(int itemsCount)
        {
            var arrayBindSize = new int[itemsCount];

            for (var i = 0; i < itemsCount; i++)
            {
                arrayBindSize[i] = 2000;
            }
            return arrayBindSize;
        }

        private static int[] GenerateIntegerArrayBindSize(int itemsCount)
        {
            var arrayBindSize = new int[itemsCount];

            for (var i = 0; i < itemsCount; i++)
            {
                arrayBindSize[i] = 12;
            }
            return arrayBindSize;
        }
    }
}
