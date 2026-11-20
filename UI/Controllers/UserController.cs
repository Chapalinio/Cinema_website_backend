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
    private readonly AuthService _authService;
    private readonly UserRegisterRequestValidator _registerValidator;
    private readonly IMapper _mapper;
    public UserController(AuthService service, UserRegisterRequestValidator registerValidator, IMapper mapper)
    {
        _authService = service;
        _registerValidator = registerValidator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest user)
    {
        var validationResult = _registerValidator.Validate(user);

        if (validationResult.IsValid)
        {
            var registrationResult = await _authService.RegisterUserAsync(_mapper.Map<UserDTO>(user), user.Password);

            if (registrationResult.Succeed)
            {
                return Ok(registrationResult.Data);
            }
            else
            {
                return BadRequest(registrationResult.Errors);
            }
        }
        else
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
