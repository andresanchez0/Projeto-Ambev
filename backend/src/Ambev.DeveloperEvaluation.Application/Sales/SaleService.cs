using Ambev.DeveloperEvaluation.Common.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Services.Sales;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Guid> CreateSaleAsync(CreateSaleRequest request)
    {
        var sale = new Sale
        {
            SaleNumber = request.SaleNumber,
            SaleDate = request.SaleDate,
            Customer = request.Customer,
            Branch = request.Branch
        };

        foreach (var item in request.Items)
        {
            if (item.Quantity > 20)
                throw new InvalidOperationException("Não é permitido vender mais de 20 unidades do mesmo produto.");

            sale.AddItem(item.Product, item.Quantity, item.UnitPrice);
        }

        await _saleRepository.AddAsync(sale);
        return sale.Id;
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _saleRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(Guid id, CreateSaleRequest request)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null)
            throw new KeyNotFoundException("Venda não encontrada.");

        sale.SaleNumber = request.SaleNumber;
        sale.SaleDate = request.SaleDate;
        sale.Customer = request.Customer;
        sale.Branch = request.Branch;

        sale.Items.Clear();

        foreach (var item in request.Items)
        {
            if (item.Quantity > 20)
                throw new InvalidOperationException("Não é permitido vender mais de 20 unidades do mesmo produto.");

            sale.AddItem(item.Product, item.Quantity, item.UnitPrice);
        }

        await _saleRepository.UpdateAsync(sale);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null)
            throw new KeyNotFoundException("Venda não encontrada.");

        sale.Cancel();
        await _saleRepository.UpdateAsync(sale);
    }
}


