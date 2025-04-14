namespace Ambev.DeveloperEvaluation.Common.DTOs
{
    public class CreateSaleRequest
    {
        public required string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public required string Customer { get; set; }
        public required string Branch { get; set; }
        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }
}

