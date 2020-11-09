using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> ListAsync();
        Task<IEnumerable<Post>> ListByUserIdAsync(long userId);
        Task<IEnumerable<Post>> ListByReaderIdAsync(long readerId);
        Task<IEnumerable<Post>> ListByHashtagIdAsync(long hashtagId);
        Task<PostResponse> GetByIdAsync(long id);
        Task<PostResponse> SavePostAsync(Post post, long userId);
        Task<PostResponse> UpdatePostAsync(long id, Post post);
        Task<PostResponse> DeletePostAsync(long id);
    }
}
