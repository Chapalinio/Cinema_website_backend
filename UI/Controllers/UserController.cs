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
    public async Task<ActionResult<UserResponce>> Register([FromBody] UserRegisterRequest user)
    {
        var validationResult = _registerValidator.Validate(user);

        if (validationResult.IsValid)
        {
            var responce = await _authService.RegisterUserAsync(_mapper.Map<UserDTO>(user), user.Password);

            if (responce.Succeed)
            {
                return Ok(_mapper.Map<UserResponce>(responce.Data));
            }
            else
            {
                return BadRequest(responce.Errors);
            }
        }
        else
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }
    }

    [HttpGet("get-profile/{id}")]
    public async Task<ActionResult<UserProfileResponce>> GetUserProfile([FromRoute] string id)
    {
        var responce = await _authService.GetUserById(id);

        if (responce.Succeed)
        {
            return Ok(_mapper.Map<UserProfileResponce>(responce.Data));
        }
        else
        {
            return NotFound(responce.Errors);
        }
    }
}
