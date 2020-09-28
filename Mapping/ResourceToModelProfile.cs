using AutoMapper;
using EasyStory.API.Domain.Models;
using EasyStory.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyStory.API.Mapping
{
    public class ResourceToModelProfile:Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveHashtagResource,Hashtag>();
        }
    }
}
