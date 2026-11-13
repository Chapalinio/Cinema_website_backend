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
        if (string.IsNullOrWhiteSpace(filters.Title) &&
            string.IsNullOrWhiteSpace(filters.Genre) &&
            filters.ReleaseYear == 0 &&
            string.IsNullOrWhiteSpace(filters.Country) &&
            string.IsNullOrWhiteSpace(filters.KeyWord))
        {
            return await GetAllAsync();
        }

        var hasTitle = !string.IsNullOrWhiteSpace(filters.Title);
        var title = filters.Title.Trim().ToLower();

        var hasGenre = !string.IsNullOrWhiteSpace(filters.Genre);
        var genre = filters.Genre.Trim().ToLower();

        var hasCountry = !string.IsNullOrWhiteSpace(filters.Country);
        var country = filters.Country.Trim().ToLower();

        var hasKeyword = !string.IsNullOrWhiteSpace(filters.KeyWord);
        var keyword = filters.KeyWord.Trim().ToLower();

        var hasYear = filters.ReleaseYear > 0;

        var filteredFilms = await _filmRepository.GetByConditionAsync(f =>
            (!hasTitle || f.Title.ToLower().Contains(title)) &&
            (!hasGenre || f.Genre.ToLower().Contains(genre)) &&
            (!hasCountry || f.Country.ToLower().Contains(country)) &&
            (!hasYear || f.ReleaseYear == filters.ReleaseYear) &&
            (!hasKeyword || f.Title.ToLower().Contains(keyword) || f.Description.ToLower().Contains(keyword))
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
