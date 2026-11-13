using DLL.Context;
using DLL.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repositories;

public class FilmRepository : IFilmRepository
{
    private readonly AppDbContext _appContext;
    public FilmRepository(AppDbContext context)
    {
        _appContext = context;
    }

    public async Task AddAsync(Film film)
    {
        if (_appContext.Films.FirstOrDefault(f => f.Title == film.Title) == null)
        {
            await _appContext.Films.AddAsync(film);
            await SaveChangesAsync();
        }
    }

    public async Task EditAsync(int id, Film film)
    {
        var filmToEdit = _appContext.Films.FirstOrDefault(f => f.Id == id);
        if (filmToEdit != null)
        {
            filmToEdit.Title = film.Title;
            filmToEdit.Description = film.Description;
            filmToEdit.PosterUrl = film.PosterUrl;
            filmToEdit.FilmUrl = film.FilmUrl;
            filmToEdit.ReleaseYear = film.ReleaseYear;
            filmToEdit.LengthInMinutes = film.LengthInMinutes;
            await SaveChangesAsync();
        }
    }

    public async void Remove(int id)
    {
        var filmToDelete = _appContext.Films.FirstOrDefault(f => f.Id == id);
        if (filmToDelete != null)
        {
            _appContext.Films.Remove(filmToDelete);
            await SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Film>?> GetAllAsync()
    {
        return await _appContext.Films.Include(f => f.Comments).ToListAsync();
    }

    public async Task<IEnumerable<Film>?> GetByConditionAsync(Expression<Func<Film, bool>> predicate)
    {
        return await _appContext.Films.Where(predicate).ToListAsync();
    }

    public async Task<Film?> GetByIdAsync(int id)
    {
        return _appContext.Films.Include(f => f.Comments).FirstOrDefault(f => f.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _appContext.SaveChangesAsync();
    }
}
