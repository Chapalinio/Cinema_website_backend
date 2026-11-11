namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool Edited { get; set; }

    public int FilmId { get; set; }
    public Film Film { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
