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

    public async Task AddAsync(Comment comment)
    {
        await _appContext.Comments.AddAsync(comment);
        await SaveChangesAsync();
    }

    public async Task EditAsync(int id, Comment comment)
    {
        var commentToEdit = await GetByIdAsync(id);

        if (commentToEdit != null)
        {
            commentToEdit.Text = comment.Text;
            commentToEdit.Edited = true;
            await SaveChangesAsync();
        }
    }

    public async void Remove(int id)
    {
        var commentToDelete = await GetByIdAsync(id);

        if (commentToDelete != null)
        {
            _appContext.Comments.Remove(commentToDelete);
            await SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Comment>?> GetAllAsync()
    {
        return await _appContext.Comments.Include(c => c.Film).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return _appContext.Comments.FirstOrDefault(c => c.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _appContext.SaveChangesAsync();
    }
}
