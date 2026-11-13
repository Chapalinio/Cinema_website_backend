using Domain.Entities;

namespace DLL.Repositories.Interfaces;

public interface IFilmRepository : IRepository<Film>
{
    public Task<IEnumerable<Film>?> GetByConditionAsync(Func<Film, bool> predicate);
}
