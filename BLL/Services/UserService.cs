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

    public async Task RegisterAsync(UserDTO user)
    {
        await _userRepository.AddAsync(_mapper.Map<User>(user));
    }

    public async Task<bool> LoginAsync(UserDTO user)
    {
        return await _userRepository.LoginAsync(user.Login, user.Password);
    }

    public async Task<int?> GetIdByLoginAsync(string login)
    {
        return await _userRepository.GetIdByLoginAsync(login);
    }
}
