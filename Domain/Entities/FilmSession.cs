namespace Domain.Entities;

public class FilmSession
{
    public int Id { get; set; }
    public string HallName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
    public DateTime StartsAt { get; set; }

    public int FilmId { get; set; }
    public Film Film { get; set; }
}
