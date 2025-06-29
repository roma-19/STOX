using AutoMapper;
using STOX.Data.Entities;
using STOX.Repo.Interfaces;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Service.Services;

public class CartItemService : ICartItemService
{
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IMapper _mapper;

    public CartItemService(IRepository<CartItem> cartItemRepository, IMapper mapper)
    {
        _cartItemRepository = cartItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CartItemDto>> GetAllAsync()
    {
        var cartItems = await _cartItemRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CartItemDto>>(cartItems);
    }

    public async Task<CartItemDto> GetByIdAsync(Guid id)
    {
        var cartItem = await _cartItemRepository.GetByKeysAsync(id);
        return _mapper.Map<CartItemDto>(cartItem);
    }

    public async Task CreateAsync(CartItemDto cartItemDto)
    {
        var cartItem = _mapper.Map<CartItem>(cartItemDto);
        await _cartItemRepository.AddAsync(cartItem);
        await _cartItemRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(CartItemDto cartItemDto)
    {
        var cartItem = await _cartItemRepository.GetByKeysAsync(cartItemDto.Id);
        if (cartItem == null)
        {
            throw new KeyNotFoundException("Cart item not found.");
        }

        _mapper.Map(cartItemDto, cartItem);
        _cartItemRepository.Update(cartItem);
        await _cartItemRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var cartItem = await _cartItemRepository.GetByKeysAsync(id);
        if (cartItem != null)
        {
            _cartItemRepository.Delete(cartItem);
            await _cartItemRepository.SaveChangesAsync();
        }
    }
}