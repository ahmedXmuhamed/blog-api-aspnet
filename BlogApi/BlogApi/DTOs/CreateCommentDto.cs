namespace BlogApi.DTOs;

public class CreateCommentDto
{
    public string Content { get; set; }= string.Empty;
    public int PostId { get; set; }
    public int UserId { get; set; }
}