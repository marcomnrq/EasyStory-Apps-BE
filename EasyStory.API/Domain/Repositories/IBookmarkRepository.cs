using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;

namespace EasyStory.API.Domain.Repositories
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<Bookmark>>ListAsync();
        Task AddAsync(Bookmark bookmark);
        Task<Bookmark> FindById(long id);
        void Remove(Bookmark bookmark);
    }
}