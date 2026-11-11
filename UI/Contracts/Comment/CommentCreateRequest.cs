namespace UI.Contracts.Comment;

public class CommentCreateRequest
{
    public string Text { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public int FilmId { get; set; }
    public int UserId { get; set; }
}
