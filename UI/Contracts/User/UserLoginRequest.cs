namespace UI.Contracts.User;

public class UserLoginRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
