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

    public void Create(CommentDTO comment)
    {
        Comment newComment = _mapper.Map<Comment>(comment);
        newComment.CreatedAt = DateTime.Now;
        _commentRepository.Add(newComment);
    }

    public void Edit(int id, CommentDTO comment)
    {
        _commentRepository.Edit(id, _mapper.Map<Comment>(comment));
    }
    
    public void Remove(int id)
    {
        _commentRepository.Remove(id);
    }

    public List<CommentDTO> GetAllByFilmTitle(string title)
    {
        var comments = _commentRepository.GetAll();
        
        if(comments == null)
        {
            return [];
        }

        var responce = comments.Where(c => c.Film != null && c.Film.Title == title).ToList();

        return _mapper.Map<List<CommentDTO>>(responce);
    }
}
