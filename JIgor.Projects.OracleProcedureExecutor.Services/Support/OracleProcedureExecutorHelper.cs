using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Services.Support
{
    public static  class OracleProcedureExecutorHelper
    {
        public static int[] GenerateArrayBindSize(int arraySize, int bindSize)
        {
            var arrayBindSize = new int[arraySize];
            
            for (var i = 0; i < arraySize; i++)
            {
                arrayBindSize[i] = bindSize;
            }
            return arrayBindSize;
        }
        
        public static OracleParameter CreateOracleParameter(
            string name, 
            OracleDbType type,
            ParameterDirection direction,
            object value = default)
        {
            var parameter = new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = type,
                Direction = direction,
                Value = value,
            };

            return parameter;
        }

        public static OracleParameter CreateArrayOracleParameter(
            string name, 
            ParameterDirection direction,
            int size, 
            int bindSize = default,
            object value = default)
        {
            var parameter = new OracleParameter()
            {
                ParameterName = name,
                Direction = direction,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                Size = size,
                ArrayBindSize = GenerateArrayBindSize(size, bindSize),
                Value = value
            };

            return parameter;
        }
    }
}