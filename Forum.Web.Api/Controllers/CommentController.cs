using Forum.Application.Dto;
using Forum.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> AddNewCommentAsync([FromBody] CommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _commentService.AddNewCommentAsync(commentDto);
            return CreatedAtAction(nameof(GetCommentByIdAsync), new { commentId = result.Id }, result);
        }

        [HttpPost("reply/{topicId}")]
        public async Task<ActionResult<CommentDto>> AddReplyAsync(long topicId, [FromBody] CommentDto replyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _commentService.AddReplyAsync(topicId, replyDto);
            return CreatedAtAction(nameof(GetCommentByIdAsync), new { commentId = result.Id }, result);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(long commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);
            return NoContent();
        }

        // Placeholder for GetCommentByIdAsync method
        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentDto>> GetCommentByIdAsync(long commentId)
        {
            var commentDto = await _commentService.GetCommentByIdAsync(commentId);
            if (commentDto == null)
            {
                return NotFound($"Comment with ID {commentId} not found.");
            }
            return Ok(commentDto);
        }
    }
}
