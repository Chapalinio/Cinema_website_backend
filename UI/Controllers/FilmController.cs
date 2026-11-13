using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UI.Contracts.Common;
using UI.Contracts.Film;
using UI.Validators.Film;

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
        public async Task<ActionResult<List<FilmResponce>>> GetAllFilmsAsync()
        {
            var allFilms = await _filmService.GetAllAsync();

            if (allFilms.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<FilmResponce>>(allFilms));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FilmResponce>> GetFilmByIdAsync([FromRoute] int id)
        {
            var film = await _filmService.GetByIdAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FilmResponce>(film));
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<FilmResponce>>> SearchFilmsByFilters([FromBody] SetFiltersRequest filters)
        {
            return _mapper.Map<List<FilmResponce>>(await _filmService.SearchFilmByFiltersAsync(_mapper.Map<SearchFiltersDTO>(filters)));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFilmAsync([FromBody] FilmCreateRequest film)
        {
            if (!_createValidator.Validate(film).IsValid)
            {
                return BadRequest(_createValidator.Validate(film).Errors);
            }

            await _filmService.AddAsync(_mapper.Map<FilmDTO>(film));
            return Created();
        }

        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> EditFilmAsync([FromRoute] int id, [FromBody] FilmUpdateRequest film)
        {
            if (!_updateValidator.Validate(film).IsValid)
            {
                return BadRequest(_updateValidator.Validate(film).Errors);
            }

            await _filmService.EditAsync(id, _mapper.Map<FilmDTO>(film));
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
