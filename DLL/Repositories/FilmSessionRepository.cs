using DLL.Context;
using DLL.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories;

public class FilmSessionRepository : IFilmSessionRepository
{
    private readonly AppDbContext _dbContext;

    public FilmSessionRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(FilmSession data)
    {
        if (await FilmExistAsync(data.FilmId))
        {
            await _dbContext.FilmSessions.AddAsync(data);

            await SaveChangesAsync();
        }
    }

    public async Task EditAsync(int id, FilmSession data)
    {
        if (await FilmExistAsync(data.FilmId) && await FilmExistAsync(id))
        {
            var filmToEdit = _dbContext.FilmSessions.FirstOrDefault(fs => fs.Id == id);

            filmToEdit.HallName = data.HallName;
            filmToEdit.Price = data.Price;
            filmToEdit.AvailableSeats = data.AvailableSeats;
            filmToEdit.StartsAt = data.StartsAt;
            filmToEdit.FilmId = data.FilmId;

            await SaveChangesAsync();
        }
    }

    public async Task<bool> FilmExistAsync(int filmId)
    {
        var film = _dbContext.Films.FirstOrDefault(f => f.Id == filmId);

        if (film == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<IEnumerable<FilmSession>?> GetAllAsync()
    {
        return await _dbContext.FilmSessions.ToListAsync();
    }

    public async Task<FilmSession?> GetByIdAsync(int id)
    {
        return _dbContext.FilmSessions.FirstOrDefault(fs => fs.Id == id);
    }

    public void Remove(int id)
    {
        var filmToRemove = _dbContext.FilmSessions.FirstOrDefault(fs => fs.Id == id);

        if (filmToRemove != null)
        {
            _dbContext.FilmSessions.Remove(filmToRemove);

            _dbContext.SaveChanges();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
