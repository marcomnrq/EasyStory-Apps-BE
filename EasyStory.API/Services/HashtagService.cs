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
    public class HashtagService : IHashtagService
    {
        private readonly IHashtagRepository _hashtagRepository;
        private readonly IUnitOfWork _unitOfWork;
        public HashtagService(IHashtagRepository hashtagRepository,IUnitOfWork unitOfWork)
        {
            _hashtagRepository = hashtagRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HashtagResponse> DeleteHashtagAsync(long id)
        {
            var existingHashtag = await _hashtagRepository.FindById(id);
            if (existingHashtag == null)
                return new HashtagResponse("Hashtag not found");
            try
            {
                _hashtagRepository.Remove(existingHashtag);
                await _unitOfWork.CompleteAsync();
                return new HashtagResponse(existingHashtag);
            }
            catch (Exception ex)
            {
                return new HashtagResponse($"An error ocurred while deleting HashTag: {ex.Message}");
            }
        }

        public async Task<HashtagResponse> GetByIdAsync(long id)
        {
            var existingHashtag = await _hashtagRepository.FindById(id);
            if (existingHashtag == null)
                return new HashtagResponse("Hashtag not found");
            return new HashtagResponse(existingHashtag);
        }

        public async Task<IEnumerable<Hashtag>> ListAsync()
        {
            return await _hashtagRepository.ListAsync();
        }

        public  Task<IEnumerable<Hashtag>> ListByPostIdAsync(long postId)
        {
            // para implementar esta sección se necesita la implementación del servicio  de post, despues realizar su unión en el nuevo post hashtag
            throw new NotImplementedException();
        }

        public async Task<HashtagResponse> SaveHashtagAsync(Hashtag hashtag)
        {
            try
            {
                await _hashtagRepository.AddAsync(hashtag);
                await _unitOfWork.CompleteAsync();
                return new HashtagResponse(hashtag);
            }
            catch (Exception ex)
            {
                return new HashtagResponse($"An error ocurred while saving the hashtag: {ex.Message}");
            }
        }

        public async Task<HashtagResponse> UpdateHashtagAsync(long id, Hashtag hashtag)
        {
            var existingHashtag = await _hashtagRepository.FindById(id);
            if (existingHashtag == null)
                return new HashtagResponse("Hashtag not found");
            try
            {
                existingHashtag.Name = hashtag.Name;
                _hashtagRepository.Update(existingHashtag);
                await _unitOfWork.CompleteAsync();
                return new HashtagResponse(existingHashtag);
            }
            catch(Exception ex)
            {
                return new HashtagResponse($"An error ocurred while updating the hashtag: {ex.Message}");
            }

        }
    }
}
