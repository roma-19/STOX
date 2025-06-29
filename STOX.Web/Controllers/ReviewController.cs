using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /// <summary>
    /// Получает список всех отзывов
    /// </summary>
    /// <returns>Список отзывов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
    {
        var reviews = await _reviewService.GetAllAsync();
        return Ok(reviews);
    }

    /// <summary>
    /// Получает отзыв по ID
    /// </summary>
    /// <param name="id">ID отзыва</param>
    /// <returns>Отзыв</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviewDto>> GetById(Guid id)
    {
        var review = await _reviewService.GetByIdAsync(id);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review);
    }

    /// <summary>
    /// Создаёт новый отзыв
    /// </summary>
    /// <param name="reviewDto">Данные отзыва</param>
    /// <returns>Созданный отзыв</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] ReviewDto reviewDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _reviewService.CreateAsync(reviewDto);
        return CreatedAtAction(nameof(GetById), new { id = reviewDto.Id }, reviewDto);
    }

    /// <summary>
    /// Обновляет отзыв
    /// </summary>
    /// <param name="id">ID отзыва</param>
    /// <param name="reviewDto">Обновлённые данные отзыва</param>
    /// <returns>Статус выполнения</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, [FromBody] ReviewDto reviewDto)
    {
        if (id != reviewDto.Id || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingReview = await _reviewService.GetByIdAsync(id);
        if (existingReview == null)
        {
            return NotFound();
        }

        await _reviewService.UpdateAsync(reviewDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет отзыв
    /// </summary>
    /// <param name="id">ID отзыва</param>
    /// <returns>Статус выполнения</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existingReview = await _reviewService.GetByIdAsync(id);
        if (existingReview == null)
        {
            return NotFound();
        }

        await _reviewService.DeleteAsync(id);
        return NoContent();
    }
}