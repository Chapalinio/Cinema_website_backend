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

    public async Task<Result<string>> RegisterUserAsync(UserDTO user, string password)
    {
        var res = new Result<string>();
        var result = await _userManager.CreateAsync(_mapper.Map<User>(user), password);

        if (result.Succeeded)
        {
            res.Succeed = true;
            res.Data = "User successfully registrated";
            res.Errors = [];
        }
        else
        {
            res.Succeed = false;
            res.Errors = result.Errors.Select(e => e.Description).ToList();
        }

        return res;
    }
}
