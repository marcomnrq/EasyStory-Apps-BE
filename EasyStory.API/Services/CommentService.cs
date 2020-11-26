using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Repositories;
using EasyStory.API.Domain.Services;
using EasyStory.API.Domain.Services.Communications;

namespace EasyStory.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(ICommentRepository CommentRepository, IUnitOfWork unitOfWork)
        {
            _CommentRepository = CommentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentResponse> DeleteAsync(long id)
        {
            var existingComment = await _CommentRepository.FindById(id);

            if (existingComment == null)
                return new CommentResponse("Comment not found");

            try
            {
                _CommentRepository.Remove(existingComment);
                await _unitOfWork.CompleteAsync();

                return new CommentResponse(existingComment);
            }
            catch (Exception ex)
            {
                return new CommentResponse($"An error ocurred while deleting Comment: {ex.Message}");
            }
        }

        public async Task<CommentResponse> GetByIdAsync(long id)
        {
            var existingComment = await _CommentRepository.FindById(id);
            if (existingComment == null)
                return new CommentResponse("Comment not found");
            return new CommentResponse(existingComment);
        }

        public async Task<CommentResponse> GetByUserIdAndPostIdAsync(long userId, long postId)
        {
            var existingComment = await _CommentRepository.FindByUserIdAndPostId(userId, postId);
            if (existingComment == null)
                return new CommentResponse("Comment not found");
            return new CommentResponse(existingComment);
        }

        public async Task<IEnumerable<Comment>> ListAsync()
        {
            return await _CommentRepository.ListAsync();
        }

        public async Task<IEnumerable<Comment>> ListByPostIdAsync(long postId)
        {
            return await _CommentRepository.ListByPostIdAsync(postId);
        }

        public async Task<IEnumerable<Comment>> ListByUserIdAsync(long userId)
        {
            return await _CommentRepository.ListByUserIdAsync(userId);
        }

        public async Task<CommentResponse> SaveAsync(Comment Comment, long userId, long postId)
        {
            Comment.UserId = userId;
            Comment.PostId = postId;
            try
            {
                await _CommentRepository.AddAsync(Comment);
                await _unitOfWork.CompleteAsync();

                return new CommentResponse(Comment);
            }
            catch (Exception ex)
            {
                return new CommentResponse(
                    $"An error ocurred while saving the Comment: {ex.Message}");
            }
        }

        public async Task<CommentResponse> UpdateAsync(long id, Comment Comment)
        {
            var existingComment = await _CommentRepository.FindById(id);

            if (existingComment == null)
                return new CommentResponse("Comment not found");

            existingComment.Content = Comment.Content;

            try
            {
                _CommentRepository.Update(existingComment);
                await _unitOfWork.CompleteAsync();

                return new CommentResponse(existingComment);
            }
            catch (Exception ex)
            {
                return new CommentResponse($"An error ocurred while updating Comment: {ex.Message}");
            }
        }
    }
}
