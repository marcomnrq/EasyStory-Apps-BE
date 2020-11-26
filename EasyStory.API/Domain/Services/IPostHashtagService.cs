using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services
{
    public interface IPostHashtagService
    {
        Task<IEnumerable<PostHashtag>> ListByPostIdAsync(long postId);
        Task<IEnumerable<PostHashtag>> ListByHashtagIdAsync(long hashtagId);
        Task<PostHashtagResponse> AssignPostHashtagAsync(long postId, long hashtagId);
        Task<PostHashtagResponse> UnassignPostHashtagAsync(long postId, long hashtagId);
    }
}
