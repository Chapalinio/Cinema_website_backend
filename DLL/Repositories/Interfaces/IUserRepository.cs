using Domain.Entities;

namespace DLL.Repositories.Interfaces;
public interface IUserRepository : IRepository<User>
{
    Task<bool> LoginAsync(string login, string password);
    Task<int?> GetIdByLoginAsync(string login);
}
