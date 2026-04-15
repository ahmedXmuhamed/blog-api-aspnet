using Microsoft.AspNetCore.Mvc;
using BlogApi.Data;
using BlogApi.Models;
using BlogApi.DTOs;
namespace BlogApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }
    // Get all users
    [HttpGet]
    public ActionResult<List<UserResponseDto>> GetAllUsers()
    {
        var users = _context.Users.Select(
            u=>new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }
            ).ToList();
        if (users.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(users);
    }
    
    // Create user
    [HttpPost]
    public IActionResult CreateUser(CreateUserDto userDto)
    {
        User user = new User()
        {
            Name = userDto.Name,
            Email=userDto.Email
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        UserResponseDto response = new UserResponseDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
        return Ok(response);
    }
    
    //Get user posts
    //GET /api/users/{id}/posts
    [HttpGet("{id}/posts")]
    //[Route("{id}/posts")]
    public ActionResult<List<PostResponseDto>> GetUserPostsById(int id)
    {
        var userExist=_context.Users.Any(u => u.Id == id);
        if (!userExist)
        {
            return NotFound();
        }
        var userPosts = _context.Posts.Where(p => p.UserId == id).Select(
            p=>new PostResponseDto
            {
                Id= p.Id,
                Title=p.Title,
                Content = p.Content
            }
            ).ToList();
        return Ok(userPosts);
    }
    
    //get user comments
    //GET /api/users/{id}/comments
    [HttpGet("{id}/comments")]
    //[Route("{id}/comments")]
    public ActionResult<List<CommentResponseDto>> GetUserCommentsById(int id)
    {
        var userExist=_context.Users.Any(u => u.Id == id);
        if (!userExist)
        {
            return NotFound();
        }
        var userComments=_context.Comments.Where(c => c.UserId == id).Select
        (
            c=>new CommentResponseDto
            {
                Id=c.Id,
                Content=c.Content
            }
        )
            .ToList();
        return Ok(userComments);
    }
}