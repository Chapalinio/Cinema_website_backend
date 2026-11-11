using UI.Contracts.Comment;

namespace UI.Contracts.Film;

public class FilmResponce
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public string FilmUrl { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int LengthInMinutes { get; set; }

    public List<CommentResponse>? Comments { get; set; }
}
