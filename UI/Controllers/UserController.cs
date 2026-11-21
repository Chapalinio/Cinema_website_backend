using AutoMapper;
using BLL.DTOs.User;
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
    private readonly UserLoginRequestValidator _loginValidator;
    private readonly IMapper _mapper;
    public UserController(AuthService service, UserRegisterRequestValidator registerValidator, IMapper mapper, UserLoginRequestValidator loginValidator)
    {
        _authService = service;
        _registerValidator = registerValidator;
        _mapper = mapper;
        _loginValidator = loginValidator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponce>> Register([FromBody] UserRegisterRequest user)
    {
        var validationResult = await _registerValidator.ValidateAsync(user);

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

    [HttpPost("login")]
    public async Task<ActionResult<bool>> Login([FromBody] UserLoginRequest req)
    {
        var validationResult = await _loginValidator.ValidateAsync(req);

        if (validationResult.IsValid)
        {
            var responce = await _authService.LoginAsync(_mapper.Map<UserLoginDTO>(req));

            if (responce.Succeed)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
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
        var responce = await _authService.GetUserByIdAsync(id);

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
