using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services
{
    public interface IQualificationService
    {
        Task<IEnumerable<Qualification>> ListAsync();
        Task<IEnumerable<Qualification>> ListByUserIdAsync(long userId);
        Task<IEnumerable<Qualification>> ListByPostIdAsync(long postId);
        Task<QualificationResponse> GetByPostIdandUserIdAsync(long userId, long postId);
        Task<QualificationResponse> SaveQualificationAsync(long userId, long postId, Qualification qualification);
        Task<QualificationResponse> UpdateQualificationAsync(long userId, long postId, Qualification qualification);
        Task<QualificationResponse> UnnasignQualificationAsync(long userId, long postId);
    }
}
