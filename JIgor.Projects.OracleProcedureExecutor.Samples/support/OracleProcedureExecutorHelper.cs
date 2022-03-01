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
    }
}