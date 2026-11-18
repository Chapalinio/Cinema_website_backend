using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UI.Contracts.FilmSession;
using UI.Validators.FilmSession;

namespace UI.Controllers
{
    [Route("api/sessions")]
    [ApiController]
    public class FilmSessionController : ControllerBase
    {
        private readonly FilmSessionService _filmSessionService;
        private readonly IMapper _mapper;
        private readonly FilmSessionCreateRequestValidator _filmSessionCreateValidator;
        private readonly FilmSessionUpdateRequestValidator _filmSessionUpdateValidator;

        public FilmSessionController(FilmSessionService service, IMapper mapper, FilmSessionUpdateRequestValidator updateValidator, FilmSessionCreateRequestValidator createValidator)
        {
            _filmSessionService = service;
            _mapper = mapper;
            _filmSessionCreateValidator = createValidator;
            _filmSessionUpdateValidator = updateValidator;
        }


        [HttpPost("add-sesion")]
        public async Task<IActionResult> AddSessionAsync([FromBody] FilmSessionCreateRequest req)
        {
            var validationResult = await _filmSessionCreateValidator.ValidateAsync(req);
            var serviceResult = await _filmSessionService.AddSessionAsync(_mapper.Map<FilmSessionDTO>(req));

            if (validationResult.IsValid)
            {
                if (serviceResult.Succeed)
                {
                    return Created();
                }
                else
                {
                    return BadRequest(serviceResult.Errors);
                }
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }

        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> EditSessionAsync([FromRoute] int id, [FromBody] FilmSessionUpdateRequest req)
        {
            var validationResult = await _filmSessionUpdateValidator.ValidateAsync(req);
            var serviceResult = await _filmSessionService.EditSessionAsync(id, _mapper.Map<FilmSessionDTO>(req));

            if (validationResult.IsValid)
            {
                if (serviceResult.Succeed)
                {
                    return Ok(serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult.Errors);
                }
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }

        [HttpDelete("remove/{id:int}")]
        public IActionResult RemoveSession([FromRoute] int id)
        {
            _filmSessionService.RemoveSession(id);
            return NoContent();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<FilmSessionResponce>>> GetAllSessionsAsync()
        {
            var result = await _filmSessionService.GetAllSessionsAsync();

            if (result.Succeed)
            {
                return Ok(_mapper.Map<List<FilmSessionResponce>>(result.Data));
            }
            else
            {
                return NotFound(result.Errors);
            }
        }
    }
}
