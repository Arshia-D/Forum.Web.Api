using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.Application.Repositories;
using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Repositories
{
    internal class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Topic>> GetTopicsByStatusAsync(TopicStatus status)
        {
            return await Query.Where(t => t.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Topic>> GetTopicsByCreatorAsync(long creatorId)
        {
            return await Query.Where(t => t.CreatorId == creatorId).ToListAsync();
        }

        public async Task UpdateTopicStatusAsync(long topicId, TopicStatus status)
        {
            var topic = await Query.FirstOrDefaultAsync(t => t.Id == topicId);
            if (topic != null)
            {
                topic.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task IncrementLikesAsync(long topicId)
        {
            var topic = await Query.FirstOrDefaultAsync(t => t.Id == topicId);
            if (topic != null)
            {
                topic.Likes += 1;
                await _context.SaveChangesAsync();
            }
        }
    }
}