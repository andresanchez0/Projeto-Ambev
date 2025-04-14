using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public required string Customer { get; set; }
        public required string Branch { get; set; }

        public decimal TotalAmount => Items.Sum(i => i.TotalAmount);
        public bool IsCancelled { get; set; } = false;

        public List<SaleItem> Items { get; set; } = new();

        public void AddItem(string product, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(product, quantity, unitPrice);
            item.ApplyDiscount();
            Items.Add(item);
        }

        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}
