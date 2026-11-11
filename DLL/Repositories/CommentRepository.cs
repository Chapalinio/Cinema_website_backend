using DLL.Context;
using DLL.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories;

public class CommentRepository : IRepository<Comment>
{
    private readonly AppDbContext _appContext;
    public CommentRepository(AppDbContext context)
    {
        _appContext = context;
    }

    public void Add(Comment comment)
    {
        _appContext.Comments.Add(comment);
        SaveChanges();
    }

    public void Edit(int id, Comment comment)
    {
        var commentToEdit = GetById(id);
        
        if(commentToEdit != null)
        {
            commentToEdit.Text = comment.Text;
            commentToEdit.Edited = true;
            SaveChanges();
        }
    }

    public void Remove(int id)
    {
        var commentToDelete = GetById(id);
        
        if(commentToDelete != null)
        {
            _appContext.Comments.Remove(commentToDelete);
            SaveChanges();
        }
    }

    public List<Comment>? GetAll()
    {
        return _appContext.Comments.Include(c => c.Film).ToList();
    }

    public Comment? GetById(int id)
    {
        return _appContext.Comments.FirstOrDefault(c => c.Id == id);
    }

    public void SaveChanges()
    {
        _appContext.SaveChanges();
    }
}
