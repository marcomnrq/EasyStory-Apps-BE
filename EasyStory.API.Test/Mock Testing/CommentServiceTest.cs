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
    class CommentServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task GetAllAsyncWhenNoCommentsReturnsEmptyCollection()
        {
            // Arrange
            var mockCommentRepository = GetDefaultICommentRepositoryInstance();
            mockCommentRepository.Setup(r => r.ListAsync())
                .ReturnsAsync(new List<Comment>());
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CommentService(
                mockCommentRepository.Object,
                mockUnitOfWork.Object);

            // Act
            List<Comment> comments = (List<Comment>)await service.ListAsync();
            var commentsCount = comments.Count;

            // Assert
            commentsCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByUserIdAndPostIdAsyncWhenInvalidUserIdAndPostIdReturnsCommentNotFoundReResponse()
        {
            // Arrange
            var mockCommentRepository = GetDefaultICommentRepositoryInstance();
            var postId = 1;
            var userId = 1;
            mockCommentRepository.Setup(r => r.FindByUserIdAndPostId(userId,postId))
                .Returns(Task.FromResult<Comment>(null));
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var service = new CommentService(
               mockCommentRepository.Object,
                mockUnitOfWork.Object);


            // Act
            CommentResponse response = await service.GetByUserIdAndPostIdAsync(userId,postId);
            var message = response.Message;

            // Assert
            message.Should().Be("Comment not found");
        }


        private Mock<ICommentRepository> GetDefaultICommentRepositoryInstance()
        {
            return new Mock<ICommentRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
