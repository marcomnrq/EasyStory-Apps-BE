using System.Collections.Generic;
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

        public async Task<IEnumerable<Bookmark>> ListAsync()
        {
            return await _context.Bookmarks.ToListAsync();
        }

        public async Task AddAsync(Bookmark bookmark)
        {
            await _context.Bookmarks.AddAsync(bookmark);
        }

        public async Task<Bookmark> FindById(long id)
        {
            return await _context.Bookmarks.FindAsync(id);
        }
        
        public void Remove(Bookmark bookmark)
        {
            _context.Bookmarks.Remove(bookmark);
        }
        
    }
}