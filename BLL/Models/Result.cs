namespace BLL.Models;

public class Result<T>
{
    public bool Succeed { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = [];
}