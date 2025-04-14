using Ambev.DeveloperEvaluation.Common.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

public interface ISaleService
{
    Task<Guid> CreateSaleAsync(CreateSaleRequest request);
    Task<List<Sale>> GetAllAsync();
    Task<Sale?> GetByIdAsync(Guid id);
    Task UpdateAsync(Guid id, CreateSaleRequest request);
    Task DeleteAsync(Guid id);
}




