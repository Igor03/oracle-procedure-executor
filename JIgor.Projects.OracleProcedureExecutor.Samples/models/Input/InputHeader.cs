using System.Collections.Generic;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.models.Input
{
    public class InputHeader
    {
        public int Id { get; set; }
        
        public int PurchaseTotal { get; set; }
        
        public decimal TaxRate { get; set; }
        
        public IEnumerable<InputItem> Items { get; set; }
    }
}