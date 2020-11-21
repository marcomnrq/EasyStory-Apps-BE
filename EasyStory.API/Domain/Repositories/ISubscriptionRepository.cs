using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> ListAsync();
        Task<IEnumerable<Subscription>> ListBySubscriberIdAsync(long userId);
        Task<IEnumerable<Subscription>> ListBySubscribedIdAsync(long subscribedId);
        Task<Subscription> FindBySubscriberIdAndSubscribedId(long userId, long subscribedId);
        Task AddAsync(Subscription subscription);
        void Remove(Subscription subscription);
        Task AssignSubscription(long userId, long subscribedId);
        void UnassignSubscription(long userId, long subscribedId);
    }
}
