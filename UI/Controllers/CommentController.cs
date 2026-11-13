using UI.Contracts.Comment;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UI.Validators.Comment;
using AutoMapper;
using BLL.DTOs;

namespace UI.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;
    private readonly CommentCreateValidator _commentCreateValidator;
    private readonly IMapper _mapper;

    public CommentController(CommentService service, CommentCreateValidator validator, IMapper mapper)
    {
        _commentService = service;
        _commentCreateValidator = validator;
        _mapper = mapper;
    }

    [HttpGet("{title}")]
    public async Task<ActionResult<List<CommentResponse>>> GetCommentsByFilmTitleAsync([FromRoute] string title)
    {
        var comments = _mapper.Map<List<CommentResponse>>(await _commentService.GetAllByFilmTitleAsync(title));
        
        if(comments.Count == 0)
        {
            return NotFound();
        }

        return Ok(comments);
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateCommentAsync([FromBody] CommentCreateRequest comment)
    {
        var result = await _commentCreateValidator.ValidateAsync(comment);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        await _commentService.CreateAsync(_mapper.Map<CommentDTO>(comment));
        return Created();
    }

    [HttpPut("edit/{id:int}")]
    public async Task<IActionResult> EditCommentAsync([FromRoute] int id, [FromBody] CommentCreateRequest comment)
    {
        await _commentService.EditAsync(id, _mapper.Map<CommentDTO>(comment));
        return NoContent();
    }

    [HttpDelete("remove/{id:int}")]
    public IActionResult DeleteComment([FromRoute] int id)
    {
        _commentService.Remove(id);
        return NoContent();
    }
}
