namespace JIgor.Projects.OracleProcedureExecutor.Samples.models.Input
{
    public class InputItem
    {
        public InputItem(int number, decimal unitPrice, int quantity, string itemDescription)
        {
            Number = number;
            UnitPrice = unitPrice;
            Quantity = quantity;
            ItemDescription = itemDescription;
        }

        public InputItem()
        {
        }

        public int Number { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ItemDescription { get; set; }
    }
}