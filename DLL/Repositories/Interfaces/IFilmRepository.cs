using Domain.Entities;
using System.Linq.Expressions;

namespace DLL.Repositories.Interfaces;

public interface IFilmRepository : IRepository<Film>
{
    public Task<IEnumerable<Film>?> GetByConditionAsync(Expression<Func<Film, bool>> predicate);
}