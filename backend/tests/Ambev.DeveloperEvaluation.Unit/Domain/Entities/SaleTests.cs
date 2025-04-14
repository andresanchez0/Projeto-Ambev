using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void AddItem_ShouldAddItemWithCorrectValues()
    {
        var sale = new Sale
        {
            SaleNumber = "001",
            Customer = "João",
            Branch = "Loja 1",
            SaleDate = DateTime.Today
        };

        sale.AddItem("Produto", 3, 10);

        sale.Items.Should().HaveCount(1);
        sale.Items[0].Product.Should().Be("Produto");
        sale.Items[0].Quantity.Should().Be(3);
        sale.Items[0].UnitPrice.Should().Be(10);
        sale.Items[0].Discount.Should().Be(0); 
    }

    [Fact]
    public void TotalAmount_ShouldSumAllItemsCorrectly()
    {
        var sale = new Sale
        {
            SaleNumber = "002",
            Customer = "Maria",
            Branch = "Filial 2",
            SaleDate = DateTime.Today
        };

        sale.AddItem("Item A", 5, 10); 
        sale.AddItem("Item B", 12, 10); 

        sale.TotalAmount.Should().Be(141);
    }

    [Fact]
    public void Cancel_ShouldSetIsCancelledToTrue()
    {
        var sale = new Sale
        {
            SaleNumber = "003",
            Customer = "Carlos",
            Branch = "Loja Teste",
            SaleDate = DateTime.Today
        };

        sale.IsCancelled.Should().BeFalse();

        sale.Cancel();

        sale.IsCancelled.Should().BeTrue();
    }
}
