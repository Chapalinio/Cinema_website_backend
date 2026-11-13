using AutoMapper;
using BLL.DTOs;
using DLL.Repositories.Interfaces;
using Domain.Entities;
namespace BLL.Services;

public class CommentService
{
    private readonly IRepository<Comment> _commentRepository;
    private readonly IMapper _mapper;
    public CommentService(IRepository<Comment> repository, IMapper mapper)
    {
        _commentRepository = repository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CommentDTO comment)
    {
        Comment newComment = _mapper.Map<Comment>(comment);
        newComment.CreatedAt = DateTime.Now;
        await _commentRepository.AddAsync(newComment);
    }

    public async Task EditAsync(int id, CommentDTO comment)
    {
        await _commentRepository.EditAsync(id, _mapper.Map<Comment>(comment));
    }

    public void Remove(int id)
    {
        _commentRepository.Remove(id);
    }

    public async Task<List<CommentDTO>> GetAllByFilmTitleAsync(string title)
    {
        var comments = await _commentRepository.GetAllAsync();

        if (comments == null)
        {
            return [];
        }

        var responce = comments.Where(c => c.Film != null && c.Film.Title == title);

        return _mapper.Map<List<CommentDTO>>(responce);
    }
}
