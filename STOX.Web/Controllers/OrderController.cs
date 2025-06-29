using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Получает список всех заказов
    /// </summary>
    /// <returns>Список заказов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrdersDto>>> GetAll()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Получает заказ по ID
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <returns>Заказ</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrdersDto>> GetById(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    /// <summary>
    /// Создаёт новый заказ
    /// </summary>
    /// <param name="orderDto">Данные заказа</param>
    /// <returns>Созданный заказ</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] OrdersDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _orderService.CreateAsync(orderDto);
        return CreatedAtAction(nameof(GetById), new { id = orderDto.Id }, orderDto);
    }

    /// <summary>
    /// Обновляет заказ
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <param name="orderDto">Обновлённые данные заказа</param>
    /// <returns>Статус выполнения</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, [FromBody] OrdersDto orderDto)
    {
        if (id != orderDto.Id || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingOrder = await _orderService.GetByIdAsync(id);
        if (existingOrder == null)
        {
            return NotFound();
        }

        await _orderService.UpdateAsync(orderDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет заказ
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <returns>Статус выполнения</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existingOrder = await _orderService.GetByIdAsync(id);
        if (existingOrder == null)
        {
            return NotFound();
        }

        await _orderService.DeleteAsync(id);
        return NoContent();
    }
}