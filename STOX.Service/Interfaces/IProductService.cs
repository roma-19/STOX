using STOX.Service.DTOs;

namespace STOX.Service.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(Guid id);
    Task CreateAsync(ProductDto productDto);
    Task UpdateAsync(ProductDto productDto);
    Task DeleteAsync(Guid id);
}