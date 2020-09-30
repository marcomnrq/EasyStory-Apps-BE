using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
