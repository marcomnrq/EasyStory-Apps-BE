using System;
using EasyStory.API.Domain.Models;

namespace EasyStory.API.Domain.Services.Communications
{
    public class CommentResponse : BaseResponse<Comment>
    {
        public CommentResponse(Comment resource) : base(resource)
        {
        }

        public CommentResponse(string message) : base(message)
        {
        }
    }
}
