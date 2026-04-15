namespace BlogApi.DTOs;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }=String.Empty;
    public string Email { get; set; }=string.Empty;
}