using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services.Communications
{
    public class PostHashtagResponse:BaseResponse<PostHashtag>
    {
        public PostHashtagResponse(PostHashtag resource) : base(resource)
        {
        }

        public PostHashtagResponse(string message) : base(message)
        {
        }
    }
}
