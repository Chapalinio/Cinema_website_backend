using Domain.Entities;

namespace DLL.Repositories.Interfaces;
public interface IUserRepository : IRepository<User>
{
    bool Login(string login, string password);
    int? GetIdByLogin(string login);
}
