using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    /// <summary>
    /// Получает список всех уведомлений
    /// </summary>
    /// <returns>Список уведомлений</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAll()
    {
        var notifications = await _notificationService.GetAllAsync();
        return Ok(notifications);
    }

    /// <summary>
    /// Получает уведомление по ID
    /// </summary>
    /// <param name="id">ID уведомления</param>
    /// <returns>Уведомление</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NotificationDto>> GetById(Guid id)
    {
        var notification = await _notificationService.GetByIdAsync(id);
        if (notification == null)
        {
            return NotFound();
        }
        return Ok(notification);
    }

    /// <summary>
    /// Создаёт новое уведомление
    /// </summary>
    /// <param name="notificationDto">Данные уведомления</param>
    /// <returns>Созданное уведомление</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] NotificationDto notificationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _notificationService.CreateAsync(notificationDto);
        return CreatedAtAction(nameof(GetById), new { id = notificationDto.Id }, notificationDto);
    }

    /// <summary>
    /// Обновляет уведомление
    /// </summary>
    /// <param name="id">ID уведомления</param>
    /// <param name="notificationDto">Обновлённые данные уведомления</param>
    /// <returns>Статус выполнения</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, [FromBody] NotificationDto notificationDto)
    {
        if (id != notificationDto.Id || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingNotification = await _notificationService.GetByIdAsync(id);
        if (existingNotification == null)
        {
            return NotFound();
        }

        await _notificationService.UpdateAsync(notificationDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет уведомление
    /// </summary>
    /// <param name="id">ID уведомления</param>
    /// <returns>Статус выполнения</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existingNotification = await _notificationService.GetByIdAsync(id);
        if (existingNotification == null)
        {
            return NotFound();
        }

        await _notificationService.DeleteAsync(id);
        return NoContent();
    }
}