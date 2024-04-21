using Forum.Application.Dto;
using Forum.Application.Repositories;
using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class TopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<IEnumerable<TopicDto>> GetTopicsByStatusAsync(TopicStatus status)
        {
            var topics = await _topicRepository.GetTopicsByStatusAsync(status);
            return MapToDto(topics);
        }

        public async Task<IEnumerable<TopicDto>> GetTopicsByCreatorAsync(long creatorId)
        {
            var topics = await _topicRepository.GetTopicsByCreatorAsync(creatorId);
            return MapToDto(topics);
        }

        public async Task UpdateTopicStatusAsync(long topicId, TopicStatus status)
        {
            await _topicRepository.UpdateTopicStatusAsync(topicId, status);
        }

        public async Task IncrementLikesAsync(long topicId)
        {
            await _topicRepository.IncrementLikesAsync(topicId);
        }

        private IEnumerable<TopicDto> MapToDto(IEnumerable<Topic> topics)
        {
            foreach (var topic in topics)
            {
                yield return new TopicDto
                {
                    Id = topic.Id ?? default(long),
                    CreatorId = topic.CreatorId,
                    Subject = topic.Subject,
                    Status = topic.Status,
                    Likes = topic.Likes,
                    CommentsCount = topic.Comments?.Count ?? 0
                };
            }
        }
    }
}