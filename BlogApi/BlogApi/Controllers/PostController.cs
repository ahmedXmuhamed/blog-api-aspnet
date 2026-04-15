using BlogApi.Data;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlogApi.Controllers;
using BlogApi.DTOs;

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
    public ActionResult<List<PostResponseDto>> GetAllPosts()
    {
        var posts = _context.Posts.Select(
           p=>new PostResponseDto
           {
               Id = p.Id,
               Title = p.Title,
               Content = p.Content
           }
            ).ToList();
        return Ok(posts);
    }
    // Create post
    [HttpPost]
    public IActionResult CreatePost(CreatePostDto postDto)
    {
        Post post = new Post()
        {
            Title = postDto.Title,
            Content = postDto.Content,
            UserId = postDto.UserId,
            CreatedAt = DateTime.UtcNow
        };
        _context.Posts.Add(post);
        _context.SaveChanges();
        PostResponseDto response = new PostResponseDto()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content
        };
        return Ok(response);
    }
    [HttpPut("{id}")]
    public IActionResult UpdatePost(int id,UpdatePostDto updatedPostDto)
    {
        
        var post=_context.Posts.FirstOrDefault(p=>p.Id==id);
        if (post==null)
        {
            return NotFound();
        }
        
        post.Title = updatedPostDto.Title;
        post.Content = updatedPostDto.Content;
        post.UpdatedAt = DateTime.UtcNow;
        _context.SaveChanges();
        PostResponseDto response = new PostResponseDto()
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content
        };
        return Ok(response);
    }
    
    //Get post by id
    [HttpGet("{id}")]
    public ActionResult<PostResponseDto> GetPostById(int id)
    {
        var post=_context.Posts.Where(p=>p.Id ==id).Select(
            p=> new PostResponseDto()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content
            }
            ).FirstOrDefault();
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