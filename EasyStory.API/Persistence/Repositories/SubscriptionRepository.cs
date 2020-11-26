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

        public async Task AssignSubscription(long userId, long subscribedId)
        {
            Subscription subscription = await FindBySubscriberIdAndSubscribedId(userId, subscribedId);
            if (subscription == null)
            {
                subscription = new Subscription { UserId = userId, SubscribedId = subscribedId };
                await AddAsync(subscription);
            }
        }

        public async  Task<Subscription> FindBySubscriberIdAndSubscribedId(long userId, long subscribedId)
        {
            return await _context.Subscriptions.FindAsync(userId, subscribedId);
        }

        public async Task<IEnumerable<Subscription>> ListAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> ListBySubscribedIdAsync(long subscribedId)
        {
            return await _context.Subscriptions
                .Where(p => p.SubscribedId == subscribedId)
                .Include(p => p.User)
                .Include(p => p.Subscribed)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> ListBySubscriberIdAsync(long userId)
        {
            return await _context.Subscriptions
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .Include(p => p.Subscribed)
                .ToListAsync();
        }

        public void Remove(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
        }

        public async void UnassignSubscription(long userId, long subscribedId)
        {
           Subscription subscription = await FindBySubscriberIdAndSubscribedId(userId, subscribedId);
            if (subscription != null)
            {
                Remove(subscription);
            }
        }
    }
}
