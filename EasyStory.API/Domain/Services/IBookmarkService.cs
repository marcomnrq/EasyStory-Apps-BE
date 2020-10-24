using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;

namespace EasyStory.API.Domain.Services
{
    public interface IBookmarkService
    {
        Task<IEnumerable<Bookmark>> ListAsync();
        void ListByUserIdAsync(long userId); // Falta implementar
        Task<BookmarkResponse> GetByIdAsync(long id);
        Task<BookmarkResponse> SaveBookmarkAsync(Bookmark bookmark);
        Task<BookmarkResponse> DeleteBookmarkAsync(long id);
    }
}