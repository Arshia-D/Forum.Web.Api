using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Repositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<Comment> AddNewCommentAsync(Comment comment);
        Task<Comment> AddReplyAsync(long topicId, Comment reply, long? creatorId);
        Task DeleteCommentAsync(long commentId);
    }
}
