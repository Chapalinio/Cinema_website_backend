using AutoMapper;
using BLL.DTOs;
using DLL.Repositories.Interfaces;
using Domain.Entities;

namespace BLL.Services;

public class FilmService
{
    private readonly IFilmRepository _filmRepository;
    private readonly IMapper _mapper;
    public FilmService(IFilmRepository repository, IMapper mapper)
    {
        _filmRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<FilmDTO>> GetAllAsync()
    {
        var films = await _filmRepository.GetAllAsync();

        if (films == null)
        {
            return [];
        }

        return _mapper.Map<List<FilmDTO>>(films);
    }

    public async Task<FilmDTO?> GetByIdAsync(int id)
    {
        var film = await _filmRepository.GetByIdAsync(id);

        if (film == null)
        {
            return null;
        }

        return _mapper.Map<FilmDTO>(film);
    }

    public async Task<List<FilmDTO>> SearchFilmByFiltersAsync(SearchFiltersDTO filters)
    {
        if (filters.Title == "" && filters.Genre == "" && filters.ReleaseYear == 0 && filters.Country == "" && filters.KeyWord == "")
        {
            return await GetAllAsync();
        }

        var filteredFilms = await _filmRepository.GetByConditionAsync(f =>
            f.Title.ToLower().Contains(filters.Title.ToLower()) ||
            f.Title.ToLower().Contains(filters.KeyWord.ToLower()) ||
            f.Description.ToLower().Contains(filters.KeyWord.ToLower()) ||
            f.Genre.ToLower().Contains(filters.Genre.ToLower()) ||
            f.ReleaseYear == filters.ReleaseYear ||
            f.Country.ToLower().Contains(filters.Country.ToLower())
        );

        return _mapper.Map<List<FilmDTO>>(filteredFilms);
    }

    public async Task AddAsync(FilmDTO film)
    {
        await _filmRepository.AddAsync(_mapper.Map<Film>(film));
    }

    public async Task EditAsync(int id, FilmDTO film)
    {
        await _filmRepository.EditAsync(id, _mapper.Map<Film>(film));
    }

    public void Remove(int id)
    {
        _filmRepository.Remove(id);
    }
}
