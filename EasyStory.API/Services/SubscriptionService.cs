using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Repositories;
using EasyStory.API.Domain.Services;
using EasyStory.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubscriptionResponse> AssignSubscriberSubscribedAsync(Subscription subscription, long userId, long subscribedId)
        {
            subscription.UserId = userId;
            subscription.SubscribedId = subscribedId;
            try
            {
                await _subscriptionRepository.AddAsync(subscription);

                await _unitOfWork.CompleteAsync();
                return new SubscriptionResponse(subscription);
            }
            catch (Exception ex)
            {
                return new SubscriptionResponse($"An error ocurred while saving the subscription: {ex.Message}");
            }
        }

        public async Task<SubscriptionResponse> GetBySubscriberIdAndSubscribedIdAsync(long userId, long subscribedId)
        {
            var existingSubscription = await _subscriptionRepository.FindBySubscriberIdAndSubscribedId(userId,subscribedId);
            if (existingSubscription == null)
                return new SubscriptionResponse("Subscription not found");
            return new SubscriptionResponse(existingSubscription);
        }

        public async Task<IEnumerable<Subscription>> ListAsync()
        {
            return await _subscriptionRepository.ListAsync();
        }

        public async Task<IEnumerable<Subscription>> ListBySubscribedAsync(long subscribedId)
        {
            return await _subscriptionRepository.ListBySubscribedIdAsync(subscribedId);
        }

        public async Task<IEnumerable<Subscription>> ListBySubscriberIdAsync(long userId)
        {
            return await _subscriptionRepository.ListBySubscriberIdAsync(userId);
        }

        public async Task<SubscriptionResponse> UnassignSubscriberSubscribedAsync(long userId, long subscribedId)
        {
            try
            {
                Subscription subscription = await _subscriptionRepository.FindBySubscriberIdAndSubscribedId(userId, subscribedId);
                _subscriptionRepository.Remove(subscription);
                await _unitOfWork.CompleteAsync();
                return new SubscriptionResponse(subscription);
            }
            catch (Exception ex)
            {
                return new SubscriptionResponse($"An error ocurred while unassigning Subscriber to Subscribed: {ex.Message}");
            }
        }
    }
}
