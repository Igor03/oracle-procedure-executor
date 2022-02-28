using System.Collections.Generic;
using System.Runtime.Intrinsics;
using JIgor.Projects.OracleProcedureExecutor.Samples.models.Input;
using JIgor.Projects.OracleProcedureExecutor.Samples.SampleProcedures;
using JIgor.Projects.OracleProcedureExecutor.Services;
using Microsoft.Extensions.DependencyInjection;
using static JIgor.Projects.OracleProcedureExecutor.Services.DependencyResolver;

namespace JIgor.Projects.OracleProcedureExecutor.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Getting Services

            var host = CreateHostBuilder(args).Build();
            var procedureExecutor = host.Services.GetService<OracleExecutor>();

            #endregion

            // #region Sample 1
            //
            // var getNextVal = new GetNextVal(procedureExecutor);
            // getNextVal.Run();
            //
            //
            // #endregion
            //
            // #region Sample 2
            //
            // var mathOperations = new MathOperations(procedureExecutor);
            // mathOperations.Run(2.4M, 1.2M);
            //
            // #endregion
            //
            // #region Sample 3
            //
            // var productsPrices = new List<decimal>()
            // {
            //     10M, 1.2M, 12.23M
            // };
            //
            // var productTypes = new List<string>()
            // {
            //     "Type1", "Type2", "Type3"
            // };
            //
            // var applyTaxes = new ApplyTaxes(procedureExecutor);
            // applyTaxes.Run(productsPrices, productTypes);
            //
            // #endregion

            var headerInput = new InputHeader()
            {
                Id = 1M,
                TaxRate = .2M,
                Items = new List<InputItem>()
                {
                    new InputItem(1M, 12.12M, 1M, "Description"),
                    new InputItem(2M, 0.16M, 12M, "Description"),
                    new InputItem(3M, 2.22M, 3M, "Description"),
                }
            };
            var unifiedCalculation = new UnifiedCalculation(procedureExecutor, headerInput);
            unifiedCalculation.Run();
        }
    }
}
