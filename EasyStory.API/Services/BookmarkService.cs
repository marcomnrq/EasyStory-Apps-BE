using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Repositories;
using EasyStory.API.Domain.Services;
using EasyStory.API.Domain.Services.Communications;

namespace EasyStory.API.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IUserRepository _userRepository; //Falta implementar


        public BookmarkService(IBookmarkRepository bookmarkRepository, IUnitOfWork unitOfWork)
        {
            _bookmarkRepository = bookmarkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Bookmark>> ListAsync()
        {
            return await _bookmarkRepository.ListAsync();
        }

        public void ListByUserIdAsync(long userId)
        {
            //Falta implementar
            //Error solucionado después de solucionar
        }

        public async Task<BookmarkResponse> GetByIdAsync(long id)
        {
            var existingBookmark = await _bookmarkRepository.FindById(id);
            if (existingBookmark == null)
            {
                return new BookmarkResponse("Bookmark not found");
            }
            return new BookmarkResponse(existingBookmark);
        }

        public async Task<BookmarkResponse> SaveBookmarkAsync(Bookmark bookmark)
        {
            try
            {
                await _bookmarkRepository.AddAsync(bookmark);
                await _unitOfWork.CompleteAsync();
                return new BookmarkResponse(bookmark);
            }
            catch (Exception ex)
            {
                return new BookmarkResponse($"An error occurred while saving the bookmark: {ex.Message}");
            }
        }

        public async Task<BookmarkResponse> DeleteBookmarkAsync(long id)
        {
            var existingBookmark = await _bookmarkRepository.FindById(id);
            if (existingBookmark == null)
                return new BookmarkResponse("Bookmark not found");
            try
            {
                _bookmarkRepository.Remove(existingBookmark);
                await _unitOfWork.CompleteAsync();
                return new BookmarkResponse(existingBookmark);
            }
            catch (Exception ex)
            {
                return new BookmarkResponse($"An error occurred while deleting Bookmark: {ex.Message}");
            }
        }
    }
}