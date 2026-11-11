using UI.Contracts.Comment;

namespace UI.Contracts.User;

public class UserResponce
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int Age { get; set; }
    public List<CommentResponse> Comments { get; set; }
}
