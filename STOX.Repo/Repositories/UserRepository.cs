using Microsoft.EntityFrameworkCore;
using STOX.Data.Entities;
using STOX.Repo.Interfaces;

namespace STOX.Repo.Repositories;

public class UserRepository : Repository<User>,  IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}