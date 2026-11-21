using AutoMapper;
using BLL.DTOs.User;
using BLL.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    public AuthService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
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

    public async Task<Result<string>> LoginAsync(UserLoginDTO user)
    {
        var responce = new Result<string>();

        var userToLogin = await _userManager.FindByEmailAsync(user.Login);

        if (userToLogin == null)
        {
            userToLogin = await _userManager.FindByNameAsync(user.Login);
        }

        if (userToLogin == null)
        {
            responce.Succeed = false;
            responce.Errors.Add("No user with such login.");
        }
        else
        {
            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(userToLogin, user.Password, false);

            if (passwordCheck.Succeeded)
            {
                responce.Succeed = true;
                responce.Data = "User successfully logged in.";
            }
            else
            {
                responce.Succeed = false;
                responce.Errors.Add("Invalid password.");
            }
        }

        return responce;
    }

    public async Task<Result<UserDTO>> GetUserByIdAsync(string id)
    {
        var responce = new Result<UserDTO>();
        var result = await _userManager.FindByIdAsync(id);

        if (result == null)
        {
            responce.Succeed = false;
            responce.Errors.Add("No user with such id.");
        }
        else
        {
            responce.Succeed = true;
            responce.Data = _mapper.Map<UserDTO>(result);
        }

        return responce;
    }
}
