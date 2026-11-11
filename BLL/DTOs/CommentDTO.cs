using Domain.Entities;

namespace BLL.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool Edited { get; set; }
    public int FilmId { get; set; }
    public int UserId { get; set; }
}
