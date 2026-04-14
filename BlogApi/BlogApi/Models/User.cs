namespace BlogApi.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }=String.Empty;
    public string Email { get; set; }=string.Empty;

    public List<Post> Post { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();

}