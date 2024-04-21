using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Repositories
{
    public interface ITopicRepository : IBaseRepository<Topic>
    {
        Task<IEnumerable<Topic>> GetTopicsByStatusAsync(TopicStatus status);
        Task<IEnumerable<Topic>> GetTopicsByCreatorAsync(long creatorId);
        Task UpdateTopicStatusAsync(long topicId, TopicStatus status);
        Task IncrementLikesAsync(long topicId);
    }
}
