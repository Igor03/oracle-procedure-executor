using System.Collections.Generic;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.models.Output
{
    public class OutputHeader
    {
        public decimal Id { get; set; }
        
        public decimal TaxRate { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public IEnumerable<OutputItemCalculation> ItemsCalculation { get; set; }
    }
}