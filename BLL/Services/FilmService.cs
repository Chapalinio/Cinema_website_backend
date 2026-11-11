using AutoMapper;
using BLL.DTOs;
using DLL.Repositories.Interfaces;
using Domain.Entities;

namespace BLL.Services;

public class FilmService
{
    private readonly IRepository<Film> _filmRepository;
    private readonly IMapper _mapper;
    public FilmService(IRepository<Film> repository, IMapper mapper)
    {
        _filmRepository = repository;
        _mapper = mapper;
    }

    public List<FilmDTO> GetAll()
    {
        var films = _filmRepository.GetAll();

        if(films == null)
        {
            return [];
        }

        return _mapper.Map<List<FilmDTO>>(films);
    }

    public FilmDTO? GetById(int id)
    {
        var film = _filmRepository.GetById(id);
        
        if(film == null)
        {
            return null;
        }
       
        return _mapper.Map<FilmDTO>(film);
    }

    public void Add(FilmDTO film)
    {
        _filmRepository.Add(_mapper.Map<Film>(film));
    }

    public void Edit(int id, FilmDTO film)
    {
        _filmRepository.Edit(id, _mapper.Map<Film>(film));
    }

    public void Remove(int id)
    {
        _filmRepository.Remove(id);
    }
}
