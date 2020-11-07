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

        public async Task<SubscriptionResponse> AssignSubscriberSubscribedAsync(Subscription subscription, long subscriberId, long subscribedId)
        {
            subscription.SubscriberId = subscriberId;
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

        public async Task<IEnumerable<Subscription>> ListBySubscribedAsync(long subscribedId)
        {
            return await _subscriptionRepository.ListBySubscribedIdAsync(subscribedId);
        }

        public async Task<IEnumerable<Subscription>> ListBySubscriberIdAsync(long subscriberId)
        {
            return await _subscriptionRepository.ListBySubscriberIdAsync(subscriberId);
        }

        public async Task<SubscriptionResponse> UnassignSubscriberSubscribedAsync(long subscriberId, long subscribedId)
        {
            try
            {
                Subscription subscription = await _subscriptionRepository.FindBySubscriberIdAndSubscribedId(subscriberId, subscribedId);
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
