using BLL.Services;
using UI.Validators.Film;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UI.Contracts.Film;
using BLL.DTOs;

namespace UI.Controllers
{
    [Route("api/films")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService;
        private readonly FilmCreateRequestValidator _createValidator;
        private readonly FilmUpdateRequestValidator _updateValidator;
        private readonly IMapper _mapper;
        public FilmController(FilmService service, FilmCreateRequestValidator createValidator, FilmUpdateRequestValidator updateValidator, IMapper mapper)
        {
            _filmService = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public ActionResult<List<FilmResponce>> GetAllFilms()
        {
            var allFilms = _filmService.GetAll();

            if (allFilms.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<List<FilmResponce>>(allFilms));
        }

        [HttpGet("{id:int}")]
        public ActionResult<FilmResponce> GetFilmById([FromRoute] int id)
        {
            var film = _filmService.GetById(id);

            if (film == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FilmResponce>(film));
        }

        [HttpPost("add")]
        public IActionResult AddFilm([FromBody] FilmCreateRequest film)
        {
            if (!_createValidator.Validate(film).IsValid)
            {
                return BadRequest(_createValidator.Validate(film).Errors);
            }

            _filmService.Add(_mapper.Map<FilmDTO>(film));
            return Created();
        }

        [HttpPut("edit/{id:int}")]
        public IActionResult EditFilm([FromRoute] int id, [FromBody] FilmUpdateRequest film)
        {
            if (!_updateValidator.Validate(film).IsValid)
            {
                return BadRequest(_updateValidator.Validate(film).Errors);
            }

            _filmService.Edit(id, _mapper.Map<FilmDTO>(film));
            return NoContent();
        }

        [HttpDelete("remove/{id:int}")]
        public IActionResult RemoveFilm([FromRoute] int id)
        {
            _filmService.Remove(id);
            return NoContent();
        }
    }
}
