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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> DeleteUserAsync(long id)
        {
            var existingUser = await _userRepository.FindById(id);
            if (existingUser == null)
                return new UserResponse("User not found");
            try
            {
                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting User: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetByIdAsync(long id)
        {
            var existingUser = await _userRepository.FindById(id);
            if (existingUser == null)
                return new UserResponse("User not found");
            return new UserResponse(existingUser);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _userRepository.ListAsync();
        }

        public Task<IEnumerable<User>> ListByUserIdAsync(long postId)
        {
            // para implementar esta sección se necesita la implementación del servicio  de user
            throw new NotImplementedException();
        }

        public async Task<UserResponse> SaveUserAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving the user: {ex.Message}");
            }
        }

        public Task<UserResponse> UpdateHUserAsync(long id, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> UpdateUserAsync(long id, User user)
        {
            var existingUser = await _userRepository.FindById(id);
            if (existingUser == null)
                return new UserResponse("User not found");
            try
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.AccountBalance = user.AccountBalance;
                existingUser.SubscriptionPrice = user.SubscriptionPrice;
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating the user: {ex.Message}");
            }

        }
    }
}
