namespace DLL.Repositories.Interfaces;

public interface IRepository<T>
{
    Task AddAsync(T data);
    Task EditAsync(int id, T data);
    void Remove(int id);
    Task<IEnumerable<T>?> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}
