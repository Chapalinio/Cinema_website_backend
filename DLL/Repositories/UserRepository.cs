using DLL.Context;
using DLL.Repositories.Interfaces;
using Domain.Entities;

namespace DLL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appContext;

    public UserRepository(AppDbContext context)
    {
        _appContext = context;
    }

    public void Add(User user)
    {
        if (_appContext.Users.FirstOrDefault(u => u.Login == user.Login) == null)
        {
            _appContext.Users.Add(user);
            SaveChanges();
        }
    }

    public void Edit(int id, User user)
    {
        var userToEdit = _appContext.Users.FirstOrDefault(u => u.Id == id);

        if(userToEdit != null)
        {
            userToEdit = user;
            SaveChanges();
        }
    }

    public void Remove(int id)
    {
        var userToDelete = _appContext.Users.FirstOrDefault(u => u.Id == id);

        if (userToDelete != null)
        {
            _appContext.Users.Remove(userToDelete);
            SaveChanges();
        }
    }

    public List<User>? GetAll()
    {
        return _appContext.Users.ToList();
    }

    public User? GetById(int id)
    {
        return _appContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public int? GetIdByLogin(string login)
    {
        return _appContext.Users.FirstOrDefault(u => u.Login == login).Id;
    }

    public bool Login(string login, string password)
    {
        if (_appContext.Users.FirstOrDefault(u => u.Login == login && u.Password == password) != null)
        {
            return true;
        }

        return false;
    }

    public void SaveChanges()
    {
        _appContext.SaveChanges();
    }
}
