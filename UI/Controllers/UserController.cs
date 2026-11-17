using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UI.Contracts.User;
using UI.Validators.User;

namespace UI.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IdentityAuthService _authService;
    private readonly UserLoginRequestValidator _loginValidator;
    private readonly UserRegisterRequestValidator _registerValidator;
    private readonly IMapper _mapper;
    public UserController(IdentityAuthService authService, UserLoginRequestValidator loginValidator, UserRegisterRequestValidator registerValidator, IMapper mapper)
    {
        _authService = authService;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> Register([FromBody] UserRegisterRequest user)
    {
        var validationResult = _registerValidator.Validate(user);
        var registerResult = await _authService.Register(_mapper.Map<UserDTO>(user), user.Password);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        if (!registerResult.Success)
        {
            return BadRequest(registerResult.Errors);
        }

        return Ok(registerResult.Data);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] UserLoginRequest user)
    {
        var result = await _authService.Login(user.Login, user.Password);

        if (result.Success)
        {
            return Ok(result.Data);
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }
}
