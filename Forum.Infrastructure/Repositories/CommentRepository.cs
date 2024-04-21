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
    internal class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Comment> AddNewCommentAsync(Comment comment)
        {      
            return await SaveAsync(comment);
        }

        public async Task<Comment> AddReplyAsync(long topicId, Comment reply, long? creatorId)
        {
            // Find the topic to ensure it exists
            var topic = await _context.Topics.FindAsync(topicId);
            if (topic == null)
            {
                throw new KeyNotFoundException("Topic not found.");
            }

            reply.Topic = topic;
            reply.CreatorId = creatorId; // Set the creatorId if provided

            // Save the reply comment
            return await SaveAsync(reply);
        }

        public async Task DeleteCommentAsync(long commentId)
        {
            var comment = await GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found.");
            }

            await DeleteAsync(comment);
        }
    }
}