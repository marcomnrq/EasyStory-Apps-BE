using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Persistence.Contexts;
using EasyStory.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Persistence.Repositories
{
    public class HashtagRepository : BaseRepository, IHashtagRepository
    {
        public HashtagRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Hashtag hashtag)
        {
            await _context.Hashtags.AddAsync(hashtag);
        }

        public async Task<Hashtag> FindById(long id)
        {
            return await _context.Hashtags.FindAsync(id);
        }

        public async Task<IEnumerable<Hashtag>> ListAsync()
        {
            return await _context.Hashtags.ToListAsync();
        }

        public void Remove(Hashtag hashtag)
        {
            _context.Hashtags.Remove(hashtag);
        }

        public void Update(Hashtag hashtag)
        {
            _context.Hashtags.Update(hashtag);
        }
    }
}
