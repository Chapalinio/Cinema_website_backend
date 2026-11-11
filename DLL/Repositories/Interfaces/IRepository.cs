namespace DLL.Repositories.Interfaces;

public interface IRepository<T>
{
    void Add(T data);
    void Edit(int id, T data);
    void Remove(int id);
    List<T>? GetAll();
    T? GetById(int id);
    void SaveChanges();
}
