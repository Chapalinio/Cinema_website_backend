using DLL.Context;
using DLL.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories;

public class FilmRepository : IRepository<Film>
{
    private readonly AppDbContext _appContext;
    public FilmRepository(AppDbContext context)
    {
        _appContext = context;
    }

    public void Add(Film film)
    {
        if(_appContext.Films.FirstOrDefault(f => f.Title == film.Title) == null)
        {   
            _appContext.Films.Add(film);
            SaveChanges();
        }
    }

    public void Edit(int id, Film film)
    {
        var filmToEdit = _appContext.Films.FirstOrDefault(f => f.Id == id);
        if(filmToEdit != null)
        {
            filmToEdit.Title = film.Title;
            filmToEdit.Description = film.Description;
            filmToEdit.PosterUrl = film.PosterUrl;
            filmToEdit.FilmUrl = film.FilmUrl;
            filmToEdit.ReleaseYear = film.ReleaseYear;
            filmToEdit.LengthInMinutes = film.LengthInMinutes;
            SaveChanges();
        }
    }

    public void Remove(int id)
    {
        var filmToDelete = _appContext.Films.FirstOrDefault(f => f.Id == id);
        if (filmToDelete != null)
        {
            _appContext.Films.Remove(filmToDelete);
            SaveChanges();
        }
    }

    public List<Film>? GetAll()
    {
        return _appContext.Films.Include(f => f.Comments).ToList();
    }

    public Film? GetById(int id)
    {
        return _appContext.Films.Include(f => f.Comments).FirstOrDefault(f => f.Id == id);
    }

    public void SaveChanges()
    {
        _appContext.SaveChanges();
    }
}
