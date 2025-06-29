using AutoMapper;
using STOX.Data.Entities;
using STOX.Repo;
using STOX.Repo.Interfaces;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Service.Services;

public class CartService : ICartService
{
    private readonly IRepository<Cart> _cartRepository;
    private readonly IMapper _mapper;

    public CartService(IRepository<Cart> cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CartDto>> GetAllAsync()
    {
        var carts = await _cartRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CartDto>>(carts);
    }

    public async Task<CartDto> GetByIdAsync(Guid id)
    {
        var cart = await _cartRepository.GetByKeysAsync(id);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task CreateAsync(CartDto cartDto)
    {
        var cart = _mapper.Map<Cart>(cartDto);
        await _cartRepository.AddAsync(cart);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(CartDto cartDto)
    {
        var cart = await _cartRepository.GetByKeysAsync(cartDto.Id);
        if (cart == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        _mapper.Map(cartDto, cart);
        _cartRepository.Update(cart);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var cart = await _cartRepository.GetByKeysAsync(id);
        if (cart != null)
        {
            _cartRepository.Delete(cart);
            await _cartRepository.SaveChangesAsync();
        }
    }
}