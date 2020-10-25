using EasyStory.API.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> ListAsync();
        Task<IEnumerable<Post>> ListByUserIdAsync(long userId);
        Task AddAsync(Post post);
        Task<Post> FindById(long id);
        void Update(Post post);
        void Remove(Post post);
    }
}
