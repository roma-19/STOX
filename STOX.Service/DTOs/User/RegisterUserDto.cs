using System.ComponentModel.DataAnnotations;
using STOX.Data.Enums;

namespace STOX.Service.DTOs.User;

public class RegisterUserDto
{
    [Required]
    public string Name { get; set; }
    
    public string ContactInfo { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}