using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;

namespace EasyStory.API.Domain.Repositories
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<Bookmark>> ListByUserIdAsync(long userId);
        Task<IEnumerable<Bookmark>> ListByPostIdAsync(long postId);
        Task<Bookmark> FindByUserIdAndPostId(long userId, long postId);
        Task AddAsync(Bookmark bookmark);
        Task<IEnumerable<Bookmark>> ListAsync();
        void Remove(Bookmark bookmark);
        Task AssignBookmark(long userId, long postId);
        void UnassignBookmark(long userId, long postId);
    }
}