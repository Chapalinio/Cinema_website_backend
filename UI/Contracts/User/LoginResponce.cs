namespace UI.Contracts.User;

public class LoginResponce
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
