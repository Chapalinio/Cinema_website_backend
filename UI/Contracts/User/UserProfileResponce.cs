namespace UI.Contracts.User;

public class UserProfileResponce
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string FavouriteGenre { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int Age { get; set; }
}
