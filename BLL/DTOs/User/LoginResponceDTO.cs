namespace BLL.DTOs.User;

public class LoginResponceDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
