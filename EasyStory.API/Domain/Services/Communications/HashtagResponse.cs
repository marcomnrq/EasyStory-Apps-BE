using EasyStory.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Domain.Services.Communications
{
    public class HashtagResponse : BaseResponse<Hashtag>
    {
        public HashtagResponse(Hashtag resource) : base(resource)
        {
        }

        public HashtagResponse(string message) : base(message)
        {
        }
    }
}
