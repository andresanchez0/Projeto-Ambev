using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount => (UnitPrice * Quantity) - Discount;

        public SaleItem(string product, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Não é permitido vender mais de 20 unidades do mesmo item.");

            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void ApplyDiscount()
        {
            if (Quantity >= 10 && Quantity <= 20)
                Discount = UnitPrice * Quantity * 0.20m;
            else if (Quantity >= 4)
                Discount = UnitPrice * Quantity * 0.10m;
            else
                Discount = 0;
        }
    }
}
