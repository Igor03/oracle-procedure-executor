namespace JIgor.Projects.OracleProcedureExecutor.Samples.models.Input
{
    public class InputItem
    {
        public InputItem(decimal number, decimal unitPrice, decimal quantity, string itemDescription)
        {
            Number = number;
            UnitPrice = unitPrice;
            Quantity = quantity;
            ItemDescription = itemDescription;
        }

        public InputItem()
        {
        }

        public decimal Number { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public string ItemDescription { get; set; }
    }
}