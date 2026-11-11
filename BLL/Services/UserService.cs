using AutoMapper;
using BLL.DTOs;
using DLL.Repositories.Interfaces;
using Domain.Entities;

namespace BLL.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository repository, IMapper mapper)
    {
        _userRepository = repository;
        _mapper = mapper;
    }

    public void Register(UserDTO user)
    {
        _userRepository.Add(_mapper.Map<User>(user));
    }

    public bool Login(UserDTO user)
    {
        return _userRepository.Login(user.Login, user.Password);
    }

    public int? GetIdByLogin(string login)
    {
        return _userRepository.GetIdByLogin(login);
    }
}
