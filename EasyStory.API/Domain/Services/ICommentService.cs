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
        //Task<IEnumerable<Comment>> ListByPostIdAsync(int postId);
        //Task<IEnumerable<Comment>> ListByUserIdAsync(int userId);
        Task<CommentResponse> SaveAsync(Comment comment);
        Task<CommentResponse> UpdateAsync(long id, Comment comment);
        Task<CommentResponse> DeleteAsync(long id);
    }
}
