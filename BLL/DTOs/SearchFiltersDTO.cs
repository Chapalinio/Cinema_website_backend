namespace BLL.DTOs;

public class SearchFiltersDTO
{
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int ReleaseYear { get; set; } = 0;
    public string Country { get; set; } = string.Empty;
    public string KeyWord { get; set; } = string.Empty;
}
