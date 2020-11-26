using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Repositories;
using EasyStory.API.Domain.Services.Communications;
using EasyStory.API.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyStory.API.Test.Mock_Testing
{
    class SubscriptionServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task GetAllAsyncWhenNoSubsciptionsReturnsEmptyCollection()
        {
            // Arrange
            var mockSubscriptionRepository = GetDefaultISubscriptionRepositoryInstance();
            mockSubscriptionRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Subscription>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionService(
                mockSubscriptionRepository.Object,
                mockUnitOfWork.Object);

            // Act
            List<Subscription> subscriptions = (List<Subscription>)await service.ListAsync();
            var subscriptionCount = subscriptions.Count;

            // Assert
            subscriptionCount.Should().Equals(0);
        }

        [Test]
        public async Task GetBySubscriberIdAsyncWhenInvalidIdReturnsSubscriptionNotFoundReResponse()
        {
            // Arrange
            var mockSubscriptionRepository = GetDefaultISubscriptionRepositoryInstance();
            var subscriberId = 1;
            var subscribedId = 1;
            mockSubscriptionRepository.Setup(r => r.FindBySubscriberIdAndSubscribedId(subscriberId,subscribedId))
                .Returns(Task.FromResult<Subscription>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new SubscriptionService(
                mockSubscriptionRepository.Object,
                mockUnitOfWork.Object);


            // Act
            SubscriptionResponse response = await service.GetBySubscriberIdAndSubscribedIdAsync(subscriberId, subscribedId);
            var message = response.Message;

            // Assert
            message.Should().Be("Subscription not found");
        }


        private Mock<ISubscriptionRepository> GetDefaultISubscriptionRepositoryInstance()
        {
            return new Mock<ISubscriptionRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
