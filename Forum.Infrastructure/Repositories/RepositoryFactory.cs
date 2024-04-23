using Forum.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static ICommentRepository CreateCommentRepository(DatabaseContext context)
        {
            return new CommentRepository(context);
        }

        public static ITopicRepository CreateTopicRepository(DatabaseContext context)
        {
            return new TopicRepository(context);
        }
    }

}
