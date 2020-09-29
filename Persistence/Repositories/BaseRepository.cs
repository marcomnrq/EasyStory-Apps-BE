using System;
using EasyStory.API.Domain.Persistence.Contexts;

namespace EasyStory.API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}