using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> ListBySubscriberIdAsync(long subscriberId);
        Task<IEnumerable<Subscription>> ListBySubscribedIdAsync(long subscribedId);
        Task<Subscription> FindBySubscriberIdAndSubscribedId(long subscriberId, long subscribedId);
        Task AddAsync(Subscription subscription);
        void Remove(Subscription subscription);
        Task AssignSubscription(long subscriberId, long subscribedId);
        void UnassignSubscription(long subscriberId, long subscribedId);
    }
}
