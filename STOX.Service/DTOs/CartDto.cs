using STOX.Data.Entities;

namespace STOX.Service.DTOs;

public class CartDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}