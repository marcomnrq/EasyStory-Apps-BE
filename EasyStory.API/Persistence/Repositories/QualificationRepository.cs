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
    public class QualificationRepository : BaseRepository, IQualificationRepository
    {
        public QualificationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Qualification qualification)
        {
            await _context.Qualifications.AddAsync(qualification);
        }

        public async Task<Qualification> FindById(long id)
        {
            return await _context.Qualifications.FindAsync(id);
        }

        public async Task<IEnumerable<Qualification>> ListAsync()
        {
            return await _context.Qualifications.ToListAsync();
        }

        public void Remove(Qualification qualification)
        {
            _context.Qualifications.Remove(qualification);
        }

        public void Update(Qualification qualification)
        {
            _context.Qualifications.Update(qualification);
        }
        public async Task<IEnumerable<Qualification>> ListByUserIdAsync(long userId)
        {
            return await _context.Qualifications.Where(p => p.UserId == userId).Include(p => p.User).ToListAsync();
        }
        public async Task<Qualification> FindByPostIdandUserId(long userId, long postId)
        {
            return await _context.Qualifications.FindAsync(userId, postId);
        }
        public async Task<IEnumerable<Qualification>> ListByPostIdAsync(long postId)
        {
            return await _context.Qualifications.Where(p => p.PostId == postId).Include(p => p.Post).ToListAsync();
        }
    }
}
