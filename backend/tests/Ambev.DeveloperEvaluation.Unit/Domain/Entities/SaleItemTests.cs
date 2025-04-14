using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Fact]
    public void ApplyDiscount_ShouldApply10Percent_WhenQuantityBetween5And10()
    {
        var item = new SaleItem("Produto", 6, 10);
        item.ApplyDiscount();

        item.Discount.Should().Be(6); 
        item.TotalAmount.Should().Be(54);
    }

    [Fact]
    public void ApplyDiscount_ShouldApply20Percent_WhenQuantityGreaterThan10()
    {
        var item = new SaleItem("Produto", 11, 10);
        item.ApplyDiscount();

        item.Discount.Should().Be(22); 
        item.TotalAmount.Should().Be(88);
    }

    [Fact]
    public void ApplyDiscount_ShouldApplyZeroDiscount_WhenQuantityLessThan5()
    {
        var item = new SaleItem("Produto", 3, 10);
        item.ApplyDiscount();

        item.Discount.Should().Be(0);
        item.TotalAmount.Should().Be(30);
    }
}
