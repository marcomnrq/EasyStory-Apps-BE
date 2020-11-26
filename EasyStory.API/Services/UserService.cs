using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Repositories;
using EasyStory.API.Domain.Services;
using EasyStory.API.Domain.Services.Communications;
using EasyStory.API.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EasyStory.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ISubscriptionRepository subscriptionRepository, IBookmarkRepository bookmarkRepository, IOptions<AppSettings> appSettings)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
            _bookmarkRepository = bookmarkRepository;
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            // TODO: Implement Repository-base behavior
            var user = await _userRepository.Authenticate(request.Username, request.Password);

            // Return when user not found
            if (user == null) return null;

            var token = GenerateJwtToken(user);

            return new AuthenticationResponse(user, token);
        }


        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            // Setup Security Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
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

        public async Task<IEnumerable<User>> ListBySubscriberIdAsync(long subscriberId)
        {
            var subscription = await _subscriptionRepository.ListBySubscriberIdAsync(subscriberId);
            var subscribed = subscription.Select(p => p.Subscribed).ToList();
            return subscribed;
        }

        public async Task<IEnumerable<User>> ListByUserIdAsync(long userId)
        {
            var user = await _subscriptionRepository.ListBySubscribedIdAsync(userId);
            var subscriber = user.Select(p => p.User).ToList();
            return subscriber;
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
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
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
