using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services
{
    public interface IHashtagService
    {
        Task<IEnumerable<Hashtag>> ListAsync();
        Task<IEnumerable<Hashtag>> ListByPostIdAsync(long postId);
        Task<HashtagResponse> GetByIdAsync(long id);
        Task<HashtagResponse> SaveHashtagAsync(Hashtag hashtag);
        Task<HashtagResponse> UpdateHashtagAsync(long id, Hashtag hashtag);
        Task<HashtagResponse> DeleteHashtagAsync(long id);
    }
}
