using Domain.Entities;

namespace DLL.Repositories.Interfaces;

public interface IFilmSessionRepository : IRepository<FilmSession>
{
    Task<bool> FilmExistAsync(int filmId);
}
