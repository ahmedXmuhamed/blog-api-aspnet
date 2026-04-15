    using Microsoft.AspNetCore.Mvc;
    using BlogApi.Data;
    using BlogApi.DTOs;
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
        public ActionResult<List<CommentResponseDto>> GetAllComments()
        {
            var comment=_context.Comments.Select(
                c=>new CommentResponseDto
                {
                    Id = c.Id,
                    Content = c.Content
                }
                ).ToList();
            return Ok(comment);
        }
        
        
        [HttpGet("{id}")]
        public ActionResult<CommentResponseDto> GetCommentById(int id)
        {
            var comment=_context.Comments.FirstOrDefault(c=>c.Id==id);
            if (comment==null)
            {
                return NotFound();
            }
            CommentResponseDto response = new CommentResponseDto()
            {
                Id = comment.Id,
                Content = comment.Content
            };
            return Ok(response);
        }
        
        //get post-comments
        [HttpGet("post/{id}")]
        public ActionResult<List<CommentResponseDto>> GetPostCommentsById(int id)
        {
            
            var postComments=_context.Comments.Where(c => c.PostId == id).Select(
                c=>new CommentResponseDto
                {
                    Id = c.Id,
                    Content = c.Content
                }
            ).ToList();
            if (postComments.Count == 0)
            {
                return NotFound();
            }
            return Ok(postComments);
        }

        [HttpPost]
        public IActionResult CreateComment(CreateCommentDto commentDto)
        {
            Comment comment = new Comment()
            {
                Content = commentDto.Content,
                PostId = commentDto.PostId,
                UserId = commentDto.UserId,
                CreatedAt = DateTime.UtcNow
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();
            CommentResponseDto response = new CommentResponseDto()
            {
                Id = comment.Id,
                Content = comment.Content
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCommentById(int id, UpdateCommentDto updatedCommentDto)
        {
            var exstitngComment =_context.Comments.FirstOrDefault(c=>c.Id==id);
            if (exstitngComment==null)
            {
                return NotFound();
            }
            exstitngComment.Content = updatedCommentDto.Content;
            _context.SaveChanges();
            CommentResponseDto response = new CommentResponseDto()
            {
                Id = exstitngComment.Id,
                Content = exstitngComment.Content
            };
            return Ok(response);
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