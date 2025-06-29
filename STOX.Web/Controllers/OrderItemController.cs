using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    /// <summary>
    /// Получает список всех элементов заказа
    /// </summary>
    /// <returns>Список элементов заказа</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAll()
    {
        var orderItems = await _orderItemService.GetAllAsync();
        return Ok(orderItems);
    }

    /// <summary>
    /// Получает элемент заказа по ID
    /// </summary>
    /// <param name="id">ID элемента заказа</param>
    /// <returns>Элемент заказа</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderItemDto>> GetById(Guid id)
    {
        var orderItem = await _orderItemService.GetByIdAsync(id);
        if (orderItem == null)
        {
            return NotFound();
        }
        return Ok(orderItem);
    }

    /// <summary>
    /// Создаёт новый элемент заказа
    /// </summary>
    /// <param name="orderItemDto">Данные элемента заказа</param>
    /// <returns>Созданный элемент заказа</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] OrderItemDto orderItemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _orderItemService.CreateAsync(orderItemDto);
        return CreatedAtAction(nameof(GetById), new { id = orderItemDto.Id }, orderItemDto);
    }

    /// <summary>
    /// Обновляет элемент заказа
    /// </summary>
    /// <param name="id">ID элемента заказа</param>
    /// <param name="orderItemDto">Обновлённые данные элемента заказа</param>
    /// <returns>Статус выполнения</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, [FromBody] OrderItemDto orderItemDto)
    {
        if (id != orderItemDto.Id || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingOrderItem = await _orderItemService.GetByIdAsync(id);
        if (existingOrderItem == null)
        {
            return NotFound();
        }

        await _orderItemService.UpdateAsync(orderItemDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет элемент заказа
    /// </summary>
    /// <param name="id">ID элемента заказа</param>
    /// <returns>Статус выполнения</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existingOrderItem = await _orderItemService.GetByIdAsync(id);
        if (existingOrderItem == null)
        {
            return NotFound();
        }

        await _orderItemService.DeleteAsync(id);
        return NoContent();
    }
}