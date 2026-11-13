using DLL.Context;
using DLL.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appContext;

    public UserRepository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task AddAsync(User user)
    {
        if (_appContext.Users.FirstOrDefault(u => u.Login == user.Login) == null)
        {
            await _appContext.Users.AddAsync(user);
            await SaveChangesAsync();
        }
    }

    public async Task EditAsync(int id, User user)
    {
        var userToEdit = _appContext.Users.FirstOrDefault(u => u.Id == id);

        if (userToEdit != null)
        {
            userToEdit = user;
            await SaveChangesAsync();
        }
    }

    public async void Remove(int id)
    {
        var userToDelete = _appContext.Users.FirstOrDefault(u => u.Id == id);

        if (userToDelete != null)
        {
            _appContext.Users.Remove(userToDelete);
            await SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        return await _appContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return _appContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public async Task<int?> GetIdByLoginAsync(string login)
    {
        return _appContext.Users.FirstOrDefault(u => u.Login == login).Id;
    }

    public async Task<bool> LoginAsync(string login, string password)
    {
        if (_appContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password) != null)
        {
            return true;
        }

        return false;
    }

    public async Task SaveChangesAsync()
    {
        await _appContext.SaveChangesAsync();
    }
}
