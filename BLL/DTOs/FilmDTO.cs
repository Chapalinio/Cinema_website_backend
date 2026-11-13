using Domain.Entities;

namespace BLL.DTOs;

public class FilmDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public string FilmUrl { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int LengthInMinutes { get; set; }

    public List<Comment>? Comments { get; set; }
}
