﻿namespace STOX.Service.DTOs.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ContactInfo { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}