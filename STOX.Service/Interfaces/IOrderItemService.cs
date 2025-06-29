using STOX.Service.DTOs;

namespace STOX.Service.Interfaces;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemDto>> GetAllAsync();
    Task<OrderItemDto> GetByIdAsync(Guid id);
    Task CreateAsync(OrderItemDto orderItemDto);
    Task UpdateAsync(OrderItemDto orderItemDto);
    Task DeleteAsync(Guid id);
}