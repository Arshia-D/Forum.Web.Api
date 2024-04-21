using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dto
{
    public class TopicDto
    {
        public long Id { get; set; }
        public long? CreatorId { get; set; }
        public string? Subject { get; set; }
        public TopicStatus Status { get; set; }
        public int Likes { get; set; }
        public int CommentsCount { get; set; }
    }
}
