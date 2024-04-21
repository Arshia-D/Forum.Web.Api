using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Dto
{
    public class CommentDto
    {
        public long Id { get; set; }
        public string? Text { get; set; }
        public CommentStatus Status { get; set; }
        public long? CreatorId { get; set; }
        public long TopicId { get; set; } 
        public int Likes { get; set; }
    }
}
