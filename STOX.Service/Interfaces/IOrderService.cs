using STOX.Service.DTOs;

namespace STOX.Service.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrdersDto>> GetAllAsync();
    Task<OrdersDto> GetByIdAsync(Guid id);
    Task CreateAsync(OrdersDto orderDto);
    Task UpdateAsync(OrdersDto orderDto);
    Task DeleteAsync(Guid id);
}