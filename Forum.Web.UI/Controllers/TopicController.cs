using Forum.Application.Dto;
using Forum.Application.Services;
using Forum.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly TopicService _topicService;

        public TopicController(TopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("ByStatus/{status}")]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetTopicsByStatus(TopicStatus status)
        {
            var topics = await _topicService.GetTopicsByStatusAsync(status);
            if (topics == null)
            {
                return NotFound("Topics not found.");
            }
            return Ok(topics);
        }

        [HttpGet("ByCreator/{creatorId}")]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetTopicsByCreator(long creatorId)
        {
            var topics = await _topicService.GetTopicsByCreatorAsync(creatorId);
            if (topics == null)
            {
                return NotFound("Topics not found.");
            }
            return Ok(topics);
        }

        [HttpPut("{topicId}/UpdateStatus/{status}")]
        public async Task<IActionResult> UpdateTopicStatus(long topicId, TopicStatus status)
        {
            await _topicService.UpdateTopicStatusAsync(topicId, status);
            return NoContent(); // No content to return after updating
        }

        [HttpPost("{topicId}/IncrementLikes")]
        public async Task<IActionResult> IncrementLikes(long topicId)
        {
            await _topicService.IncrementLikesAsync(topicId);
            return NoContent(); // No content to return after incrementing
        }
    }
}
