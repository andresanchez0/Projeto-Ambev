namespace Ambev.DeveloperEvaluation.Common.DTOs
{
    public class CreateSaleItemRequest
    {
        public required string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
