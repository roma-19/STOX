using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    /// <summary>
    /// Получает список всех элементов корзины
    /// </summary>
    /// <returns>Список элементов корзины</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAll()
    {
        var cartItems = await _cartItemService.GetAllAsync();
        return Ok(cartItems);
    }

    /// <summary>
    /// Получает элемент корзины по ID
    /// </summary>
    /// <param name="id">ID элемента корзины</param>
    /// <returns>Элемент корзины</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartItemDto>> GetById(Guid id)
    {
        var cartItem = await _cartItemService.GetByIdAsync(id);
        if (cartItem == null)
        {
            return NotFound();
        }
        return Ok(cartItem);
    }

    /// <summary>
    /// Создаёт новый элемент корзины
    /// </summary>
    /// <param name="cartItemDto">Данные элемента корзины</param>
    /// <returns>Созданный элемент корзины</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] CartItemDto cartItemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _cartItemService.CreateAsync(cartItemDto);
        return CreatedAtAction(nameof(GetById), new { id = cartItemDto.Id }, cartItemDto);
    }

    /// <summary>
    /// Обновляет элемент корзины
    /// </summary>
    /// <param name="id">ID элемента корзины</param>
    /// <param name="cartItemDto">Обновлённые данные элемента корзины</param>
    /// <returns>Статус выполнения</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, [FromBody] CartItemDto cartItemDto)
    {
        if (id != cartItemDto.Id || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingCartItem = await _cartItemService.GetByIdAsync(id);
        if (existingCartItem == null)
        {
            return NotFound();
        }

        await _cartItemService.UpdateAsync(cartItemDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет элемент корзины
    /// </summary>
    /// <param name="id">ID элемента корзины</param>
    /// <returns>Статус выполнения</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existingCartItem = await _cartItemService.GetByIdAsync(id);
        if (existingCartItem == null)
        {
            return NotFound();
        }

        await _cartItemService.DeleteAsync(id);
        return NoContent();
    }
}