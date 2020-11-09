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
    public class QualificationService : IQualificationService
    {
        private readonly IQualificationRepository _qualificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QualificationService(IQualificationRepository qualificationRepository, IUserRepository userRepository, IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _qualificationRepository = qualificationRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<QualificationResponse> UnnasignQualificationAsync(long userId, long postId)
        {


            try
            {
                Qualification qualification = await _qualificationRepository.FindByPostIdandUserId(userId, postId);
                _qualificationRepository.Remove(qualification);
                await _unitOfWork.CompleteAsync();
                return new QualificationResponse(qualification);
            }
            catch (Exception ex)
            {
                return new QualificationResponse($"An error occurred while deleting Qualification: {ex.Message}");
            }
        }

        public async Task<QualificationResponse> GetByPostIdandUserIdAsync(long userId, long postId)
        {
            var existingQualification = await _qualificationRepository.FindByPostIdandUserId(userId, postId);
            if (existingQualification == null)
            {
                return new QualificationResponse("Qualification not found");
            }
            return new QualificationResponse(existingQualification);
        }

        public async Task<IEnumerable<Qualification>> ListAsync()
        {
            return await _qualificationRepository.ListAsync(); ;
        }

        public async Task<IEnumerable<Qualification>> ListByPostIdAsync(long postId)
        {
            return await _qualificationRepository.ListByPostIdAsync(postId);

        }

        public async Task<IEnumerable<Qualification>> ListByUserIdAsync(long userId)
        {
            return await _qualificationRepository.ListByUserIdAsync(userId);
        }
        public async Task<QualificationResponse> UpdateQualificationAsync(long userId, long postId, Qualification qualification)
        {
            Qualification qualificationexits = await _qualificationRepository.FindByPostIdandUserId(userId, postId);
            if (qualificationexits == null)
            {
                return new QualificationResponse("Qualification not found");
            }
            try
            {

                qualificationexits.Qualificate = qualification.Qualificate;
                _qualificationRepository.Update(qualificationexits);
                await _unitOfWork.CompleteAsync();
                return new QualificationResponse(qualificationexits);
            }
            catch (Exception ex)
            {
                return new QualificationResponse($"An error occurred while deleting Qualification: {ex.Message}");
            }
        }
        public async Task<QualificationResponse> SaveQualificationAsync(long userId, long postId, Qualification qualification)
        {


            var existingQualification = await _qualificationRepository.FindByPostIdandUserId(userId, postId);
            if (existingQualification != null)
            {
                return new QualificationResponse("this Qualification already exits ");
            }


            try
            {
                qualification.UserId = userId;
                qualification.PostId = postId;
                await _qualificationRepository.AddAsync(qualification);
                await _unitOfWork.CompleteAsync();
                return new QualificationResponse(qualification);
            }
            catch (Exception ex)
            {
                return new QualificationResponse($"An error occurred while deleting Qualification: {ex.Message}");
            }
        }
    }
}
