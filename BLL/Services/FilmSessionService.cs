using AutoMapper;
using BLL.DTOs;
using BLL.Models;
using DLL.Repositories.Interfaces;
using Domain.Entities;

namespace BLL.Services;

public class FilmSessionService
{
    private readonly IFilmSessionRepository _filmSessionRepository;
    private readonly IMapper _mapper;

    public FilmSessionService(IFilmSessionRepository repository, IMapper mapper)
    {
        _filmSessionRepository = repository;
        _mapper = mapper;
    }

    public async Task<Result<string>> AddSession(FilmSessionDTO filmSession)
    {
        var result = new Result<string>();

        if (filmSession.StartsAt < DateTime.Now)
        {
            result.Succeed = false;
            result.Data = null;
            result.Errors.Add("Invalid start time. It must be in future.");
        }
        else
        {
            await _filmSessionRepository.AddAsync(_mapper.Map<FilmSession>(filmSession));
            result.Succeed = true;
        }

        return result;
    }

    public async Task<Result<string>> EditSession(int id, FilmSessionDTO filmSession)
    {
        var result = new Result<string>();

        if (filmSession.StartsAt < DateTime.Now)
        {
            result.Succeed = false;
            result.Data = null;
            result.Errors.Add("Invalid start time. It must be in future.");
        }
        else
        {
            await _filmSessionRepository.EditAsync(id, _mapper.Map<FilmSession>(filmSession));
            result.Succeed = true;
        }

        return result;
    }

    public void RemoveSession(int id)
    {
        _filmSessionRepository.Remove(id);
    }

    public async Task<Result<List<FilmSessionDTO>>> GetAllSessions()
    {
        var result = new Result<List<FilmSessionDTO>>();
        var filmSessions = _mapper.Map<List<FilmSessionDTO>>(await _filmSessionRepository.GetAllAsync());

        if (filmSessions == null || filmSessions.Count == 0)
        {
            result.Succeed = false;
            result.Errors.Add("No film sessions found.");
        }
        else
        {
            result.Succeed = true;
            result.Data = filmSessions;
        }

        return result;
    }
}