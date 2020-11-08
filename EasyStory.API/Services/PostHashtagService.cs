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
    public class PostHashtagService : IPostHashtagService
    {
        private readonly IPostHashtagRepository _postHashtagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostHashtagService(IPostHashtagRepository postHashtagRepository, IUnitOfWork unitOfWork)
        {
            _postHashtagRepository = postHashtagRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<PostHashtagResponse> AssignPostHashtagAsync(long postId, long hashtagId)
        {
            try
            {

                await _postHashtagRepository.AssignPostHashtag(postId, hashtagId);
                await _unitOfWork.CompleteAsync();
                PostHashtag postHashtag = await _postHashtagRepository.FindByPostIdAndHashtagId(postId, hashtagId);
                return new PostHashtagResponse(postHashtag);
            }
            catch (Exception ex)
            {
                return new PostHashtagResponse($"An error ocurred while assigning Hashtag to Post: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PostHashtag>> ListByHashtagIdAsync(long hashtagId)
        {
            return await _postHashtagRepository.ListByHashtagIdAsync(hashtagId);
        }

        public async Task<IEnumerable<PostHashtag>> ListByPostIdAsync(long postId)
        {
            return await _postHashtagRepository.ListByPostIdAsync(postId);
        }

        public async Task<PostHashtagResponse> UnassignPostHashtagAsync(long postId, long hashtagId)
        {
            try
            {
                PostHashtag postHashtag = await _postHashtagRepository.FindByPostIdAndHashtagId(postId, hashtagId);
                _postHashtagRepository.Remove(postHashtag);
                await _unitOfWork.CompleteAsync();
                return new PostHashtagResponse(postHashtag);
            }
            catch (Exception ex)
            {
                return new PostHashtagResponse($"An error ocurred while unassigning Hashtag to Post: {ex.Message}");
            }
        }
    }
}
