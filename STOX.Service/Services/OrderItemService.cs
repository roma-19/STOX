using AutoMapper;
using STOX.Data.Entities;
using STOX.Repo;
using STOX.Repo.Interfaces;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Service.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemService(IRepository<OrderItem> orderItemRepository, IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemDto>> GetAllAsync()
    {
        var orderItems = await _orderItemRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
    }

    public async Task<OrderItemDto> GetByIdAsync(Guid id)
    {
        var orderItem = await _orderItemRepository.GetByKeysAsync(id);
        return _mapper.Map<OrderItemDto>(orderItem);
    }

    public async Task CreateAsync(OrderItemDto orderItemDto)
    {
        var orderItem = _mapper.Map<OrderItem>(orderItemDto);
        await _orderItemRepository.AddAsync(orderItem);
        await _orderItemRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItemDto orderItemDto)
    {
        var orderItem = await _orderItemRepository.GetByKeysAsync(orderItemDto.Id);
        if (orderItem == null)
        {
            throw new KeyNotFoundException("Order item not found.");
        }

        _mapper.Map(orderItemDto, orderItem);
        _orderItemRepository.Update(orderItem);
        await _orderItemRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var orderItem = await _orderItemRepository.GetByKeysAsync(id);
        if (orderItem != null)
        {
            _orderItemRepository.Delete(orderItem);
            await _orderItemRepository.SaveChangesAsync();
        }
    }
}