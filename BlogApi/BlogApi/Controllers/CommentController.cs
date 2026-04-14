using Microsoft.AspNetCore.Mvc;
using BlogApi.Data;
using BlogApi.Models;

namespace BlogApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CommentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<Comment>> GetAllComments()
    {
        var comment=_context.Comments.ToList();
        return Ok(comment);
    }
    
    
    [HttpGet("{id}")]
    public ActionResult<Comment> GetCommentById(int id)
    {
        var comment=_context.Comments.FirstOrDefault(c=>c.Id==id);
        if (comment==null)
        {
            return NotFound();
        }
        return Ok(comment);
    }
    
    //get post-comments
    [HttpGet("post/{id}")]
    public ActionResult<List<Comment>> GetPostCommentsById(int id)
    {
        var postExist=_context.Posts.Any(p => p.Id == id);
        if (!postExist)
        {
            return NotFound();
        }
       var postComments=_context.Comments.Where(c => c.PostId == id).ToList();
       return Ok(postComments);
    }

    [HttpPost]
    public IActionResult CreateComment(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return Ok(comment);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCommentById(int id, Comment updatedComment)
    {
        var exstitngComment =_context.Comments.FirstOrDefault(c=>c.Id==id);
        if (exstitngComment==null)
        {
            return NotFound();
        }
        exstitngComment.Content = updatedComment.Content;
        _context.SaveChanges();
        return Ok(exstitngComment);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCommentById(int id)
    {
        var exstitngComment =_context.Comments.FirstOrDefault(c=>c.Id==id);
        if (exstitngComment==null)
        {
            return NotFound();
        }
        _context.Comments.Remove(exstitngComment);
        _context.SaveChanges();
        return NoContent();
    }
    
}