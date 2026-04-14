using BlogApi.Data;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PostController(ApplicationDbContext context)
    {
        _context = context;
    }
   
    // Post
    // Get all posts
    [HttpGet]
    public ActionResult<List<Post>> GetAllPosts()
    {
        var posts = _context.Posts.ToList();
        return Ok(posts);
    }
    // Create post
    [HttpPost]
    public IActionResult CreatePost(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
        return Ok(post);
    }
    [HttpPut("{id}")]
    public IActionResult UpdatePost(int id, Post updatedPost)
    {
        
        var post=_context.Posts.FirstOrDefault(p=>p.Id==id);
        if (post==null)
        {
            return NotFound();
        }
        post.Title = updatedPost.Title;
        post.Content = updatedPost.Content;
        _context.SaveChanges();
        return Ok(post);
    }
    
    //Get post by id
    [HttpGet("{id}")]
    public ActionResult<Post> GetPostById(int id)
    {
        var post=_context.Posts.FirstOrDefault(p=>p.Id==id);
        if (post==null)
        {
            return NotFound();
        }
        return Ok(post);
    }
    
    // delete post by id
    [HttpDelete("{id}")]
    public IActionResult DeletePostById(int id)
    {
        var post=_context.Posts.FirstOrDefault(p=>p.Id==id);
        if (post==null)
        {
            return NotFound();
        }
        _context.Posts.Remove(post);
        _context.SaveChanges();
        return NoContent();
    }
        
    
 
}