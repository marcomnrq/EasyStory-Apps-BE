using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Persistence.Contexts;
using EasyStory.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyStory.API.Persistence.Repositories
{
    public class BookmarkRepository : BaseRepository, IBookmarkRepository
    {
        public BookmarkRepository(AppDbContext context) : base(context)
        {
            
        }

        public async Task AddAsync(Bookmark bookmark)
        {
            await _context.Bookmarks.AddAsync(bookmark);
        }

        public async Task AssignBookmark(long userId, long postId)
        {
            Bookmark bookmark = await FindByUserIdAndPostId(userId, postId);
            if (bookmark == null)
            {
                bookmark = new Bookmark { UserId = userId, PostId = postId };
                await AddAsync(bookmark);
            }
        }

        public async Task<Bookmark> FindByUserIdAndPostId(long userId, long postId)
        {
            return await _context.Bookmarks.FindAsync(userId, postId);
        }

        public async Task<IEnumerable<Bookmark>> ListByUserIdAsync(long userId)
        {
            return await _context.Bookmarks
                .Where(p => p.UserId == userId)
                .Include(p => p.Post)
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bookmark>> ListByPostIdAsync(long postId)
        {
            return await _context.Bookmarks
                .Where(p => p.PostId == postId)
                .Include(p => p.Post)
                .Include(p => p.User)
                .ToListAsync();
        }

        public void Remove(Bookmark bookmark)
        {
            _context.Bookmarks.Remove(bookmark);
        }

        public async void UnassignBookmark(long userId, long postId)
        {
            Bookmark bookmark = await FindByUserIdAndPostId(userId, postId);
            if (bookmark != null)
            {
                Remove(bookmark);
            }
        }

        public async Task<IEnumerable<Bookmark>> ListAsync()
        {
            return await _context.Bookmarks.ToListAsync();
        }


    }
}