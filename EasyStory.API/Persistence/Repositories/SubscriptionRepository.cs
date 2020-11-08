using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Persistence.Contexts;
using EasyStory.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EasyStory.API.Persistence.Repositories
{
    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription); ;
        }

        public async Task AssignSubscription(long subscriberId, long subscribedId)
        {
            Subscription subscription = await FindBySubscriberIdAndSubscribedId(subscriberId, subscribedId);
            if (subscription == null)
            {
                subscription = new Subscription { SubscriberId = subscriberId, SubscribedId = subscribedId };
                await AddAsync(subscription);
            }
        }

        public async  Task<Subscription> FindBySubscriberIdAndSubscribedId(long subscriberId, long subscribedId)
        {
            return await _context.Subscriptions.FindAsync(subscriberId, subscribedId);
        }

        public async Task<IEnumerable<Subscription>> ListAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> ListBySubscribedIdAsync(long subscribedId)
        {
            return await _context.Subscriptions
                .Where(p => p.SubscribedId == subscribedId)
                .Include(p => p.Subscriber)
                .Include(p => p.Subscribed)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> ListBySubscriberIdAsync(long subscriberId)
        {
            return await _context.Subscriptions
                .Where(p => p.SubscriberId == subscriberId)
                .Include(p => p.Subscriber)
                .Include(p => p.Subscribed)
                .ToListAsync();
        }

        public void Remove(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
        }

        public async void UnassignSubscription(long subscriberId, long subscribedId)
        {
           Subscription subscription = await FindBySubscriberIdAndSubscribedId(subscriberId, subscribedId);
            if (subscription != null)
            {
                Remove(subscription);
            }
        }
    }
}
