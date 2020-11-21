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
    class HashtagServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoHashtagsReturnsEmptyCollection()
        {
            // Arrange
            var mockHashtagRepository = GetDefaultIHashtagRepositoryInstance();
            var mockPostHashtagRepository = GetDefaultIPostHashtagRepositoryInstance();
            mockHashtagRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Hashtag>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new HashtagService(
                mockHashtagRepository.Object,
                mockPostHashtagRepository.Object,
                mockUnitOfWork.Object);

            // Act
            List<Hashtag> hashtags = (List<Hashtag>)await service.ListAsync();
            var hashtagsCount = hashtags.Count;

            // Assert
            hashtagsCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsHashtagNotFoundReResponse()
        {
            // Arrange
            var mockHashtagRepository = GetDefaultIHashtagRepositoryInstance();
            var mockPostHashtagRepository = GetDefaultIPostHashtagRepositoryInstance();
            var hashtagId = 1;
            mockHashtagRepository.Setup(r => r.FindById(hashtagId))
                .Returns(Task.FromResult<Hashtag>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new HashtagService(
                mockHashtagRepository.Object,
                mockPostHashtagRepository.Object,
                mockUnitOfWork.Object);


            // Act
            HashtagResponse response = await service.GetByIdAsync(hashtagId);
            var message = response.Message;

            // Assert
            message.Should().Be("Hashtag not found");
        }

        private Mock<IHashtagRepository> GetDefaultIHashtagRepositoryInstance()
        {
            return new Mock<IHashtagRepository>();
        }

        private Mock<IPostHashtagRepository> GetDefaultIPostHashtagRepositoryInstance()
        {
            return new Mock<IPostHashtagRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
