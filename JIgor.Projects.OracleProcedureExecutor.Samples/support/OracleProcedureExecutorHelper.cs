using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.support
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
            OracleCollectionType collectionType, 
            int size, 
            int bindSize = default,
            object value = default)
        {
            var parameter = new OracleParameter()
            {
                ParameterName = name,
                Direction = direction,
                CollectionType = collectionType,
                Size = size,
                ArrayBindSize = GenerateArrayBindSize(size, bindSize),
                Value = value
            };

            return parameter;
        }
    }
}