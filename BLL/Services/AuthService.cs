using AutoMapper;
using BLL.DTOs;
using BLL.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public AuthService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<UserDTO>> RegisterUserAsync(UserDTO user, string password)
    {
        var responce = new Result<UserDTO>();
        var result = await _userManager.CreateAsync(_mapper.Map<User>(user), password);

        if (result.Succeeded)
        {
            var createdUser = await _userManager.FindByEmailAsync(user.Email);
            responce.Succeed = true;
            responce.Data = _mapper.Map<UserDTO>(createdUser);
            responce.Errors = [];
        }
        else
        {
            responce.Succeed = false;
            responce.Errors = result.Errors.Select(e => e.Description).ToList();
        }

        return responce;
    }

    public async Task<Result<UserDTO>> GetUserById(string id)
    {
        var responce = new Result<UserDTO>();
        var result = await _userManager.FindByIdAsync(id);

        if (result == null)
        {
            responce.Succeed = false;
            responce.Errors.Add("No such user.");
        }
        else
        {
            responce.Succeed = true;
            responce.Data = _mapper.Map<UserDTO>(result);
        }

        return responce;
    }
}
