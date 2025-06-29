using STOX.Data.Entities;

namespace STOX.Repo.Interfaces;

public interface IUserRepository :  IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}