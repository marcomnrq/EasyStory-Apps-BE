using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Persistence.Contexts;
using EasyStory.API.Domain.Repositories;
using System.Linq;

namespace EasyStory.API.Persistence.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task<Comment> FindById(long id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment> FindByUserIdAndPostId(long userId, long postId)
        {
            return await _context.Comments.FindAsync(userId, postId);
        }

        public async Task<IEnumerable<Comment>> ListAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> ListByPostIdAsync(long postId)=>
        
            await _context.Comments
                 .Where(p => p.PostId == postId)
                 .Include(p => p.Post)
                 .ToListAsync();
        

        public async Task<IEnumerable<Comment>> ListByUserIdAsync(long userId) =>
            await _context.Comments
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .ToListAsync();




        public void Remove(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);
        }
    }
}
