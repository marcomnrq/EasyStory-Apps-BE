using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;

namespace EasyStory.API.Domain.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> ListAsync();
        Task<CommentResponse> GetByIdAsync(long id);
        Task<CommentResponse> GetByUserIdAndPostIdAsync(long userId, long postId);
        Task<IEnumerable<Comment>> ListByPostIdAsync(long postId);
        Task<IEnumerable<Comment>> ListByUserIdAsync(long userId);
        Task<CommentResponse> SaveAsync(Comment comment, long userId, long postId);
        Task<CommentResponse> UpdateAsync(long id, Comment comment);
        Task<CommentResponse> DeleteAsync(long id);
    }
}
