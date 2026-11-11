using BLL.Services;
using FluentValidation.Results;
using UI.Validators.User;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL.DTOs;
using UI.Contracts.User;

namespace UI.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly UserLoginRequestValidator _loginValidator;
    private readonly UserRegisterRequestValidator _registerValidator;
    private readonly IMapper _mapper;
    public UserController(UserService service, UserLoginRequestValidator loginValidator, UserRegisterRequestValidator registerValidator, IMapper mapper)
    {
        _userService = service;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterRequest user)
    {
        var result = _registerValidator.Validate(user);

        if (result.IsValid)
        {
            _userService.Register(_mapper.Map<UserDTO>(user));
            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginRequest user)
    {
        ValidationResult result = _loginValidator.Validate(user);
        if (result.IsValid && _userService.Login(_mapper.Map<UserDTO>(user)))
        {
            return Ok();
        }

        if(result.Errors.Count > 0)
        {
            return BadRequest(result.Errors);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{login}")]
    public ActionResult<int> GetUserIdByLogin([FromRoute] string login)
    {
        return Ok(_userService.GetIdByLogin(login));
    }
}
