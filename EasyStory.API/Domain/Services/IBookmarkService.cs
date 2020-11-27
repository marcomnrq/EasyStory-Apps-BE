using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;

namespace EasyStory.API.Domain.Services
{
    public interface IBookmarkService
    {
        Task<IEnumerable<Bookmark>> ListAsync();
        Task<IEnumerable<Bookmark>> ListByUserIdAsync(long userId);
        Task<IEnumerable<Bookmark>> ListByPostIdAsync(long postId);
        Task<BookmarkResponse> GetByUserIdAndPostIdAsync(long userId, long postId);
        Task<BookmarkResponse> AssignUserPostAsync(long userId, long postId);
        Task<BookmarkResponse> UnassignUserPostAsync(long userId, long postId);
    }
}