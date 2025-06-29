using AutoMapper;
using STOX.Data.Entities;
using STOX.Repo;
using STOX.Repo.Interfaces;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Service.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Orders> _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IRepository<Orders> orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrdersDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrdersDto>>(orders);
    }

    public async Task<OrdersDto> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByKeysAsync(id);
        return _mapper.Map<OrdersDto>(order);
    }

    public async Task CreateAsync(OrdersDto orderDto)
    {
        var order = _mapper.Map<Orders>(orderDto);
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrdersDto ordersDto)
    {
        var order = await _orderRepository.GetByKeysAsync(ordersDto.Id);
        if (order == null)
        {
            throw new KeyNotFoundException("Order not found.");
        }

        _mapper.Map(ordersDto, order);
        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await _orderRepository.GetByKeysAsync(id);
        if (order != null)
        {
            _orderRepository.Delete(order);
            await _orderRepository.SaveChangesAsync();
        }
    }
}