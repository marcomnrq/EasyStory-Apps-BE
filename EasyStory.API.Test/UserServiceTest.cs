using EasyStory.API.Domain.Models;
using EasyStory.API.Domain.Repositories;
using EasyStory.API.Domain.Services.Communications;
using EasyStory.API.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyStory.API.Test
{
    public class UserServiceTest

    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoUsersReturnsEmptyCollection()
        {
            //Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            mockUserRepository.Setup(r=> r.ListAsync())
                .ReturnsAsync(new List<User>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserService(
                mockUserRepository.Object,
                mockUnitOfWork.Object);

            //Act
            List<User> users = (List<User>) await service.ListAsync();
            var usersCount = users.Count;

            //Assert
            usersCount.Should().Equals(0);

        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsUserNotFoundReResponse()
        {
            // Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var userId = 1;
            mockUserRepository.Setup(r => r.FindById(userId))
                .Returns(Task.FromResult<User>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new UserService(
                mockUserRepository.Object,
                mockUnitOfWork.Object);


            // Act
            UserResponse response = await service.GetByIdAsync(userId);
            var message = response.Message;

            // Assert
            message.Should().Be("User not found");
        }



        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}