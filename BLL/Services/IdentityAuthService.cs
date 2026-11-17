using AutoMapper;
using BLL.DTOs;
using BLL.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services;

public class IdentityAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public IdentityAuthService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<string>> Register(UserDTO userDTO, string password)
    {
        var user = _mapper.Map<User>(userDTO);
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return new Result<string>()
            {
                Success = true,
                Data = user.Id,
                Errors = []
            };
        }
        else
        {
            return new Result<string>()
            {
                Success = false,
                Data = null,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
    }

    public async Task<Result<string>> Login(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return new Result<string>()
            {
                Success = false,
                Data = null,
                Errors = ["Invalid login"]
            };
        }
        else
        {
            var passwordResult = await _userManager.CheckPasswordAsync(user, password);

            if (passwordResult)
            {
                return new Result<string>()
                {
                    Success = true,
                    Data = user.Id,
                    Errors = []
                };
            }
            else
            {
                return new Result<string>()
                {
                    Success = false,
                    Data = null,
                    Errors = ["Invalid password"]
                };
            }
        }
    }
}
