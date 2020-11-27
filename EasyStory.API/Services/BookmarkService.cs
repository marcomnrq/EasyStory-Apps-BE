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

        public async Task<BookmarkResponse> AssignUserPostAsync(long userId, long postId)
        {

            try
            {
                await _bookmarkRepository.AssignBookmark(userId, postId);
                await _unitOfWork.CompleteAsync();
                Bookmark bookmark = await _bookmarkRepository.FindByUserIdAndPostId(userId, postId);
                return new BookmarkResponse(bookmark);
            }
            catch (Exception ex)
            {
                return new BookmarkResponse($"An error ocurred while assigning Bookmark: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Bookmark>> ListByUserIdAsync(long userId)
        {
            return await _bookmarkRepository.ListByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Bookmark>> ListByPostIdAsync(long postId)
        {
            return await _bookmarkRepository.ListByPostIdAsync(postId);
        }

        public async Task<BookmarkResponse> UnassignUserPostAsync(long userId, long postId)
        {
            try
            {
                Bookmark bookmark = await _bookmarkRepository.FindByUserIdAndPostId(userId, postId);
                _bookmarkRepository.Remove(bookmark);
                await _unitOfWork.CompleteAsync();
                return new BookmarkResponse(bookmark);
            }
            catch (Exception ex)
            {
                return new BookmarkResponse($"An error ocurred while unassigning Bookmark: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Bookmark>> ListAsync()
        {
            return await _bookmarkRepository.ListAsync();

        }

        public async Task<BookmarkResponse> GetByUserIdAndPostIdAsync(long userId, long postId)
        {
            var existingBookmark = await _bookmarkRepository.FindByUserIdAndPostId(userId, postId);
            if (existingBookmark == null)
                return new BookmarkResponse("Bookmark not found");
            return new BookmarkResponse(existingBookmark);
        }
    }
}