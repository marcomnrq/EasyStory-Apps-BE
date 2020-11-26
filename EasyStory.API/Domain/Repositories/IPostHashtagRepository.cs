using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Repositories
{
    public interface IPostHashtagRepository
    {
        Task<IEnumerable<PostHashtag>> ListByPostIdAsync(long postId);
        Task<IEnumerable<PostHashtag>> ListByHashtagIdAsync(long hashtagId);
        Task<PostHashtag> FindByPostIdAndHashtagId(long postId, long hashtagId);
        Task AddAsync(PostHashtag postHashtag);
        void Remove(PostHashtag postHashtag);
        Task AssignPostHashtag(long postId, long hashtagId);
        void UnassignPostHashtag(long postId, long hashtagId);
    }
}
