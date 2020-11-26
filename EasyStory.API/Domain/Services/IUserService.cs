using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<IEnumerable<User>> ListByUserIdAsync(long userId);
        Task<IEnumerable<User>> ListBySubscriberIdAsync(long subscriberId);
        Task<UserResponse> GetByIdAsync(long id);
        Task<UserResponse> SaveUserAsync(User user);
        Task<UserResponse> UpdateUserAsync(long id, User user);
        Task<UserResponse> DeleteUserAsync(long id);
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
    }
}
