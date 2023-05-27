using Microsoft.AspNetCore.Mvc;
using blogApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Comment = blogApi.Models.Comment;

namespace blogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly WebApiContext _context;

        public CommentsController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        [Route("GetPoemComments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPoem
            ([FromQuery] int idPoem)
        {
            return await _context.Comment.Where(x => x.IdPoem == idPoem).ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.IdComment)
            {
                return BadRequest();
            }
            _context.Entry(comment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // CREATE: api/addComment
        [HttpPost]
        [Route("addComment")]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetComment", new { id = comment.IdComment }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }


        private bool CommentExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
