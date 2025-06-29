using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STOX.Service.DTOs.User;
using STOX.Service.Interfaces;

namespace STOX.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Регистрирует нового пользователя
    /// </summary>
    /// <param name="registerDto">Данные для регистрации пользователя</param>
    /// <returns>Статус выполнения</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _userService.RegisterAsync(registerDto);
            return Created("", null);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Аутентифицирует пользователя и возвращает JWT-токен
    /// </summary>
    /// <param name="loginDto">Данные для входа</param>
    /// <returns>JWT-токен</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<string>> Login([FromBody] LoginUserDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _userService.LoginAsync(loginDto);
            return Ok(token);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid email or password.");
        }
    }
}