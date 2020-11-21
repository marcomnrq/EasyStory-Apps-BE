using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Repositories
{
    public interface IQualificationRepository
    {
        Task<IEnumerable<Qualification>> ListAsync();
        Task AddAsync(Qualification qualification);
        Task<Qualification> FindById(long id);
        Task<Qualification> FindByPostIdandUserId(long userId, long postId);
        void Update(Qualification qualification);
        void Remove(Qualification qualification);
        Task<IEnumerable<Qualification>> ListByUserIdAsync(long Id);
        Task<IEnumerable<Qualification>> ListByPostIdAsync(long Id);
    }
}
