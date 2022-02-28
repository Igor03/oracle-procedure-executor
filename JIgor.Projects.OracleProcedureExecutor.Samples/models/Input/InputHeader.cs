using System.Collections.Generic;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.models.Input
{
    public class InputHeader
    {
        public InputHeader()
        {
        }
        
        public decimal Id { get; set; }

        public decimal TaxRate { get; set; }
         
        public IEnumerable<InputItem> Items { get; set; }
    }
}