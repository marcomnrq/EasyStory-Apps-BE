using EasyStory.API.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Repositories
{
    public interface IHashtagRepository
    {
        Task<IEnumerable<Hashtag>> ListAsync();
        Task AddAsync(Hashtag hashtag);
        Task<Hashtag> FindById(long id);
        void Update(Hashtag hashtag);
        void Remove(Hashtag hashtag);
    }
}
