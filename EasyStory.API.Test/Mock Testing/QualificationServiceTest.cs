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
    class QualificationServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task GetAllAsyncWhenNoQualificationsReturnsEmptyCollection()
        {
            // Arrange
            var mockQualificationRepository = GetDefaultIQualificationRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            mockQualificationRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Qualification>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new QualificationService(mockQualificationRepository.Object,
                                                   mockUserRepository.Object,
                                                   mockPostRepository.Object,
                                                   mockUnitOfWork.Object);

            // Act
            List<Qualification> qualifications = (List<Qualification>)await service.ListAsync();
            var qualificationscount = qualifications.Count;

            // Assert
            qualificationscount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsQualificationNotFoundReResponse()
        {
            // Arrange
            var mockQualificationRepository = GetDefaultIQualificationRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var postId = 1;
            var userId = 1;
            mockQualificationRepository.Setup(r => r.FindByPostIdandUserId(userId, postId))
                .Returns(Task.FromResult<Qualification>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new QualificationService(mockQualificationRepository.Object,
                                                   mockUserRepository.Object,
                                                   mockPostRepository.Object,
                                                   mockUnitOfWork.Object);


            // Act
            QualificationResponse response = await service.GetByPostIdandUserIdAsync(userId, postId);
            var message = response.Message;

            // Assert
            message.Should().Be("Qualification not found");
        }


        private Mock<IQualificationRepository> GetDefaultIQualificationRepositoryInstance()
        {
            return new Mock<IQualificationRepository>();
        }
        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }
        private Mock<IPostRepository> GetDefaultIPostRepositoryInstance()
        {
            return new Mock<IPostRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
