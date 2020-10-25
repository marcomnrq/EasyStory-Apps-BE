using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services.Communications
{
    public class PostResponse:BaseResponse<Post>
    {
        public PostResponse(Post resource) : base(resource)
        {
        }

        public PostResponse(string message) : base(message)
        {
        }
    }
}
