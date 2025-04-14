using Ambev.DeveloperEvaluation.Application.Services.Sales;
using Ambev.DeveloperEvaluation.Common.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Services.Sales;

public class SaleServiceTests
{
    [Fact]
    public async Task CreateSaleAsync_ShouldApplyDiscountRulesCorrectly()
    {
        var mockRepo = new Mock<ISaleRepository>();
        var service = new SaleService(mockRepo.Object);

        var request = new CreateSaleRequest
        {
            SaleNumber = "123",
            SaleDate = DateTime.Today,
            Customer = "Cliente Teste",
            Branch = "Filial 1",
            Items = new List<CreateSaleItemRequest>
            {
                new() { Product = "Cerveja", Quantity = 5, UnitPrice = 10 },   
                new() { Product = "Vodka", Quantity = 15, UnitPrice = 20 }     
            }
        };

        var result = await service.CreateSaleAsync(request);

        result.Should().NotBeEmpty();

        mockRepo.Verify(x => x.AddAsync(It.Is<Sale>(s =>
            s.Items[0].Discount == 5 &&     
            s.Items[1].Discount == 60     
        )), Times.Once);
    }

    [Fact]
    public async Task CreateSaleAsync_ShouldThrow_WhenItemQuantityExceedsLimit()
    {
        var mockRepo = new Mock<ISaleRepository>();
        var service = new SaleService(mockRepo.Object);

        var request = new CreateSaleRequest
        {
            SaleNumber = "002",
            SaleDate = DateTime.Today,
            Customer = "Erro",
            Branch = "Loja XPTO",
            Items = new List<CreateSaleItemRequest>
            {
                new() { Product = "Excesso", Quantity = 25, UnitPrice = 10 }
            }
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateSaleAsync(request));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfSales()
    {
        var sales = new List<Sale>
        {
            new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = "001",
                Customer = "A",
                Branch = "1",
                SaleDate = DateTime.Today
            },
            new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = "002",
                Customer = "B",
                Branch = "2",
                SaleDate = DateTime.Today
            }
        };

        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(sales);

        var service = new SaleService(mockRepo.Object);
        var result = await service.GetAllAsync();

        result.Should().HaveCount(2);
        result[0].Customer.Should().Be("A");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnSale_WhenFound()
    {
        var id = Guid.NewGuid();
        var sale = new Sale
        {
            Id = id,
            SaleNumber = "XYZ",
            Customer = "João",
            Branch = "XPTO",
            SaleDate = DateTime.Today
        };

        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(sale);

        var service = new SaleService(mockRepo.Object);
        var result = await service.GetByIdAsync(id);

        result.Should().NotBeNull();
        result.Id.Should().Be(id);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateSale()
    {
        var id = Guid.NewGuid();
        var existing = new Sale
        {
            Id = id,
            SaleNumber = "001",
            Customer = "Antigo",
            Branch = "Loja A",
            SaleDate = DateTime.Today
        };

        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(existing);

        var service = new SaleService(mockRepo.Object);

        var request = new CreateSaleRequest
        {
            SaleNumber = "001",
            SaleDate = DateTime.Today,
            Customer = "Atualizado",
            Branch = "Unidade 2",
            Items = new List<CreateSaleItemRequest>
            {
                new() { Product = "Item", Quantity = 3, UnitPrice = 15 }
            }
        };

        await service.UpdateAsync(id, request);

        mockRepo.Verify(x => x.UpdateAsync(It.Is<Sale>(s =>
            s.Customer == "Atualizado" &&
            s.Items.Count == 1 &&
            s.Items[0].Product == "Item"
        )), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCancelSale()
    {
        var id = Guid.NewGuid();
        var sale = new Sale
        {
            Id = id,
            SaleNumber = "DEL-01",
            Customer = "Carlos",
            Branch = "Centro",
            SaleDate = DateTime.Today,
            IsCancelled = false
        };

        var mockRepo = new Mock<ISaleRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(sale);

        var service = new SaleService(mockRepo.Object);
        await service.DeleteAsync(id);

        mockRepo.Verify(x => x.UpdateAsync(It.Is<Sale>(s => s.IsCancelled)), Times.Once);
    }
}
