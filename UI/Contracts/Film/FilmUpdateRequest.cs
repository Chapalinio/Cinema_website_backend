namespace UI.Contracts.Film;

public class FilmUpdateRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public string FilmUrl { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int LengthInMinutes { get; set; }
}
