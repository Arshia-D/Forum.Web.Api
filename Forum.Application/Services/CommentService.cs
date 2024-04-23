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
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDto> AddNewCommentAsync(CommentDto commentDto)
        {
            var comment = new Comment
            {
                Text = commentDto.Text,
                Status = commentDto.Status,
                CreatorId = commentDto.CreatorId,              
                Likes = commentDto.Likes
            };

            // Direct assignment of TopicId is omitted; assuming it's set appropriately elsewhere or within repository logic

            var savedComment = await _commentRepository.AddNewCommentAsync(comment);
            return MapToDto(savedComment);
        }


        public async Task<CommentDto> AddReplyAsync(long topicId, CommentDto replyDto)
        {
            var reply = new Comment
            {
                Text = replyDto.Text,
                Status = replyDto.Status,
                CreatorId = replyDto.CreatorId,
                Likes = replyDto.Likes
                // TopicId and Topic setting is assumed to be handled as per domain logic
            };

            var savedReply = await _commentRepository.AddReplyAsync(topicId, reply, replyDto.CreatorId);
            return MapToDto(savedReply);
        }


        public async Task DeleteCommentAsync(long commentId)
        {
            await _commentRepository.DeleteCommentAsync(commentId);
        }

        public async Task<CommentDto> GetCommentByIdAsync(long commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            }
            return MapToDto(comment);
        }

        private CommentDto MapToDto(Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id ?? default(long),
                Text = comment.Text,
                Status = comment.Status,
                CreatorId = comment.CreatorId,
                TopicId = comment.Topic?.Id ?? 0,
                Likes = comment.Likes
            };
        }
    }
}