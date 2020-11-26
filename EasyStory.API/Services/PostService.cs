using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Repositories;
using EasyStory.API.Domain.Services;
using EasyStory.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Services
{
    public class PostService:IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostHashtagRepository _postHashtagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookmarkRepository _bookmarkRepository;
        public PostService(IPostRepository postRepository, IPostHashtagRepository postHashtagRepository, IBookmarkRepository bookmarkRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _postHashtagRepository = postHashtagRepository;
            _bookmarkRepository = bookmarkRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<PostResponse> DeletePostAsync(long id)
        {
            var existingPost = await _postRepository.FindById(id);
            if (existingPost == null)
                return new PostResponse("Post not found");
            try
            {
                _postRepository.Remove(existingPost);
                await _unitOfWork.CompleteAsync();
                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                return new PostResponse($"An error ocurred while deleting Post: {ex.Message}");
            }
        }

        public async Task<PostResponse> GetByIdAsync(long id)
        {
            var existingPost = await _postRepository.FindById(id);
            if (existingPost == null)
                return new PostResponse("Post not found");
            return new PostResponse(existingPost);
        }

        public async Task<IEnumerable<Post>> ListAsync()
        {
            return await _postRepository.ListAsync();
        }

        public async Task<IEnumerable<Post>> ListByHashtagIdAsync(long hashtagId)
        {
            var postHashtags = await _postHashtagRepository.ListByHashtagIdAsync(hashtagId);
            var posts = postHashtags.Select(p => p.Post).ToList();
            return posts;
        }

        public async Task<IEnumerable<Post>> ListByReaderIdAsync(long readerId)
        {
            var bookmark = await _bookmarkRepository.ListByUserIdAsync(readerId);
            var post = bookmark.Select(p => p.Post).ToList();
            return post;
        }

        public async Task<IEnumerable<Post>> ListByUserIdAsync(long userId)
        {
            return await _postRepository.ListByUserIdAsync(userId);
        }

        public async Task<PostResponse> SavePostAsync(Post post, long userId)
        {
            post.UserId = userId;
            try
            {
                await _postRepository.AddAsync(post);
                
                await _unitOfWork.CompleteAsync();
                return new PostResponse(post);
            }
            catch (Exception ex)
            {
                return new PostResponse($"An error ocurred while saving the post: {ex.Message}");
            }
        }

        public async Task<PostResponse> UpdatePostAsync(long id, Post post)
        {
            var existingPost = await _postRepository.FindById(id);
            if (existingPost == null)
                return new PostResponse("Post not found");
            try
            {
                existingPost.Title = post.Title;
                existingPost.Description = post.Description;
                existingPost.Content = post.Content;
                _postRepository.Update(existingPost);
                await _unitOfWork.CompleteAsync();
                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                return new PostResponse($"An error ocurred while updating the post: {ex.Message}");
            }
        }
    }
}
