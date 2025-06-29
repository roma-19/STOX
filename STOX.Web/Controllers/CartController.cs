using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    /// <summary>
    /// Получает список всех корзин
    /// </summary>
    /// <returns>Список корзин</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CartDto>>> GetAll()
    {
        var carts = await _cartService.GetAllAsync();
        return Ok(carts);
    }

    /// <summary>
    /// Получает корзину по ID
    /// </summary>
    /// <param name="id">ID корзины</param>
    /// <returns>Корзина</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartDto>> GetById(Guid id)
    {
        var cart = await _cartService.GetByIdAsync(id);
        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    /// <summary>
    /// Создаёт новую корзину
    /// </summary>
    /// <param name="cartDto">Данные корзины</param>
    /// <returns>Созданная корзина</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] CartDto cartDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _cartService.CreateAsync(cartDto);
        return CreatedAtAction(nameof(GetById), new { id = cartDto.Id }, cartDto);
    }

    /// <summary>
    /// Обновляет корзину
    /// </summary>
    /// <param name="id">ID корзины</param>
    /// <param name="cartDto">Обновлённые данные корзины</param>
    /// <returns>Статус выполнения</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, [FromBody] CartDto cartDto)
    {
        if (id != cartDto.Id || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingCart = await _cartService.GetByIdAsync(id);
        if (existingCart == null)
        {
            return NotFound();
        }

        await _cartService.UpdateAsync(cartDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет корзину
    /// </summary>
    /// <param name="id">ID корзины</param>
    /// <returns>Статус выполнения</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existingCart = await _cartService.GetByIdAsync(id);
        if (existingCart == null)
        {
            return NotFound();
        }

        await _cartService.DeleteAsync(id);
        return NoContent();
    }
}