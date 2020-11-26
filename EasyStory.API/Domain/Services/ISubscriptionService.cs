using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> ListAsync();
        Task<IEnumerable<Subscription>> ListBySubscriberIdAsync(long userId);
        Task<IEnumerable<Subscription>> ListBySubscribedAsync(long subscribedId);
        Task<SubscriptionResponse> GetBySubscriberIdAndSubscribedIdAsync(long userId, long subscribedId);
        Task<SubscriptionResponse> AssignSubscriberSubscribedAsync(Subscription subscription, long userId, long subscribedId);
        Task<SubscriptionResponse> UnassignSubscriberSubscribedAsync(long userId, long subscribedId);
    }
}
