using Domain.Entities;

namespace DLL.Repositories.Interfaces;

public interface IFilmRepository : IRepository<Film>
{
    public Task<IEnumerable<Film>?> GetByCondition(Func<Film, bool> predicate);
}
