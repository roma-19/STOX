using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOX.Service.DTOs;
using STOX.Service.Interfaces;

namespace STOX.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Получает список всех продуктов
    /// </summary>
    /// <returns>Список продуктов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    /// <summary>
    /// Получает продукт по ID
    /// </summary>
    /// <param name="id">ID продукта</param>
    /// <returns>Продукт</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    /// <summary>
    /// Создаёт новый продукт
    /// </summary>
    /// <param name="productDto">Данные продукта</param>
    /// <returns>Созданный продукт</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productService.CreateAsync(productDto);
        return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
    }

    /// <summary>
    /// Обновляет продукт
    /// </summary>
    /// <param name="id">ID продукта</param>
    /// <param name="productDto">Обновлённые данные продукта</param>
    /// <returns>Статус выполнения</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, [FromBody] ProductDto productDto)
    {
        if (id != productDto.Id || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingProduct = await _productService.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _productService.UpdateAsync(productDto);
        return NoContent();
    }

    /// <summary>
    /// Удаляет продукт
    /// </summary>
    /// <param name="id">ID продукта</param>
    /// <returns>Статус выполнения</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existingProduct = await _productService.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _productService.DeleteAsync(id);
        return NoContent();
    }
}