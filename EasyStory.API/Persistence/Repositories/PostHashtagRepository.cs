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
    public class PostHashtagRepository : BaseRepository, IPostHashtagRepository
    {
        public PostHashtagRepository(AppDbContext context) : base(context)
        {

        }
        public async Task AddAsync(PostHashtag postHashtag)
        {
            await _context.PostHashtags.AddAsync(postHashtag);
        }

        public async Task AssignPostHashtag(long postId, long hashtagId)
        {
            PostHashtag postHashtag = await FindByPostIdAndHashtagId(postId,hashtagId);
            if (postHashtag == null)
            {
                postHashtag= new PostHashtag { PostId = postId, HashtagId = hashtagId };
                await AddAsync(postHashtag);
            }
        }

        public async Task<PostHashtag> FindByPostIdAndHashtagId(long postId, long hashtagId)
        {
            return await _context.PostHashtags.FindAsync(postId, hashtagId);
        }

        public async Task<IEnumerable<PostHashtag>> ListByHashtagIdAsync(long hashtagId)
        {
            return await _context.PostHashtags
                .Where(p => p.HashtagId == hashtagId)
                .Include(p => p.Post)
                .Include(p => p.Hashtag)
                .ToListAsync();
        }

        public async Task<IEnumerable<PostHashtag>> ListByPostIdAsync(long postId)
        {
            return await _context.PostHashtags
                .Where(p => p.PostId == postId)
                .Include(p => p.Post)
                .Include(p => p.Hashtag)
                .ToListAsync();
        }

        public void Remove(PostHashtag postHashtag)
        {
            _context.PostHashtags.Remove(postHashtag);
        }

        public async void UnassignPostHashtag(long postId, long hashtagId)
        {
            PostHashtag postHashtag = await FindByPostIdAndHashtagId(postId, hashtagId);
            if (postHashtag != null)
            {
                Remove(postHashtag);
            }
        }
    }
}
