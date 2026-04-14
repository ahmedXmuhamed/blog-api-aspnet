using Microsoft.AspNetCore.Mvc;
using BlogApi.Data;
using BlogApi.Models;

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
    public ActionResult<List<User>> GetAllUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }
    
    // Create user
    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(user);
    }
    
    //Get user posts
    //GET /api/users/{id}/posts
    [HttpGet("{id}/posts")]
    //[Route("{id}/posts")]
    public ActionResult<List<Post>> GetUserPostsById(int id)
    {
        var userExist=_context.Users.Any(u => u.Id == id);
        if (!userExist)
        {
            return NotFound();
        }
        var userPosts = _context.Posts.Where(p => p.UserId == id).ToList();
        return Ok(userPosts);
    }
    
    //get user comments
    //GET /api/users/{id}/comments
    [HttpGet("{id}/comments")]
    //[Route("{id}/comments")]
    public ActionResult<List<Comment>> GetUserCommentsById(int id)
    {
        var userExist=_context.Users.Any(u => u.Id == id);
        if (!userExist)
        {
            return NotFound();
        }
        var userComments=_context.Comments.Where(c => c.UserId == id).ToList();
        return Ok(userComments);
    }
}